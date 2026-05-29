using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models;

public class Canje
{
    public int Id { get; set; }

    // --- RELACIÓN: Qué usuario realizó el canje ---
    [Required]
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    // --- RELACIÓN: Qué recompensa se canjeó ---
    [Required]
    public int RecompensaId { get; set; }
    public Recompensa? Recompensa { get; set; }

    // Fecha en que se realizó el canje
    public DateTime FechaCanje { get; set; } = DateTime.Now;

    // Código único tipo voucher que el estudiante presenta (ej: "USMP-8A3B9C")
    [Required]
    [StringLength(20)]
    public string CodigoVoucher { get; set; } = string.Empty;

    // "Disponible" = aún no lo reclama, "Reclamado" = ya lo usó, "Expirado" = venció
    [Required]
    public string Estado { get; set; } = "Disponible";

    // Puntos que costó en el momento del canje (snapshot histórico)
    public int PuntosGastados { get; set; }
}
