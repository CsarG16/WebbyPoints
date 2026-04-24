using System.Diagnostics;
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
        return View(puntos);
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