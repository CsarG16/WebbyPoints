using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics; // Necesario para ignorar el warning
using WebbyPoints.Models;

namespace WebbyPoints.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; } 
    public DbSet<PuntoInteres> PuntosInteres { get; set; }
    public DbSet<Reseña> Reseñas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Esto ignora el error de "PendingModelChangesWarning" que te bloqueaba
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Semilla de Usuarios (Tu Admin con un ID alto para evitar conflictos)
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = 100, 
                Nombre = "César Paredes",
                Email = "cesar_paredes7@usmp.pe",
                Password = "Vinicola14//", 
                Universidad = "USMP",
                Carrera = "Ingeniería de Computación y Sistemas",
                Edad = 21,
                Preferencias = "Todo",
                EsAdmin = true,
                Puntos = 999,
                Rango = "Administrador"
            },
            new Usuario
            {
                Id = 200,
                Nombre = "Fausto Miranda",
                Email = "fausto_miranda@usmp.pe",
                Password = "Mimamamemima123",
                Universidad = "USMP",
                Carrera = "Ingeniería de Computación y Sistemas",
                Edad = 21,
                Preferencias = "Todo",
                EsAdmin = true,
                Puntos = 999,
                Rango = "Administrador"
            }
        );

        // 2. Semilla de Puntos de Interés
        modelBuilder.Entity<PuntoInteres>().HasData(
            new PuntoInteres
            {
                Id = 1,
                Nombre = "Starbucks Central",
                Descripcion = "El lugar ideal para concentrarse con un buen café y WiFi estable.",
                Categoria = "Estudio",
                Ubicacion = "Av. Principal 123",
                ImagenUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93",
                PlanTipo = "Solo",
                Calificacion = 4.5,
                Horario = "07:00 AM - 10:00 PM"
            },
            new PuntoInteres
            {
                Id = 2,
                Nombre = "La Bodega de la Esquina",
                Descripcion = "Bar con ambiente relajado, perfecto para un after-class.",
                Categoria = "Ocio",
                Ubicacion = "Calle Universitaria 456",
                ImagenUrl = "/images/bodega_esquina.png",
                PlanTipo = "Amigos",
                Calificacion = 4.8,
                Horario = "04:00 PM - 02:00 AM"
            },
            new PuntoInteres
            {
                Id = 3,
                Nombre = "Mirador del Parque",
                Descripcion = "Vista increíble y ambiente tranquilo para una cita especial.",
                Categoria = "Romántico",
                Ubicacion = "Malecón de los Estudiantes",
                ImagenUrl = "https://images.unsplash.com/photo-1529619768328-e37af76c6fe5",
                PlanTipo = "Pareja",
                Calificacion = 4.9,
                Horario = "06:00 AM - 11:00 PM"
            },
            new PuntoInteres
            {
                Id = 4,
                Nombre = "Biblioteca Nacional (Sede Norte)",
                Descripcion = "Silencio absoluto y miles de libros a tu disposición.",
                Categoria = "Estudio",
                Ubicacion = "Av. Cultura 789",
                ImagenUrl = "https://images.unsplash.com/photo-1521587760476-6c12a4b040da",
                PlanTipo = "Solo",
                Calificacion = 4.2,
                Horario = "08:00 AM - 08:00 PM"
            },
            new PuntoInteres
            {
                Id = 5,
                Nombre = "Pizza & Chill",
                Descripcion = "Las mejores pizzas artesanales para compartir.",
                Categoria = "Comida",
                Ubicacion = "Pasaje Gastronómico 10",
                ImagenUrl = "https://images.unsplash.com/photo-1513104890138-7c749659a591",
                PlanTipo = "Amigos",
                Calificacion = 4.6,
                Horario = "12:00 PM - 11:00 PM"
            },
            new PuntoInteres
            {
                Id = 6,
                Nombre = "Cine Planetario",
                Descripcion = "Estrenos con descuento para universitarios.",
                Categoria = "Ocio",
                Ubicacion = "Centro Comercial El Polo",
                ImagenUrl = "https://images.unsplash.com/photo-1485846234645-a62644f84728",
                PlanTipo = "Pareja",
                Calificacion = 4.4,
                Horario = "02:00 PM - 12:00 AM"
            }
        );
    }
}