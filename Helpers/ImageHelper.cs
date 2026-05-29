namespace WebbyPoints.Helpers;

public static class ImageHelper
{
    public static string OptimizeUnsplashUrl(string url, int width = 450, int quality = 75)
    {
        if (string.IsNullOrEmpty(url))
        {
            return "/images/placeholder.png"; // Fallback if url is empty
        }

        if (url.Contains("unsplash.com"))
        {
            // Remove any existing query string parameters
            var baseUrl = url.Split('?')[0];
            return $"{baseUrl}?auto=format&fit=crop&w={width}&q={quality}";
        }

        return url;
    }
}
