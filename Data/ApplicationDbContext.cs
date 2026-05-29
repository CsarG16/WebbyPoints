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
    public DbSet<Recompensa> Recompensas { get; set; }
    public DbSet<Canje> Canjes { get; set; }
    public DbSet<CheckIn> CheckIns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Esto ignora el error de "PendingModelChangesWarning" que te bloqueaba
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Índice Único en la columna Email
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Índice Único en CodigoVoucher para búsquedas rápidas
        modelBuilder.Entity<Canje>()
            .HasIndex(c => c.CodigoVoucher)
            .IsUnique();

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

        // 2. Semilla de Puntos de Interés (con campos de gamificación)
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
                Horario = "07:00 AM - 10:00 PM",
                EsEventoEspecial = false,
                PuntosRecompensa = 10
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
                Horario = "04:00 PM - 02:00 AM",
                EsEventoEspecial = false,
                PuntosRecompensa = 10
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
                Horario = "06:00 AM - 11:00 PM",
                EsEventoEspecial = false,
                PuntosRecompensa = 10
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
                Horario = "08:00 AM - 08:00 PM",
                EsEventoEspecial = false,
                PuntosRecompensa = 10
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
                Horario = "12:00 PM - 11:00 PM",
                EsEventoEspecial = false,
                PuntosRecompensa = 10
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
                Horario = "02:00 PM - 12:00 AM",
                EsEventoEspecial = false,
                PuntosRecompensa = 10
            },
            // --- NUEVOS: Eventos especiales con bonus de puntos ---
            new PuntoInteres
            {
                Id = 7,
                Nombre = "Limpieza de Playas - Costa Verde",
                Descripcion = "Únete a la jornada ecológica de limpieza en la Costa Verde. Contribuye al medio ambiente y gana puntos extra.",
                Categoria = "Medio Ambiente",
                Ubicacion = "Playa Costa Verde, Miraflores",
                ImagenUrl = "https://images.unsplash.com/photo-1618477460930-d8c0465e5744",
                PlanTipo = "Amigos",
                Calificacion = 4.9,
                Horario = "07:00 AM - 01:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 8,
                Nombre = "Noche de Museos - Museo de Arte de Lima",
                Descripcion = "Recorre las galerías del MALI con entrada libre para universitarios. Arte contemporáneo y clásico peruano.",
                Categoria = "Cultura",
                Ubicacion = "Parque de la Exposición, Cercado de Lima",
                ImagenUrl = "https://images.unsplash.com/photo-1554907984-15263bfd63bd",
                PlanTipo = "Pareja",
                Calificacion = 4.7,
                Horario = "06:00 PM - 11:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 9,
                Nombre = "Voluntariado - Comedor Popular San Martín",
                Descripcion = "Ayuda en la preparación y distribución de alimentos para familias vulnerables en Lima Norte.",
                Categoria = "Cívico",
                Ubicacion = "Jr. San Martín 450, Los Olivos",
                ImagenUrl = "https://images.unsplash.com/photo-1593113598332-cd288d649433",
                PlanTipo = "Amigos",
                Calificacion = 5.0,
                Horario = "08:00 AM - 02:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 10,
                Nombre = "Siembra de Árboles - Parque Ecológico",
                Descripcion = "Participa en la reforestación urbana plantando árboles nativos en el Parque Ecológico de Villa El Salvador.",
                Categoria = "Medio Ambiente",
                Ubicacion = "Parque Ecológico, Villa El Salvador",
                ImagenUrl = "https://images.unsplash.com/photo-1542601906990-b4d3fb778b09",
                PlanTipo = "Amigos",
                Calificacion = 4.8,
                Horario = "07:00 AM - 12:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            }
        );

        // 3. Semilla de Recompensas (Premios canjeables por puntos)
        modelBuilder.Entity<Recompensa>().HasData(
            new Recompensa
            {
                Id = 1,
                Nombre = "Café Gratis en Starbucks USMP",
                Descripcion = "Un café de cualquier tamaño en el Starbucks frente a la sede de Santa Anita. Válido por 7 días.",
                CostoPuntos = 50,
                Stock = 100,
                Categoria = "Descuento",
                ImagenUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93",
                Activa = true
            },
            new Recompensa
            {
                Id = 2,
                Nombre = "15% de Descuento en Pizza & Chill",
                Descripcion = "Descuento del 15% en tu cuenta total. Presentar voucher al mesero. Válido por 14 días.",
                CostoPuntos = 80,
                Stock = 50,
                Categoria = "Descuento",
                ImagenUrl = "https://images.unsplash.com/photo-1513104890138-7c749659a591",
                Activa = true
            },
            new Recompensa
            {
                Id = 3,
                Nombre = "Entrada Doble al Cine Planetario",
                Descripcion = "Dos entradas para cualquier función de lunes a jueves. Válido por 30 días.",
                CostoPuntos = 150,
                Stock = 30,
                Categoria = "Ocio",
                ImagenUrl = "https://images.unsplash.com/photo-1485846234645-a62644f84728",
                Activa = true
            },
            new Recompensa
            {
                Id = 4,
                Nombre = "Termo Coleccionable USMP",
                Descripcion = "Termo de acero inoxidable de 500ml con el logo de la USMP. Edición limitada.",
                CostoPuntos = 200,
                Stock = 20,
                Categoria = "Merchandising",
                ImagenUrl = "https://images.unsplash.com/photo-1602143407151-7111542de6e8",
                Activa = true
            },
            new Recompensa
            {
                Id = 5,
                Nombre = "Pase Libre - Parque de las Aguas",
                Descripcion = "Entrada gratuita al Circuito Mágico del Agua para ti y un acompañante. Válido fines de semana.",
                CostoPuntos = 120,
                Stock = 40,
                Categoria = "Experiencia",
                ImagenUrl = "https://images.unsplash.com/photo-1504214208698-ea1916a2195a",
                Activa = true
            },
            new Recompensa
            {
                Id = 6,
                Nombre = "Polera Oficial WebbyPoints",
                Descripcion = "Polera de algodón premium con el diseño exclusivo de WebbyPoints. Tallas S, M, L, XL.",
                CostoPuntos = 300,
                Stock = 15,
                Categoria = "Merchandising",
                ImagenUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab",
                Activa = true
            },
            new Recompensa
            {
                Id = 7,
                Nombre = "Tour Gastronómico por Barranco",
                Descripcion = "Recorrido guiado por 4 restaurantes emblemáticos de Barranco con degustación incluida.",
                CostoPuntos = 400,
                Stock = 10,
                Categoria = "Experiencia",
                ImagenUrl = "https://images.unsplash.com/photo-1414235077428-338989a2e8c0",
                Activa = true
            },
            new Recompensa
            {
                Id = 8,
                Nombre = "Entrada al Museo MALI + Guía",
                Descripcion = "Acceso VIP al Museo de Arte de Lima con guía personalizado para ti y 2 amigos.",
                CostoPuntos = 180,
                Stock = 25,
                Categoria = "Cultura",
                ImagenUrl = "https://images.unsplash.com/photo-1554907984-15263bfd63bd",
                Activa = true
            }
        );
    }
}