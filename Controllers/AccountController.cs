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
    // AJAX: Login
    // =====================================================
    [HttpPost]
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

        var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == model.Email);
        if (emailExiste)
            return Json(new { success = false, errors = new[] { "Este correo ya está registrado." } });

        var nuevoUsuario = new Usuario
        {
            Nombre       = model.Nombre,
            Email        = model.Email,
            Password     = model.Password,
            Edad         = model.Edad,
            Universidad  = model.Universidad,
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
    // LOGOUT
    // =====================================================
    public async Task<IActionResult> Logout()
    {
        await HttpContext.Session.LoadAsync();
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}