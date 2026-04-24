using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models;

public class Usuario
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress(ErrorMessage = "Formato de correo inválido")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty; // Recuerda luego usar BCrypt para encriptar
    
    public string Universidad { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;
    public int Edad { get; set; }
    
    // Aquí guardamos los intereses seleccionados en el Registro Paso 2 (ej: "Gym, Café, Bares")
    public string Preferencias { get; set; } = string.Empty;

    // --- CAMPOS DE ESCALABILIDAD ---

    // Foto de perfil del estudiante
    public string? FotoUrl { get; set; }

    // Fecha en que se unió (útil para ver la antigüedad del usuario)
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    // --- GAMIFICACIÓN ---

    // Puntos acumulados por reseñas, fotos, etc.
    public int Puntos { get; set; } = 0;

    // Rango según los puntos: "Explorador", "Local", "Experto", "Leyenda"
    public string Rango { get; set; } = "Novato";

    // --- RELACIONES (Propiedades de Navegación) ---

    // Un usuario puede escribir muchas reseñas
    public List<Reseña> Reseñas { get; set; } = new List<Reseña>();

    public bool EsAdmin { get; set; } = false;
}