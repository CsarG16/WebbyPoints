using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models;

public class Reseña
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El comentario no puede estar vacío")]
    [StringLength(500, ErrorMessage = "El comentario es muy largo (máximo 500 caracteres)")]
    public string Comentario { get; set; } = string.Empty;
    
    [Range(1, 5, ErrorMessage = "La calificación debe ser entre 1 y 5 estrellas")]
    public int Calificacion { get; set; } 

    public DateTime FechaPublicacion { get; set; } = DateTime.Now;

    public int PuntoInteresId { get; set; }
    public PuntoInteres? PuntoInteres { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}