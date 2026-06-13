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

    [KernelFunction, Description("Obtiene la lista de todos los puntos de interés de la universidad. Opcionalmente se puede filtrar por categoría (ej. Estudio, Ocio, Comida, Cultura, Romántico, Deporte, Medio Ambiente, Cívico), por tipo de plan (ej. Solo, Pareja, Amigos) o realizar una búsqueda de texto libre.")]
    public async Task<string> GetPointsOfInterest(
        [Description("Categoría opcional para filtrar los lugares (ej. Estudio, Ocio, Comida, Cultura, Romántico, Deporte, Medio Ambiente, Cívico)")] string? categoria = null,
        [Description("Tipo de plan opcional para filtrar (ej. Solo, Pareja, Amigos)")] string? planTipo = null,
        [Description("Búsqueda opcional de texto libre para buscar palabras en el nombre o descripción (ej. 'pizza', 'café', 'biblioteca')")] string? search = null)
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

            if (!string.IsNullOrEmpty(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(p => p.Nombre.ToLower().Contains(lowerSearch) || p.Descripcion.ToLower().Contains(lowerSearch));
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

    [KernelFunction, Description("Obtiene el historial de canjes o vouchers de recompensas activos y pasados del usuario.")]
    public async Task<string> GetUserRedemptions(
        [Description("El ID único del usuario actual")] int userId)
    {
        try
        {
            var canjes = await _dbContext.Canjes
                .Where(c => c.UsuarioId == userId)
                .OrderByDescending(c => c.FechaCanje)
                .Select(c => new
                {
                    c.Id,
                    RecompensaNombre = c.Recompensa != null ? c.Recompensa.Nombre : "Recompensa",
                    c.FechaCanje,
                    c.CodigoVoucher,
                    c.Estado,
                    c.PuntosGastados
                })
                .ToListAsync();

            return System.Text.Json.JsonSerializer.Serialize(canjes);
        }
        catch (Exception ex)
        {
            return $"Error al obtener los canjes del usuario: {ex.Message}";
        }
    }
}
