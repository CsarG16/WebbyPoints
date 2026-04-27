using Microsoft.EntityFrameworkCore;
using WebbyPoints.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// =========================================================
// REDIS CLOUD: Usamos Redis en la nube como caché distribuida
// =========================================================
var redisConfig = builder.Configuration["REDIS_URL"] ?? "redis-10393.crce181.sa-east-1-2.ec2.cloud.redislabs.com:10393,password=dvSBxVTX5ljVrqPPY4FXAU0CdZbeFdC3,ssl=false,abortConnect=false";

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

// Aplicar migraciones automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al aplicar las migraciones.");
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



