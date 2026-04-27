using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebbyPoints.Data;
using WebbyPoints.Models;

namespace WebbyPoints.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

// =====================================================
    // EXPLORAR: Filtra, busca y ordena puntos de interés
    // =====================================================
    public async Task<IActionResult> Explorar(string plan, string searchTerm, string sortBy)
    {
        ViewData["Plan"] = plan ?? "todos";
        ViewData["SearchTerm"] = searchTerm; // Guardamos lo que buscó para dejarlo escrito
        ViewData["SortBy"] = sortBy;

        var query = _context.PuntosInteres.AsQueryable();

        // 1. Filtrar por Plan (Solo, Pareja, Amigos)
        if (!string.IsNullOrEmpty(plan) && plan.ToLower() != "todos")
        {
            query = query.Where(p => p.PlanTipo.ToLower() == plan.ToLower());
        }

        // 2. Búsqueda por Texto (Busca en Nombre o Categoría)
        if (!string.IsNullOrEmpty(searchTerm))
        {
            var termino = searchTerm.ToLower();
            query = query.Where(p => p.Nombre.ToLower().Contains(termino) || 
                                     p.Categoria.ToLower().Contains(termino));
        }

        // 3. Ordenamiento (Select de la vista)
        switch (sortBy)
        {
            case "calificacion":
                query = query.OrderByDescending(p => p.Calificacion);
                break;
            case "cercanos":
                query = query.OrderBy(p => p.Distancia);
                break;
            case "baratos":
                query = query.OrderBy(p => p.PrecioRango); // Ordena $, $$, $$$
                break;
            default:
                // Si no eligió filtro, usamos tu Lógica de Match con la sesión
                var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
                if (usuarioId.HasValue)
                {
                    var usuario = await _context.Usuarios.FindAsync(usuarioId.Value);
                    if (usuario != null && !string.IsNullOrEmpty(usuario.Preferencias))
                    {
                        var preferencias = usuario.Preferencias
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(p => p.Trim().ToLower())
                            .ToList();

                        query = query.OrderByDescending(p =>
                            preferencias.Contains(p.Categoria.ToLower()) ? 1 : 0
                        ).ThenByDescending(p => p.Calificacion);
                    }
                }
                else
                {
                    query = query.OrderByDescending(p => p.Calificacion);
                }
                break;
        }

        var puntos = await query.Take(20).ToListAsync();

        // ==========================================================
        // SESIONES + REDIS: Leer los IDs de "Vistos Recientemente"
        // desde la sesión privada de ESTE usuario (guardada en Redis Cloud)
        // ==========================================================
        await HttpContext.Session.LoadAsync();
        var vistosJson = HttpContext.Session.GetString("VistosRecientes");
        var vistosIds = new List<int>();

        if (!string.IsNullOrEmpty(vistosJson))
        {
            vistosIds = JsonSerializer.Deserialize<List<int>>(vistosJson) ?? new List<int>();
        }

        if (vistosIds.Any())
        {
            var vistoPuntos = await _context.PuntosInteres
                .Where(p => vistosIds.Contains(p.Id))
                .ToListAsync();

            // Ordenar en el mismo orden que la lista de IDs (más reciente primero)
            ViewBag.VistosRecientes = vistosIds
                .Select(id => vistoPuntos.FirstOrDefault(p => p.Id == id))
                .Where(p => p != null)
                .ToList();
        }
        else
        {
            ViewBag.VistosRecientes = new List<PuntoInteres>();
        }

        return View(puntos);
    }

    // =====================================================
    // DETALLE: Muestra la ficha completa de un punto
    // con sus reseñas y datos del usuario que las escribió
    // =====================================================
    public async Task<IActionResult> Detalle(int id)
    {
        var punto = await _context.PuntosInteres
            .Include(p => p.Reseñas)
                .ThenInclude(r => r.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (punto == null)
            return NotFound();

        // ==========================================================
        // SESIONES + REDIS: Guardar este punto en "Vistos Recientemente"
        // Cada usuario tiene su propia lista privada en su sesión.
        // La sesión se almacena físicamente en Redis Cloud.
        // ==========================================================
        // Aseguramos que la sesión cargue de forma asíncrona para evitar deadlocks con Redis
        await HttpContext.Session.LoadAsync();
        var vistosJson = HttpContext.Session.GetString("VistosRecientes");
        var vistosIds = new List<int>();

        if (!string.IsNullOrEmpty(vistosJson))
        {
            try
            {
                vistosIds = JsonSerializer.Deserialize<List<int>>(vistosJson) ?? new List<int>();
            }
            catch (JsonException)
            {
                vistosIds = new List<int>();
                HttpContext.Session.Remove("VistosRecientes");
            }
        }

        // Eliminar si ya existía (para moverlo al inicio)
        vistosIds.Remove(id);

        // Insertar al inicio (el más reciente primero)
        vistosIds.Insert(0, id);

        // Máximo 5 elementos para no sobrecargar
        if (vistosIds.Count > 5)
            vistosIds = vistosIds.Take(5).ToList();

        // Guardar de vuelta en la sesión → viaja a Redis Cloud automáticamente
        HttpContext.Session.SetString("VistosRecientes", JsonSerializer.Serialize(vistosIds));

        return View(punto);
    }

    // =====================================================
    // CREAR RESEÑA: POST desde la tarjeta lateral de Detalle
    // =====================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearReseña(int puntoId, int calificacion, string comentario)
    {
        await HttpContext.Session.LoadAsync();
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (usuarioId == null)
            return RedirectToAction("Login", "Account");

        var reseña = new Reseña
        {
            PuntoInteresId = puntoId,
            UsuarioId = usuarioId.Value,
            Calificacion = calificacion,
            Comentario = comentario,
            FechaPublicacion = DateTime.Now
        };

        _context.Reseñas.Add(reseña);

        // Recalcular la calificación promedio del punto
        var punto = await _context.PuntosInteres
            .Include(p => p.Reseñas)
            .FirstOrDefaultAsync(p => p.Id == puntoId);

        if (punto != null)
        {
            punto.Reseñas.Add(reseña);
            punto.Calificacion = punto.Reseñas.Average(r => r.Calificacion);
        }

        // Dar puntos al usuario por la reseña (+10 pts)
        var usuario = await _context.Usuarios.FindAsync(usuarioId.Value);
        if (usuario != null)
        {
            usuario.Puntos += 10;
            // Actualizar rango según puntos
            usuario.Rango = usuario.Puntos switch
            {
                >= 500 => "Leyenda",
                >= 200 => "Experto",
                >= 100 => "Local",
                >= 30  => "Explorador",
                _      => "Novato"
            };
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Detalle", new { id = puntoId });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}