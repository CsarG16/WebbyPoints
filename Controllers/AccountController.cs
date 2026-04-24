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
    // REGISTRO - PASO 1: Captura datos personales
    // =====================================================
    [HttpGet]
    public IActionResult RegistroPaso1()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegistroPaso1(RegistroViewModel model)
    {
        // No validamos CategoriasFavoritas aquí, eso es del Paso 2
        ModelState.Remove("CategoriasFavoritas");

        if (!ModelState.IsValid)
        {
            return View("RegistroPaso1", model);
        }

        return View("RegistroPaso2", model);
    }

    // =====================================================
    // REGISTRO - PASO 2: Confirma y guarda en DB
    // =====================================================
    [HttpPost]
    public async Task<IActionResult> ConfirmarRegistro(RegistroViewModel model)
    {
        // Verificar si el correo ya está en uso
        var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == model.Email);
        if (emailExiste)
        {
            ModelState.AddModelError("Email", "Este correo ya está registrado.");
            return View("RegistroPaso1", model);
        }

        var nuevoUsuario = new Usuario
        {
            Nombre      = model.Nombre,
            Email       = model.Email,
            Password    = model.Password, // TODO: reemplazar con BCrypt en la expo
            Edad        = model.Edad,
            Universidad = model.Universidad,
            Carrera     = model.Carrera,
            Preferencias = model.CategoriasFavoritas != null
                           ? string.Join(",", model.CategoriasFavoritas)
                           : string.Empty,
            Puntos        = 0,
            Rango         = "Explorador",
            FechaRegistro = DateTime.Now
        };

        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        // Sesión simple para simular autenticación
        HttpContext.Session.SetInt32("UsuarioId", nuevoUsuario.Id);
        HttpContext.Session.SetString("UsuarioNombre", nuevoUsuario.Nombre);

        return RedirectToAction("Index", "Home");
    }

    // =====================================================
    // LOGIN - GET: Muestra el formulario
    // =====================================================
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // =====================================================
    // LOGIN - POST: Valida credenciales
    // =====================================================
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Buscar usuario por email y password (texto plano por ahora)
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

        if (usuario == null)
        {
            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            return View(model);
        }

        // Guardar sesión
        HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
        HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);

        return RedirectToAction("Index", "Home");
    }

    // =====================================================
    // LOGOUT: Cierra sesión
    // =====================================================
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}