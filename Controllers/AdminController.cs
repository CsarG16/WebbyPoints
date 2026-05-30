using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebbyPoints.Data;
using WebbyPoints.Models;
using Microsoft.Extensions.Caching.Distributed;
using WebbyPoints.Helpers;

namespace WebbyPoints.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;

    public AdminController(ApplicationDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    // =====================================================
    // HELPER: Verificar que el usuario sea admin
    // =====================================================
    private async Task<bool> EsAdmin()
    {
        await HttpContext.Session.LoadAsync();
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (usuarioId == null) return false;

        var usuario = await _context.Usuarios.FindAsync(usuarioId.Value);
        return usuario?.EsAdmin ?? false;
    }

    // =====================================================
    // DASHBOARD ADMIN
    // =====================================================
    public async Task<IActionResult> Index()
    {
        if (!await EsAdmin())
            return RedirectToAction("Login", "Account");

        // ==========================================================
        // REDIS CACHE: Estadísticas del dashboard cacheadas por 2 min
        // Son 4 consultas COUNT/AVG que se ejecutan cada vez que un
        // admin entra al panel. Con Redis, solo se ejecutan cada 2 min.
        // ==========================================================
        var stats = await RedisCacheHelper.GetOrSetAsync(
            _cache,
            "admin:dashboard_stats",
            async () => new DashboardStats
            {
                TotalLugares = await _context.PuntosInteres.CountAsync(),
                TotalUsuarios = await _context.Usuarios.CountAsync(),
                TotalReseñas = await _context.Reseñas.CountAsync(),
                PromedioCalificacion = await _context.PuntosInteres.CountAsync() > 0
                    ? await _context.PuntosInteres.AverageAsync(p => p.Calificacion)
                    : 0
            },
            absoluteExpiration: TimeSpan.FromMinutes(2)
        );

        ViewBag.TotalLugares = stats?.TotalLugares ?? 0;
        ViewBag.TotalUsuarios = stats?.TotalUsuarios ?? 0;
        ViewBag.TotalReseñas = stats?.TotalReseñas ?? 0;
        ViewBag.PromedioCalificacion = stats?.PromedioCalificacion ?? 0;

        // Últimos usuarios y reseñas (no cacheados, son datos en tiempo real para admin)
        var ultimosUsuarios = await _context.Usuarios
            .OrderByDescending(u => u.FechaRegistro)
            .Take(5)
            .ToListAsync();
        ViewBag.UltimosUsuarios = ultimosUsuarios;

        var ultimasReseñas = await _context.Reseñas
            .Include(r => r.Usuario)
            .Include(r => r.PuntoInteres)
            .OrderByDescending(r => r.FechaPublicacion)
            .Take(5)
            .ToListAsync();
        ViewBag.UltimasReseñas = ultimasReseñas;

        return View();
    }

    // =====================================================
    // GESTIÓN DE LUGARES
    // =====================================================
    public async Task<IActionResult> Lugares()
    {
        if (!await EsAdmin())
            return RedirectToAction("Login", "Account");

        var lugares = await _context.PuntosInteres
            .Include(p => p.Reseñas)
            .OrderByDescending(p => p.Id)
            .ToListAsync();

        return View(lugares);
    }

    // =====================================================
    // CREAR LUGAR: GET
    // =====================================================
    [HttpGet]
    public async Task<IActionResult> CrearLugar()
    {
        if (!await EsAdmin())
            return RedirectToAction("Login", "Account");

        return View(new PuntoInteres());
    }

    // =====================================================
    // CREAR LUGAR: POST
    // =====================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearLugar(PuntoInteres lugar)
    {
        if (!await EsAdmin())
            return RedirectToAction("Login", "Account");

        ModelState.Remove("Reseñas");

        if (!ModelState.IsValid)
            return View(lugar);

        _context.PuntosInteres.Add(lugar);
        await _context.SaveChangesAsync();

        // Invalidar cachés de Redis relacionados con lugares
        await RedisCacheHelper.InvalidateMultipleAsync(_cache,
            "home:destacados", "admin:dashboard_stats");

        TempData["Success"] = $"Lugar \"{lugar.Nombre}\" creado exitosamente.";
        return RedirectToAction("Lugares");
    }

    // =====================================================
    // EDITAR LUGAR: GET
    // =====================================================
    [HttpGet]
    public async Task<IActionResult> EditarLugar(int id)
    {
        if (!await EsAdmin())
            return RedirectToAction("Login", "Account");

        var lugar = await _context.PuntosInteres.FindAsync(id);
        if (lugar == null) return NotFound();

        return View(lugar);
    }

    // =====================================================
    // EDITAR LUGAR: POST
    // =====================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarLugar(PuntoInteres lugar)
    {
        if (!await EsAdmin())
            return RedirectToAction("Login", "Account");

        ModelState.Remove("Reseñas");

        if (!ModelState.IsValid)
            return View(lugar);

        _context.PuntosInteres.Update(lugar);
        await _context.SaveChangesAsync();

        // Invalidar cachés de Redis relacionados con lugares
        await RedisCacheHelper.InvalidateMultipleAsync(_cache,
            "home:destacados", "admin:dashboard_stats");

        TempData["Success"] = $"Lugar \"{lugar.Nombre}\" actualizado exitosamente.";
        return RedirectToAction("Lugares");
    }

    // =====================================================
    // ELIMINAR LUGAR: POST
    // =====================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarLugar(int id)
    {
        if (!await EsAdmin())
            return RedirectToAction("Login", "Account");

        var lugar = await _context.PuntosInteres
            .Include(p => p.Reseñas)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (lugar == null) return NotFound();

        // Eliminar reseñas asociadas primero
        _context.Reseñas.RemoveRange(lugar.Reseñas);
        _context.PuntosInteres.Remove(lugar);
        await _context.SaveChangesAsync();

        // Invalidar cachés de Redis relacionados con lugares y reseñas
        await RedisCacheHelper.InvalidateMultipleAsync(_cache,
            "home:destacados", "admin:dashboard_stats");

        TempData["Success"] = $"Lugar \"{lugar.Nombre}\" eliminado.";
        return RedirectToAction("Lugares");
    }

    // =====================================================
    // GESTIÓN DE USUARIOS
    // =====================================================
    public async Task<IActionResult> Usuarios()
    {
        if (!await EsAdmin())
            return RedirectToAction("Login", "Account");

        var usuarios = await _context.Usuarios
            .Include(u => u.Reseñas)
            .OrderByDescending(u => u.FechaRegistro)
            .ToListAsync();

        return View(usuarios);
    }

    // =====================================================
    // GESTIÓN DE RESEÑAS
    // =====================================================
    public async Task<IActionResult> Reseñas()
    {
        if (!await EsAdmin())
            return RedirectToAction("Login", "Account");

        var reseñas = await _context.Reseñas
            .Include(r => r.Usuario)
            .Include(r => r.PuntoInteres)
            .OrderByDescending(r => r.FechaPublicacion)
            .ToListAsync();

        return View(reseñas);
    }

    // =====================================================
    // ELIMINAR RESEÑA: POST
    // =====================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarReseña(int id)
    {
        if (!await EsAdmin())
            return RedirectToAction("Login", "Account");

        var reseña = await _context.Reseñas.FindAsync(id);
        if (reseña == null) return NotFound();

        var puntoId = reseña.PuntoInteresId;
        _context.Reseñas.Remove(reseña);

        // Recalcular calificación del punto
        var punto = await _context.PuntosInteres
            .Include(p => p.Reseñas)
            .FirstOrDefaultAsync(p => p.Id == puntoId);

        if (punto != null)
        {
            var restantes = punto.Reseñas.Where(r => r.Id != id).ToList();
            punto.Calificacion = restantes.Any() ? restantes.Average(r => r.Calificacion) : 0;
        }

        await _context.SaveChangesAsync();

        // Invalidar estadísticas del dashboard en Redis
        await RedisCacheHelper.InvalidateAsync(_cache, "admin:dashboard_stats");

        TempData["Success"] = "Reseña eliminada exitosamente.";
        return RedirectToAction("Reseñas");
    }
}

// DTO para cachear las estadísticas del dashboard admin en Redis
public class DashboardStats
{
    public int TotalLugares { get; set; }
    public int TotalUsuarios { get; set; }
    public int TotalReseñas { get; set; }
    public double PromedioCalificacion { get; set; }
}
