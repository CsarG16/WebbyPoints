using Microsoft.EntityFrameworkCore;
using WebbyPoints.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// =========================================================
// REDIS CLOUD: Usamos Redis en la nube como caché distribuida
// =========================================================
var redisConfig = builder.Configuration.GetConnectionString("Redis") 
                  ?? builder.Configuration["REDIS_URL"] 
                  ?? "localhost:6379";

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConfig;
    options.InstanceName = "WebbyPoints_";
});

// =========================================================
// SESIONES: Datos privados de cada usuario (guardados en Redis)
// =========================================================
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=WebbyPoints.db"));

var app = builder.Build();

// =========================================================
// INTELIGENCIA ARTIFICIAL: Entrenamiento automático del recomendador si no existe
// =========================================================
string modelPath = Path.Combine(AppContext.BaseDirectory, "RecommenderModel.mlnet");
if (!File.Exists(modelPath))
{
    try
    {
        Console.WriteLine("[IA] No se encontró el modelo de recomendación. Entrenando...");
        WebbyPoints.ML.Recommender.RecommenderModelTraining.TrainModel();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[IA Error] No se pudo entrenar la IA automáticamente: {ex.Message}");
    }
}

// Aplicar migraciones automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        // Asegurar que los admins y usuarios semilla tengan los puntos e información correctos
        var cesar = context.Usuarios.FirstOrDefault(u => u.Email == "cesar_paredes7@usmp.pe");
        if (cesar != null)
        {
            cesar.Puntos = 2000;
            cesar.Carrera = "Ingenieria de Sistemas";
            cesar.EsAdmin = true;
            cesar.Rango = "Administrador";
        }
        else
        {
            context.Usuarios.Add(new WebbyPoints.Models.Usuario
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
            });
        }

        var fausto = context.Usuarios.FirstOrDefault(u => u.Email == "fausto_miranda@usmp.pe");
        if (fausto != null)
        {
            fausto.Puntos = 1000;
            fausto.Carrera = "Ingenieria de Sistemas";
            fausto.EsAdmin = true;
            fausto.Rango = "Administrador";
        }
        else
        {
            context.Usuarios.Add(new WebbyPoints.Models.Usuario
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
            });
        }

        // Agregar 3 usuarios random normales con 500 puntos si no existen
        if (!context.Usuarios.Any(u => u.Email == "juan_perez@usmp.pe"))
        {
            context.Usuarios.Add(new WebbyPoints.Models.Usuario
            {
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
            });
        }

        if (!context.Usuarios.Any(u => u.Email == "maria_rojas@usmp.pe"))
        {
            context.Usuarios.Add(new WebbyPoints.Models.Usuario
            {
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
            });
        }

        if (!context.Usuarios.Any(u => u.Email == "luis_flores@usmp.pe"))
        {
            context.Usuarios.Add(new WebbyPoints.Models.Usuario
            {
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
            });
        }

        context.SaveChanges();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al aplicar las migraciones o poblar usuarios.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession(); // Necesario para HttpContext.Session

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();



