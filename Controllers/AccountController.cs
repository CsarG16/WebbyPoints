using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebbyPoints.Data;
using WebbyPoints.Models;

namespace WebbyPoints.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // =====================================================
    // AUTH UNIFICADO - Página con animaciones
    // =====================================================
    [HttpGet]
    public IActionResult RegistroPaso1()
    {
        ViewData["AuthMode"] = "register";
        return View("Auth");
    }

    [HttpGet]
    public IActionResult Login()
    {
        ViewData["AuthMode"] = "login";
        return View("Auth");
    }

    // =====================================================
    // =====================================================
    // AJAX: Login
    // =====================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAjax([FromForm] string Email, [FromForm] string Password)
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            return Json(new { success = false, errors = new[] { "Completa todos los campos." } });

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);

        if (usuario == null)
            return Json(new { success = false, errors = new[] { "Correo o contraseña incorrectos." } });

        await HttpContext.Session.LoadAsync();
        HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
        HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
        HttpContext.Session.SetInt32("EsAdmin", usuario.EsAdmin ? 1 : 0);

        return Json(new { success = true, redirect = "/" });
    }

    // =====================================================
    // AJAX: Registro completo (Paso 1 + Paso 2)
    // =====================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegistroAjax([FromForm] RegistroViewModel model)
    {
        ModelState.Remove("CategoriasFavoritas");

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return Json(new { success = false, errors });
        }

        var emailLower = model.Email.Trim().ToLower();
        if (!emailLower.EndsWith("@usmp.pe"))
        {
            return Json(new { success = false, errors = new[] { "Debes registrarte con un correo institucional de la USMP (@usmp.pe)." } });
        }

        if (model.Universidad.Trim().ToUpper() != "USMP")
        {
            return Json(new { success = false, errors = new[] { "Esta plataforma es de uso exclusivo para miembros de la USMP." } });
        }

        var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == emailLower);
        if (emailExiste)
            return Json(new { success = false, errors = new[] { "Este correo ya está registrado." } });

        var nuevoUsuario = new Usuario
        {
            Nombre       = model.Nombre,
            Email        = emailLower,
            Password     = model.Password, // Guardamos la contraseña en texto plano
            Edad         = model.Edad,
            Universidad  = "USMP",
            Carrera      = model.Carrera,
            Preferencias = model.CategoriasFavoritas != null
                           ? string.Join(",", model.CategoriasFavoritas)
                           : string.Empty,
            Puntos        = 10,
            Rango         = "Explorador",
            FechaRegistro = DateTime.Now
        };

        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        await HttpContext.Session.LoadAsync();
        HttpContext.Session.SetInt32("UsuarioId", nuevoUsuario.Id);
        HttpContext.Session.SetString("UsuarioNombre", nuevoUsuario.Nombre);

        return Json(new { success = true, redirect = "/" });
    }

    // =====================================================
    // PERFIL DEL ESTUDIANTE: Vista premium con estadísticas
    // =====================================================
    public async Task<IActionResult> Perfil()
    {
        await HttpContext.Session.LoadAsync();
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        if (usuarioId == null)
            return RedirectToAction("Login", "Account");

        var usuario = await _context.Usuarios
            .Include(u => u.Reseñas)
                .ThenInclude(r => r.PuntoInteres)
            .Include(u => u.Canjes)
                .ThenInclude(c => c.Recompensa)
            .Include(u => u.CheckIns)
                .ThenInclude(c => c.PuntoInteres)
            .FirstOrDefaultAsync(u => u.Id == usuarioId.Value);

        if (usuario == null)
            return RedirectToAction("Login", "Account");

        // Calcular estadísticas del perfil
        ViewBag.TotalReseñas = usuario.Reseñas.Count;
        ViewBag.TotalCanjes = usuario.Canjes.Count;
        ViewBag.TotalCheckIns = usuario.CheckIns.Count;
        ViewBag.PuntosGastados = usuario.Canjes.Sum(c => c.PuntosGastados);

        // Progreso al siguiente rango
        var (rangoActual, puntosSiguiente, nombreSiguiente) = usuario.Puntos switch
        {
            >= 500 => ("Leyenda", 999, "¡Máximo nivel!"),
            >= 200 => ("Experto", 500, "Leyenda"),
            >= 100 => ("Local", 200, "Experto"),
            >= 30  => ("Explorador", 100, "Local"),
            _      => ("Novato", 30, "Explorador")
        };

        ViewBag.RangoActual = rangoActual;
        ViewBag.PuntosSiguienteRango = puntosSiguiente;
        ViewBag.NombreSiguienteRango = nombreSiguiente;

        // Porcentaje de progreso al siguiente rango
        var puntosBase = rangoActual switch
        {
            "Leyenda"    => 500,
            "Experto"    => 200,
            "Local"      => 100,
            "Explorador" => 30,
            _            => 0
        };
        var rango = puntosSiguiente - puntosBase;
        var progreso = rango > 0 ? (int)((double)(usuario.Puntos - puntosBase) / rango * 100) : 100;
        ViewBag.ProgresoRango = Math.Min(progreso, 100);

        // Canjes activos (disponibles)
        ViewBag.CanjesActivos = usuario.Canjes
            .Where(c => c.Estado == "Disponible")
            .OrderByDescending(c => c.FechaCanje)
            .ToList();

        // Últimas reseñas
        ViewBag.UltimasReseñas = usuario.Reseñas
            .OrderByDescending(r => r.FechaPublicacion)
            .Take(5)
            .ToList();

        // Últimos Check-Ins
        ViewBag.UltimosCheckIns = usuario.CheckIns
            .OrderByDescending(c => c.FechaCheckIn)
            .Take(5)
            .ToList();

        return View(usuario);
    }

    // =====================================================
    // LOGOUT
    // =====================================================
    public async Task<IActionResult> Logout()
    {
        await HttpContext.Session.LoadAsync();
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}