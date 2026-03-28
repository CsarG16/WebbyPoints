using Microsoft.AspNetCore.Mvc;
using WebbyPoints.Models.ViewModels;
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

    [HttpGet]
    public IActionResult RegistroPaso1()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegistroPaso1(RegistroViewModel model)
    {
        // IMPORTANTE: Ahora que YA pedimos estos datos, 
        // SOLO removemos la validación de las categorías (Paso 2)
        ModelState.Remove("CategoriasFavoritas");

        if (!ModelState.IsValid)
        {
            // Si las contraseñas no coinciden o falta el Email, 
            // el sistema te detendrá aquí mismo.
            return View("RegistroPaso1", model); 
        }

        return View("RegistroPaso2", model);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmarRegistro(RegistroViewModel model)
    {
        // Aquí los datos ya vienen validados y completos
        var nuevoUsuario = new Usuario
        {
            Nombre = model.Nombre,
            Email = model.Email,
            Password = model.Password, // Luego implementaremos BCrypt para la expo
            Edad = model.Edad,
            Universidad = model.Universidad,
            Carrera = model.Carrera,
            Preferencias = model.CategoriasFavoritas != null ? string.Join(",", model.CategoriasFavoritas) : ""
        };

        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
}