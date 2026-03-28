using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models.ViewModels;

public class RegistroViewModel
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de correo inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Range(16, 99, ErrorMessage = "Debes ser mayor de 16 años")]
    public int Edad { get; set; }

    [Required(ErrorMessage = "Indica tu universidad")]
    public string Universidad { get; set; } = string.Empty;

    [Required(ErrorMessage = "Indica tu carrera")]
    public string Carrera { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    [DataType(DataType.Password)]
    public string ConfirmarContraseña { get; set; } = string.Empty;
    
    public List<string> CategoriasFavoritas { get; set; } = new();
}