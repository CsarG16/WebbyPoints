using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models;

public class Reseña
{
    public int Id { get; set; }
    
    public string Comentario { get; set; } = string.Empty;
    
    [Range(1, 5)]
    public int Calificacion { get; set; } // Estrellas del 1 al 5

    // RELACIÓN: Esto indica a qué punto pertenece la reseña
    public int PuntoInteresId { get; set; }
    public PuntoInteres? PuntoInteres { get; set; }
}