using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace WebbyPoints.Helpers;

/// <summary>
/// Helper para simplificar las operaciones de caché distribuida con Redis.
/// Encapsula la serialización/deserialización JSON y el manejo de TTL (tiempo de vida).
/// </summary>
public static class RedisCacheHelper
{
    /// <summary>
    /// Obtiene un objeto del caché de Redis. Si no existe, ejecuta la función factory,
    /// guarda el resultado en Redis con el TTL especificado y lo devuelve.
    /// Patrón Cache-Aside (Lazy Loading).
    /// </summary>
    public static async Task<T?> GetOrSetAsync<T>(
        IDistributedCache cache,
        string cacheKey,
        Func<Task<T>> factory,
        TimeSpan? absoluteExpiration = null,
        TimeSpan? slidingExpiration = null)
    {
        // 1. Intentar leer del caché de Redis
        var cachedJson = await cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedJson))
        {
            // HIT: Los datos estaban en Redis, deserializamos y devolvemos
            Console.WriteLine($"[Redis CACHE HIT] Key: {cacheKey}");
            return JsonSerializer.Deserialize<T>(cachedJson);
        }

        // MISS: No estaban en Redis, ejecutamos la consulta a la BD
        Console.WriteLine($"[Redis CACHE MISS] Key: {cacheKey} → Consultando BD...");
        var data = await factory();

        if (data != null)
        {
            // Guardamos en Redis con las opciones de expiración
            var options = new DistributedCacheEntryOptions();

            if (absoluteExpiration.HasValue)
                options.AbsoluteExpirationRelativeToNow = absoluteExpiration.Value;

            if (slidingExpiration.HasValue)
                options.SlidingExpiration = slidingExpiration.Value;

            // Si no se especificó ningún TTL, usamos 5 minutos por defecto
            if (!absoluteExpiration.HasValue && !slidingExpiration.HasValue)
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

            var json = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(cacheKey, json, options);
            Console.WriteLine($"[Redis CACHE SET] Key: {cacheKey} guardado en Redis Cloud");
        }

        return data;
    }

    /// <summary>
    /// Invalida (elimina) una clave específica del caché de Redis.
    /// Útil cuando se modifica un dato en la BD y queremos que la próxima
    /// lectura traiga datos frescos.
    /// </summary>
    public static async Task InvalidateAsync(IDistributedCache cache, string cacheKey)
    {
        await cache.RemoveAsync(cacheKey);
        Console.WriteLine($"[Redis CACHE INVALIDATE] Key: {cacheKey} eliminada");
    }

    /// <summary>
    /// Invalida múltiples claves del caché de Redis a la vez.
    /// </summary>
    public static async Task InvalidateMultipleAsync(IDistributedCache cache, params string[] cacheKeys)
    {
        foreach (var key in cacheKeys)
        {
            await cache.RemoveAsync(key);
            Console.WriteLine($"[Redis CACHE INVALIDATE] Key: {key} eliminada");
        }
    }
}
