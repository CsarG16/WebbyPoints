using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models;

public class Usuario
{
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Universidad { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Preferencias { get; set; } = string.Empty; 
}