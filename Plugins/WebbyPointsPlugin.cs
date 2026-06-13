using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.EntityFrameworkCore;
using WebbyPoints.Data;

namespace WebbyPoints.Plugins;

public class WebbyPointsPlugin
{
    private readonly ApplicationDbContext _dbContext;

    public WebbyPointsPlugin(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [KernelFunction, Description("Obtiene la lista de todos los puntos de interés de la universidad. Opcionalmente se puede filtrar por categoría (ej. Estudio, Ocio, Comida, Cultura, Romántico, Deporte, Medio Ambiente, Cívico) o por tipo de plan (ej. Solo, Pareja, Amigos).")]
    public async Task<string> GetPointsOfInterest(
        [Description("Categoría opcional para filtrar los lugares (ej. Estudio, Ocio, Comida, Cultura, Romántico, Deporte, Medio Ambiente, Cívico)")] string? categoria = null,
        [Description("Tipo de plan opcional para filtrar (ej. Solo, Pareja, Amigos)")] string? planTipo = null)
    {
        try
        {
            var query = _dbContext.PuntosInteres.AsQueryable();

            if (!string.IsNullOrEmpty(categoria))
            {
                var lowerCat = categoria.ToLower();
                query = query.Where(p => p.Categoria.ToLower().Contains(lowerCat));
            }

            if (!string.IsNullOrEmpty(planTipo))
            {
                var lowerPlan = planTipo.ToLower();
                query = query.Where(p => p.PlanTipo.ToLower() == lowerPlan);
            }

            var puntos = await query.Take(15)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.Descripcion,
                    p.Categoria,
                    p.Ubicacion,
                    p.PlanTipo,
                    p.Calificacion,
                    p.PrecioRango,
                    p.Horario,
                    p.EsEventoEspecial,
                    p.PuntosRecompensa
                })
                .ToListAsync();

            return System.Text.Json.JsonSerializer.Serialize(puntos);
        }
        catch (Exception ex)
        {
            return $"Error al obtener los puntos de interés: {ex.Message}";
        }
    }

    [KernelFunction, Description("Obtiene una lista de las recompensas activas disponibles en la tienda de WebbyPoints que los estudiantes pueden canjear usando sus puntos.")]
    public async Task<string> GetAvailableRewards()
    {
        try
        {
            var rewards = await _dbContext.Recompensas
                .Where(r => r.Activa && r.Stock > 0)
                .Select(r => new
                {
                    r.Id,
                    r.Nombre,
                    r.Descripcion,
                    r.CostoPuntos,
                    r.Stock,
                    r.Categoria
                })
                .ToListAsync();

            return System.Text.Json.JsonSerializer.Serialize(rewards);
        }
        catch (Exception ex)
        {
            return $"Error al obtener las recompensas: {ex.Message}";
        }
    }

    [KernelFunction, Description("Obtiene la información de perfil del estudiante autenticado actual, incluyendo sus puntos acumulados, carrera, edad, universidad y preferencias/intereses (ej. 'Estudio, Ocio, Comida').")]
    public async Task<string> GetUserDetails(
        [Description("El ID único del usuario en sesión")] int userId)
    {
        try
        {
            var user = await _dbContext.Usuarios
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    u.Nombre,
                    u.Email,
                    u.Universidad,
                    u.Carrera,
                    u.Edad,
                    u.Preferencias,
                    u.Puntos,
                    u.Rango
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return "Usuario no encontrado.";
            }

            return System.Text.Json.JsonSerializer.Serialize(user);
        }
        catch (Exception ex)
        {
            return $"Error al obtener la información del usuario: {ex.Message}";
        }
    }
}
