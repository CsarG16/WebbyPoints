using Microsoft.EntityFrameworkCore;
using WebbyPoints.Models;

namespace WebbyPoints.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // Aquí registras tus modelos como tablas
    public DbSet<PuntoInteres> PuntosInteres { get; set; }
    public DbSet<Reseña> Reseñas { get; set; }
}