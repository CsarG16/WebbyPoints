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
    // EXPLORAR: Filtra puntos de interés según plan y
    // preferencias del usuario logueado
    // =====================================================
    public async Task<IActionResult> Explorar(string plan)
    {
        ViewData["Plan"] = plan;

        // 1. Obtener todos los puntos que coinciden con el plan seleccionado
        var query = _context.PuntosInteres
            .AsQueryable()
            .Where(p => p.PlanTipo.ToLower() == plan.ToLower());

        // 2. Si hay un usuario en sesión, personalizar con sus preferencias
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (usuarioId.HasValue)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId.Value);
            if (usuario != null && !string.IsNullOrEmpty(usuario.Preferencias))
            {
                // Convertir las preferencias guardadas en una lista
                // Ej: "Gym,Cafés,Cine" → ["Gym", "Cafés", "Cine"]
                var preferencias = usuario.Preferencias
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim().ToLower())
                    .ToList();

                // Dar prioridad a puntos cuya Categoría coincida con las preferencias
                // Los que sí coinciden van primero, ordenados por calificación
                query = query.OrderByDescending(p =>
                    preferencias.Contains(p.Categoria.ToLower()) ? 1 : 0
                ).ThenByDescending(p => p.Calificacion);
            }
        }
        else
        {
            // Sin sesión: ordenar solo por calificación descendente
            query = query.OrderByDescending(p => p.Calificacion);
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
