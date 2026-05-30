using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebbyPoints.Data;
using WebbyPoints.Models;
using Microsoft.Extensions.Caching.Distributed;
using WebbyPoints.Helpers;

namespace WebbyPoints.Controllers;

public class RecompensasController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;

    public RecompensasController(ApplicationDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    // =====================================================
    // TIENDA DE RECOMPENSAS: Catálogo de premios canjeables
    // =====================================================
    public async Task<IActionResult> Index(string? categoria)
    {
        await HttpContext.Session.LoadAsync();
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        // Obtener puntos del usuario actual (si está logueado)
        int puntosUsuario = 0;
        if (usuarioId.HasValue)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId.Value);
            if (usuario != null)
                puntosUsuario = usuario.Puntos;
        }

        ViewBag.PuntosUsuario = puntosUsuario;
        ViewBag.UsuarioLogueado = usuarioId.HasValue;
        ViewBag.CategoriaActual = categoria ?? "Todas";

        // ==========================================================
        // REDIS CACHE: Catálogo de recompensas cacheado por categoría
        // Cada categoría tiene su propia clave en Redis
        // ==========================================================
        var cacheKey = $"recompensas:catalogo:{categoria ?? "todas"}";
        var recompensas = await RedisCacheHelper.GetOrSetAsync(
            _cache,
            cacheKey,
            async () =>
            {
                var query = _context.Recompensas
                    .Where(r => r.Activa)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(categoria) && categoria != "Todas")
                {
                    query = query.Where(r => r.Categoria == categoria);
                }

                return await query
                    .OrderBy(r => r.CostoPuntos)
                    .ToListAsync();
            },
            absoluteExpiration: TimeSpan.FromMinutes(5)
        );

        // ==========================================================
        // REDIS CACHE: Categorías disponibles cacheadas por 10 min
        // ==========================================================
        var categorias = await RedisCacheHelper.GetOrSetAsync(
            _cache,
            "recompensas:categorias",
            async () => await _context.Recompensas
                .Where(r => r.Activa)
                .Select(r => r.Categoria)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync(),
            absoluteExpiration: TimeSpan.FromMinutes(10)
        );

        ViewBag.Categorias = categorias ?? new List<string>();

        return View(recompensas ?? new List<Recompensa>());
    }

    // =====================================================
    // CANJEAR RECOMPENSA: POST vía AJAX
    // =====================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Canjear(int recompensaId)
    {
        await HttpContext.Session.LoadAsync();
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        if (usuarioId == null)
            return Json(new { success = false, error = "Debes iniciar sesión para canjear." });

        var usuario = await _context.Usuarios.FindAsync(usuarioId.Value);
        if (usuario == null)
            return Json(new { success = false, error = "Usuario no encontrado." });

        var recompensa = await _context.Recompensas.FindAsync(recompensaId);
        if (recompensa == null)
            return Json(new { success = false, error = "Recompensa no encontrada." });

        if (!recompensa.Activa)
            return Json(new { success = false, error = "Esta recompensa ya no está disponible." });

        if (recompensa.Stock == 0)
            return Json(new { success = false, error = "¡Lo sentimos! Esta recompensa se ha agotado." });

        if (usuario.Puntos < recompensa.CostoPuntos)
            return Json(new { success = false, error = $"No tienes suficientes puntos. Necesitas {recompensa.CostoPuntos} y tienes {usuario.Puntos}." });

        // Generar código voucher único
        var codigoVoucher = GenerarCodigoVoucher();

        // Crear el canje
        var canje = new Canje
        {
            UsuarioId = usuario.Id,
            RecompensaId = recompensa.Id,
            FechaCanje = DateTime.Now,
            CodigoVoucher = codigoVoucher,
            Estado = "Disponible",
            PuntosGastados = recompensa.CostoPuntos
        };

        // Descontar puntos del usuario
        usuario.Puntos -= recompensa.CostoPuntos;

        // Actualizar rango según puntos restantes
        usuario.Rango = usuario.Puntos switch
        {
            >= 500 => "Leyenda",
            >= 200 => "Experto",
            >= 100 => "Local",
            >= 30  => "Explorador",
            _      => "Novato"
        };

        // Descontar stock (si no es ilimitado)
        if (recompensa.Stock > 0)
            recompensa.Stock--;

        _context.Canjes.Add(canje);
        await _context.SaveChangesAsync();

        // ==========================================================
        // REDIS CACHE: Invalidar catálogo porque cambió el stock
        // ==========================================================
        await RedisCacheHelper.InvalidateMultipleAsync(_cache,
            $"recompensas:catalogo:{recompensa.Categoria}",
            "recompensas:catalogo:todas",
            "home:recompensas_populares"
        );

        // Actualizar puntos en la sesión
        HttpContext.Session.SetInt32("UsuarioPuntos", usuario.Puntos);

        return Json(new
        {
            success = true,
            codigoVoucher = codigoVoucher,
            puntosRestantes = usuario.Puntos,
            mensaje = $"¡Canje exitoso! Tu código es: {codigoVoucher}"
        });
    }

    // =====================================================
    // MIS CANJES: Historial de vouchers del usuario
    // =====================================================
    public async Task<IActionResult> MisCanjes()
    {
        await HttpContext.Session.LoadAsync();
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        if (usuarioId == null)
            return RedirectToAction("Login", "Account");

        var canjes = await _context.Canjes
            .Include(c => c.Recompensa)
            .Where(c => c.UsuarioId == usuarioId.Value)
            .OrderByDescending(c => c.FechaCanje)
            .ToListAsync();

        var usuario = await _context.Usuarios.FindAsync(usuarioId.Value);
        ViewBag.PuntosUsuario = usuario?.Puntos ?? 0;

        return View(canjes);
    }

    // =====================================================
    // HELPER: Generar código voucher único
    // =====================================================
    private string GenerarCodigoVoucher()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var codigo = new string(Enumerable.Range(0, 6)
            .Select(_ => chars[random.Next(chars.Length)])
            .ToArray());
        return $"USMP-{codigo}";
    }
}
