using System.ComponentModel.DataAnnotations;

namespace WebbyPoints.Models;

public class PuntoInteres
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty; // "Entrenar", "Estudiar", etc.
    public string Ubicacion { get; set; } = string.Empty;
    public string? ImagenUrl { get; set; }
    
    // CAMPOS NUEVOS PARA TU DISEÑO:
    public double Calificacion { get; set; } // Ejemplo: 4.5
    public double Distancia { get; set; }    // Ejemplo: 0.5 (en km)
    public string PrecioRango { get; set; }  = string.Empty;// Ejemplo: "S/", "S/S/", "S/S/S/"
    public string Etiquetas { get; set; }    = string.Empty; // Ejemplo: "equipado,limpio,popular"
}