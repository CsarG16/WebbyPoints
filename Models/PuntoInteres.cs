using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models;

public class PuntoInteres
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    public string Categoria { get; set; } = string.Empty; 

    public string Ubicacion { get; set; } = string.Empty;

    public string? ImagenUrl { get; set; }
    
    [Required]
    public string PlanTipo { get; set; } = string.Empty; // "Solo", "Pareja", "Amigos"
    
    // Usamos 0.0 como valor inicial para evitar errores de null
    public double Calificacion { get; set; } = 0.0; 
    public double Distancia { get; set; } = 0.0;
    
    public string PrecioRango { get; set; } = "S/"; 
    public string Etiquetas { get; set; } = string.Empty;

    // Horario de atención (ej: "Lun-Vie: 8am - 10pm")
    public string Horario { get; set; } = "9:00 AM - 10:00 PM";

    // --- GAMIFICACIÓN: Eventos especiales (culturales, ambientales, cívicos) ---

    // Indica si este lugar/evento es de impacto positivo (cultura, ecología, civismo)
    public bool EsEventoEspecial { get; set; } = false;

    // Puntos extra que otorga al dejar reseña (default 10, especiales 25)
    public int PuntosRecompensa { get; set; } = 10;

    // --- RELACIÓN: Un punto puede tener muchas reseñas ---
    public List<Reseña> Reseñas { get; set; } = new List<Reseña>();

    // --- RELACIÓN: Un punto puede tener muchos check-ins ---
    public List<CheckIn> CheckIns { get; set; } = new List<CheckIn>();
}