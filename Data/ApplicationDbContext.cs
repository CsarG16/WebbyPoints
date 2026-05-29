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
                Carrera = "Ingenieria de Sistemas",
                Edad = 21,
                Preferencias = "Estudio, Ocio, Comida, Cultura",
                EsAdmin = true,
                Puntos = 2000,
                Rango = "Administrador"
            },
            new Usuario
            {
                Id = 200,
                Nombre = "Fausto Miranda",
                Email = "fausto_miranda@usmp.pe",
                Password = "Mimamamemima123",
                Universidad = "USMP",
                Carrera = "Ingenieria de Sistemas",
                Edad = 21,
                Preferencias = "Estudio, Ocio, Comida, Cultura",
                EsAdmin = true,
                Puntos = 1000,
                Rango = "Administrador"
            },
            new Usuario
            {
                Id = 300,
                Nombre = "Juan Pérez",
                Email = "juan_perez@usmp.pe",
                Password = "Juanito123//",
                Universidad = "USMP",
                Carrera = "Medicina",
                Edad = 20,
                Preferencias = "Comida, Parques",
                EsAdmin = false,
                Puntos = 500,
                Rango = "Explorador"
            },
            new Usuario
            {
                Id = 301,
                Nombre = "María Rojas",
                Email = "maria_rojas@usmp.pe",
                Password = "Maria1234//",
                Universidad = "USMP",
                Carrera = "Derecho",
                Edad = 22,
                Preferencias = "Cafés, Estudiar",
                EsAdmin = false,
                Puntos = 500,
                Rango = "Explorador"
            },
            new Usuario
            {
                Id = 302,
                Nombre = "Luis Flores",
                Email = "luis_flores@usmp.pe",
                Password = "Luisito99//",
                Universidad = "USMP",
                Carrera = "Psicologia",
                Edad = 19,
                Preferencias = "Gaming, Bares",
                EsAdmin = false,
                Puntos = 500,
                Rango = "Explorador"
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
            },
            // --- NUEVOS LUGARES (11 AL 40): REALES Y TEMÁTICOS ---
            new PuntoInteres
            {
                Id = 11,
                Nombre = "Feria Internacional del Libro de Lima",
                Descripcion = "La feria literaria más importante del país. Cientos de stands, presentaciones de libros y conversatorios culturales.",
                Categoria = "Cultura",
                Ubicacion = "Parque Próceres de la Independencia, Jesús María",
                ImagenUrl = "https://images.unsplash.com/photo-1481627834876-b7833e8f5570",
                PlanTipo = "Amigos",
                Calificacion = 4.7,
                Horario = "11:00 AM - 10:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 12,
                Nombre = "Central Restaurante",
                Descripcion = "Experiencia culinaria inigualable a cargo del chef Virgilio Martínez. Platos basados en insumos de diversas alturas geográficas.",
                Categoria = "Comida",
                Ubicacion = "Av. Pedro de Osma 301, Barranco",
                ImagenUrl = "https://images.unsplash.com/photo-1544025162-d76694265947",
                PlanTipo = "Pareja",
                Calificacion = 4.9,
                Horario = "12:45 PM - 11:00 PM",
                PrecioRango = "S/S/S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 13,
                Nombre = "La Lucha Sanguchería Criolla",
                Descripcion = "Los mejores sándwiches criollos tradicionales con papas nativas fritas y jugos naturales. Un sabor inconfundible.",
                Categoria = "Comida",
                Ubicacion = "Pasaje Champagnat 139, Miraflores",
                ImagenUrl = "https://images.unsplash.com/photo-1509722747041-616f39b57569",
                PlanTipo = "Solo",
                Calificacion = 4.6,
                Horario = "07:00 AM - 01:00 AM",
                PrecioRango = "S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 14,
                Nombre = "Bar Maury (Centro de Lima)",
                Descripcion = "Histórico bar limeño, reconocido como el lugar donde se perfeccionó la receta original del emblemático Pisco Sour.",
                Categoria = "Ocio",
                Ubicacion = "Jr. Carabaya 387, Centro de Lima",
                ImagenUrl = "https://images.unsplash.com/photo-1514362545857-3bc16c4c7d1b",
                PlanTipo = "Amigos",
                Calificacion = 4.5,
                Horario = "12:00 PM - 10:00 PM",
                PrecioRango = "S/S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 15,
                Nombre = "Festival de Anime & Cosplay - Otaku Fest",
                Descripcion = "El gran evento de cultura pop asiática en Lima. Concursos de cosplay, bandas en vivo, venta de merchandising y videojuegos.",
                Categoria = "Cultural",
                Ubicacion = "Centro de Convenciones de Lima, San Borja",
                ImagenUrl = "https://images.unsplash.com/photo-1578632767115-351597cf2477",
                PlanTipo = "Amigos",
                Calificacion = 4.8,
                Horario = "10:00 AM - 09:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 16,
                Nombre = "Voluntariado - Albergue 'Patitas Felices'",
                Descripcion = "Ayuda en el cuidado, alimentación y paseo de perros y gatos rescatados del abandono en este hermoso refugio animal.",
                Categoria = "Medio Ambiente",
                Ubicacion = "Calle Las Flores 450, Chorrillos",
                ImagenUrl = "https://images.unsplash.com/photo-1583511655857-d19b40a7a54e",
                PlanTipo = "Amigos",
                Calificacion = 4.9,
                Horario = "09:00 AM - 04:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 17,
                Nombre = "Apoyo al Adulto Mayor - Asilo Canevaro",
                Descripcion = "Comparte una tarde de juegos, lectura y conversación con personas de la tercera edad que necesitan compañía y afecto.",
                Categoria = "Cívico",
                Ubicacion = "Jr. Áncash 1400, Barrios Altos, Centro de Lima",
                ImagenUrl = "https://images.unsplash.com/photo-1576765608535-5f04d1e3f289",
                PlanTipo = "Solo",
                Calificacion = 5.0,
                Horario = "02:00 PM - 06:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 18,
                Nombre = "Astrid & Gastón",
                Descripcion = "Alta cocina peruana de autor en una hermosa casona histórica de San Isidro. Platos criollos reinterpretados de manera gourmet.",
                Categoria = "Comida",
                Ubicacion = "Av. Paz Soldán 290, San Isidro",
                ImagenUrl = "https://images.unsplash.com/photo-1414235077428-338989a2e8c0",
                PlanTipo = "Pareja",
                Calificacion = 4.7,
                Horario = "12:00 PM - 11:30 PM",
                PrecioRango = "S/S/S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 19,
                Nombre = "Club de Lectura - Biblioteca Municipal",
                Descripcion = "Discusión mensual de obras contemporáneas peruanas y latinoamericanas en un ambiente ameno y rodeado de libros.",
                Categoria = "Estudio",
                Ubicacion = "Av. Larco 770, Miraflores",
                ImagenUrl = "https://images.unsplash.com/photo-1529156069898-49953e39b3ac",
                PlanTipo = "Solo",
                Calificacion = 4.4,
                Horario = "04:00 PM - 07:00 PM",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 20,
                Nombre = "Tarde de Juegos de Mesa - Portal de Ocio",
                Descripcion = "Ven a jugar Catán, Dixit o Carcassonne con amigos o conoce gente nueva compartiendo juegos modernos de mesa.",
                Categoria = "Ocio",
                Ubicacion = "Calle Cantuarias 140, Miraflores",
                ImagenUrl = "https://images.unsplash.com/photo-1610890716171-6b1bb98ffd09",
                PlanTipo = "Amigos",
                Calificacion = 4.6,
                Horario = "02:00 PM - 10:00 PM",
                PrecioRango = "S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 21,
                Nombre = "Karaoke Sopranos Surco",
                Descripcion = "Box privados para cantar a todo pulmón con una enorme selección de canciones y excelente servicio de coctelería y snacks.",
                Categoria = "Ocio",
                Ubicacion = "Av. Primavera 1540, Santiago de Surco",
                ImagenUrl = "https://images.unsplash.com/photo-1516280440614-37939bbacd6a",
                PlanTipo = "Amigos",
                Calificacion = 4.5,
                Horario = "06:00 PM - 03:00 AM",
                PrecioRango = "S/S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 22,
                Nombre = "Isolina Taberna Peruana",
                Descripcion = "Platos de antaño servidos en generosas porciones ideales para compartir en familia o con esa persona especial. El sabor de casa.",
                Categoria = "Comida",
                Ubicacion = "Av. San Martín 101, Barranco",
                ImagenUrl = "https://images.unsplash.com/photo-1555396273-367ea4eb4db5",
                PlanTipo = "Pareja",
                Calificacion = 4.8,
                Horario = "12:00 PM - 10:00 PM",
                PrecioRango = "S/S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 23,
                Nombre = "CicloDía - Av. Arequipa Libre",
                Descripcion = "El domingo por la mañana la Av. Arequipa se cierra al tráfico motorizado y se abre completamente para bicicletas, patines y caminatas.",
                Categoria = "Deporte",
                Ubicacion = "Av. Arequipa (Cdra 1 a 52), Lima",
                ImagenUrl = "https://images.unsplash.com/photo-1541614101331-1a5a3a194e92",
                PlanTipo = "Amigos",
                Calificacion = 4.7,
                Horario = "07:00 AM - 01:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 24,
                Nombre = "Running Club - El Pentagonito",
                Descripcion = "Únete a grupos de corredores para recorrer el perímetro verde de la comandancia militar. Excelente ruta iluminada y segura.",
                Categoria = "Deporte",
                Ubicacion = "Av. San Borja Sur, San Borja",
                ImagenUrl = "https://images.unsplash.com/photo-1476480862126-209bfaa8edc8",
                PlanTipo = "Solo",
                Calificacion = 4.6,
                Horario = "05:00 AM - 10:00 PM",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 25,
                Nombre = "Clases de Surf en Playa Makaha",
                Descripcion = "Aprende a domar las olas con profesores calificados en la Costa Verde. Incluye tabla de surf y traje de neopreno.",
                Categoria = "Deporte",
                Ubicacion = "Playa Makaha, Miraflores",
                ImagenUrl = "https://images.unsplash.com/photo-1502680390469-be75c86b636f",
                PlanTipo = "Solo",
                Calificacion = 4.8,
                Horario = "06:00 AM - 05:00 PM",
                PrecioRango = "S/S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 26,
                Nombre = "Circuito Mágico del Agua",
                Descripcion = "Parque de fuentes ornamentales con espectáculos multimedia interactivos de agua, luces y música. Un paseo romántico perfecto.",
                Categoria = "Romántico",
                Ubicacion = "Jr. Madre de Dios S/N, Cercado de Lima",
                ImagenUrl = "https://images.unsplash.com/photo-1504214208698-ea1916a2195a",
                PlanTipo = "Pareja",
                Calificacion = 4.8,
                Horario = "03:00 PM - 10:00 PM",
                PrecioRango = "S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 27,
                Nombre = "El Parque del Amor",
                Descripcion = "Vista hermosa de la bahía de Lima, murales de mosaicos coloridos y la icónica escultura de 'El Beso' de Víctor Delfín.",
                Categoria = "Romántico",
                Ubicacion = "Malecón Cisneros, Miraflores",
                ImagenUrl = "https://images.unsplash.com/photo-1518199266791-5375a83190b7",
                PlanTipo = "Pareja",
                Calificacion = 4.9,
                Horario = "24 Horas",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 28,
                Nombre = "Café Cultural La Libre",
                Descripcion = "Una hermosa y pequeña cafetería-librería en Barranco donde puedes leer en silencio mientras disfrutas de un té artesanal.",
                Categoria = "Estudio",
                Ubicacion = "Av. San Martín 108, Barranco",
                ImagenUrl = "https://images.unsplash.com/photo-1507842217343-583bb7270b66",
                PlanTipo = "Solo",
                Calificacion = 4.7,
                Horario = "09:00 AM - 09:00 PM",
                PrecioRango = "S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 29,
                Nombre = "Biblioteca de la USMP (Sede La Molina)",
                Descripcion = "Estudio académico con amplias salas de lectura silenciosa, computadoras de consulta y bibliotecología especializada.",
                Categoria = "Estudio",
                Ubicacion = "Av. Los Corregidores 1150, La Molina",
                ImagenUrl = "https://images.unsplash.com/photo-1521587760476-6c12a4b040da",
                PlanTipo = "Solo",
                Calificacion = 4.5,
                Horario = "08:00 AM - 09:00 PM",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 30,
                Nombre = "Starbucks USMP (Sede Santa Anita)",
                Descripcion = "Ubicación estratégica frente al campus universitario para hacer trabajos grupales, estudiar para parciales o tomar un espresso.",
                Categoria = "Estudio",
                Ubicacion = "Av. Los Calamacos 450, Santa Anita",
                ImagenUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93",
                PlanTipo = "Solo",
                Calificacion = 4.3,
                Horario = "07:00 AM - 10:00 PM",
                PrecioRango = "S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 31,
                Nombre = "Festival Mistura USMP (Feria Gastronómica)",
                Descripcion = "Feria gastronómica dentro del campus universitario. Expositores de comida selvática, marina y andina a precios de estudiante.",
                Categoria = "Comida",
                Ubicacion = "Parque de la Exposición, Lima",
                ImagenUrl = "https://images.unsplash.com/photo-1504674900247-0877df9cc836",
                PlanTipo = "Amigos",
                Calificacion = 4.9,
                Horario = "10:00 AM - 06:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 32,
                Nombre = "Museo Larco",
                Descripcion = "Espectacular casona virreinal que alberga una de las colecciones precolombinas de oro y plata más impresionantes del mundo.",
                Categoria = "Cultura",
                Ubicacion = "Av. Simón Bolívar 1515, Pueblo Libre",
                ImagenUrl = "https://images.unsplash.com/photo-1566121318599-8cf0c7921a8f",
                PlanTipo = "Solo",
                Calificacion = 4.8,
                Horario = "09:00 AM - 07:00 PM",
                PrecioRango = "S/S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 33,
                Nombre = "Gran Teatro Nacional (Sinfónica)",
                Descripcion = "Disfruta de las mejores temporadas de ópera, ballet y conciertos de la Orquesta Sinfónica Nacional con acústica de primer nivel mundial.",
                Categoria = "Cultura",
                Ubicacion = "Av. Javier Prado Este 2225, San Borja",
                ImagenUrl = "https://images.unsplash.com/photo-1503095396549-807759245b35",
                PlanTipo = "Pareja",
                Calificacion = 4.9,
                Horario = "07:30 PM - 10:00 PM",
                PrecioRango = "S/S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 34,
                Nombre = "Donación de Sangre - Hospital Rebagliati",
                Descripcion = "Sé un héroe cívico donando sangre para el banco nacional. Un acto altruista que salva hasta tres vidas humanas.",
                Categoria = "Cívico",
                Ubicacion = "Av. Edgardo Rebagliati 490, Jesús María",
                ImagenUrl = "https://images.unsplash.com/photo-1615461066841-6116e61058f4",
                PlanTipo = "Solo",
                Calificacion = 5.0,
                Horario = "07:00 AM - 02:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 35,
                Nombre = "Reforestación - Lomas de Villa María",
                Descripcion = "Sube las hermosas lomas costeras durante el invierno limeño para plantar semillas de flora nativa y proteger el ecosistema de la neblina.",
                Categoria = "Medio Ambiente",
                Ubicacion = "Lomas de Villa Maria del Triunfo, VMT",
                ImagenUrl = "https://images.unsplash.com/photo-1500382017468-9049fed747ef",
                PlanTipo = "Amigos",
                Calificacion = 4.8,
                Horario = "07:00 AM - 01:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 36,
                Nombre = "Limpieza Extrema - Playa Carpayo",
                Descripcion = "Jornada cívica y ecológica para recolectar desechos plásticos en una de las playas más contaminadas del litoral del Callao.",
                Categoria = "Medio Ambiente",
                Ubicacion = "Playa Carpayo, Chucuito, Callao",
                ImagenUrl = "https://images.unsplash.com/photo-1618477460930-d8c0465e5744",
                PlanTipo = "Amigos",
                Calificacion = 4.9,
                Horario = "08:00 AM - 02:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 37,
                Nombre = "Bar Ayahuasca Barranco",
                Descripcion = "Impresionante bar de tragos de autor ubicado en una mansión restaurada del siglo XIX. Hermosa decoración barroca criolla.",
                Categoria = "Ocio",
                Ubicacion = "Av. Prolongación San Martin 130, Barranco",
                ImagenUrl = "https://images.unsplash.com/photo-1470337458703-46ad1756a187",
                PlanTipo = "Amigos",
                Calificacion = 4.7,
                Horario = "06:00 PM - 02:00 AM",
                PrecioRango = "S/S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 38,
                Nombre = "Huaca Pucllana (Cena Temática)",
                Descripcion = "Disfruta de una cena espectacular a la luz de las velas frente al impresionante centro ceremonial precolombino iluminado.",
                Categoria = "Romántico",
                Ubicacion = "Calle General Borgoño 8ra cdra, Miraflores",
                ImagenUrl = "https://images.unsplash.com/photo-1504674900247-0877df9cc836",
                PlanTipo = "Pareja",
                Calificacion = 4.9,
                Horario = "07:00 PM - 11:30 PM",
                PrecioRango = "S/S/S/",
                EsEventoEspecial = false
            },
            new PuntoInteres
            {
                Id = 39,
                Nombre = "Comic Con Lima",
                Descripcion = "La convención de cultura pop geek más grande de Lima. Stands de Marvel, DC, ilustradores, actores internacionales invitados y cosplay.",
                Categoria = "Ocio",
                Ubicacion = "Centro de Exposiciones Jockey, Surco",
                ImagenUrl = "https://images.unsplash.com/photo-1607604276583-eef5d076aa5f",
                PlanTipo = "Amigos",
                Calificacion = 4.8,
                Horario = "11:00 AM - 09:00 PM",
                EsEventoEspecial = true,
                PuntosRecompensa = 25
            },
            new PuntoInteres
            {
                Id = 40,
                Nombre = "Librería El Virrey Miraflores",
                Descripcion = "Histórica y espaciosa librería limeña, ideal para tomar un café mientras ojeas las últimas novedades editoriales nacionales.",
                Categoria = "Cultura",
                Ubicacion = "Calle Bolognesi 510, Miraflores",
                ImagenUrl = "https://images.unsplash.com/photo-1512820790803-83ca734da794",
                PlanTipo = "Solo",
                Calificacion = 4.7,
                Horario = "09:30 AM - 09:00 PM",
                EsEventoEspecial = false
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