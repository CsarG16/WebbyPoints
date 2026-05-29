using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models;

public class Recompensa
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la recompensa es obligatorio")]
    [StringLength(150)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    // Puntos necesarios para canjear esta recompensa
    [Required]
    [Range(1, 10000, ErrorMessage = "El costo debe ser entre 1 y 10,000 puntos")]
    public int CostoPuntos { get; set; }

    // Unidades disponibles (-1 = ilimitado)
    public int Stock { get; set; } = -1;

    // "Cultura", "Descuento", "Merchandising", "Ocio", "Experiencia"
    public string Categoria { get; set; } = string.Empty;

    public string? ImagenUrl { get; set; }

    // Si la recompensa está activa y visible en la tienda
    public bool Activa { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // --- RELACIÓN: Una recompensa puede tener muchos canjes ---
    public List<Canje> Canjes { get; set; } = new List<Canje>();
}
