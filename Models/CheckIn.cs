using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models;

public class CheckIn
{
    public int Id { get; set; }

    [Required]
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    [Required]
    public int PuntoInteresId { get; set; }
    public PuntoInteres? PuntoInteres { get; set; }

    public DateTime FechaCheckIn { get; set; } = DateTime.Now;

    // Código único de asistencia para que los coordinadores de la USMP puedan validarlo
    [Required]
    [StringLength(50)]
    public string CodigoAsistencia { get; set; } = string.Empty;

    // Puntos otorgados en este Check-In
    public int PuntosOtorgados { get; set; }
}
