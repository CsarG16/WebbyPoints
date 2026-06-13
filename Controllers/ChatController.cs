using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using WebbyPoints.Data;
using WebbyPoints.Plugins;

namespace WebbyPoints.Controllers;

public class ChatController : Controller
{
    private readonly Kernel _kernel;
    private readonly ApplicationDbContext _dbContext;

    public ChatController(Kernel kernel, ApplicationDbContext dbContext)
    {
        _kernel = kernel;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest(new { response = "Mensaje vacío" });
        }

        try
        {
            await HttpContext.Session.LoadAsync();
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            // 1. Obtener detalles del usuario en sesión si está autenticado
            string userContext = "El usuario es un visitante no autenticado (Invitado). Aconséjale amablemente iniciar sesión o registrarse para recibir recomendaciones personalizadas según sus puntos y preferencias.";
            if (usuarioId.HasValue)
            {
                var usuario = await _dbContext.Usuarios.FindAsync(usuarioId.Value);
                if (usuario != null)
                {
                    userContext = $"El estudiante actual (UsuarioId: {usuario.Id}) se llama {usuario.Nombre}, tiene {usuario.Puntos} puntos (rango {usuario.Rango}) y estudia la carrera de {usuario.Carrera} en la universidad {usuario.Universidad}. Sus preferencias/intereses registrados son: {usuario.Preferencias}.";
                }
            }

            // 2. Definir Directiva del Sistema (System Prompt)
            string systemPrompt = $@"Eres el 'Guía Universitario' de la plataforma WebbyPoints. Tu objetivo es aconsejar, recomendar y guiar de manera amigable, dinámica y útil a los estudiantes universitarios sobre qué lugares visitar (puntos de interés) y qué recompensas pueden canjear en base a su perfil, puntos acumulados y preferencias.
Recomendaciones Clave:
- Habla en un tono entusiasta, amigable, juvenil pero respetuoso. Usa expresiones peruanas amables de forma muy sutil si se siente natural (ej: '¡Hola! ¿Listo para explorar?', '¡Qué tal!').
- {userContext}
- Búsqueda libre: Para recomendar lugares de interés, no te limites a categorías exactas. Usa la búsqueda libre del plugin `GetPointsOfInterest` para buscar palabras clave (como 'pizza', 'café', 'biblioteca') si el estudiante pregunta por términos libres.
- Enlaces Interactivos (CRÍTICO): Siempre que recomiendes un lugar de interés, debes incluir un enlace clickeable con el formato Markdown exacto `[Nombre del Lugar](/Home/Detalle/{{Id}})` usando su ID real obtenido del plugin (ej: `[Cafetería Central](/Home/Detalle/3)`). Para recompensas de la tienda, dirígelos usando `[Tienda de Recompensas](/Recompensas)`.
- Historial de canjes: Si el estudiante te pregunta por sus cupones, códigos de voucher o recompensas canjeadas anteriormente, utiliza la función `GetUserRedemptions` pasándole su `UsuarioId` para consultar la base de datos y detállale el estado de sus canjes y el código de voucher.
- Cuando recomiendes lugares de interés o eventos, menciona detalles importantes como su nombre, ubicación, categoría, tipo de plan, calificación y si otorgan puntos extra por check-ins/reseñas (los eventos especiales otorgan 25 puntos, los normales 10 puntos).
- Si te preguntan sobre recompensas, consúltalas en la base de datos y diles cuáles pueden canjear con sus puntos actuales.
- Anímalos constantemente a visitar lugares, dejar reseñas y registrar asistencias (check-ins) para acumular más puntos en WebbyPoints.
- Importante: Solo debes responder temas relacionados a WebbyPoints, vida universitaria, lugares de estudio, ocio, comida, deportes, medio ambiente, voluntariado, cultura o cívica. Si te preguntan sobre temas totalmente externos, diles de forma simpática que como su Guía Universitario de WebbyPoints solo puedes ayudarlos con actividades de la vida estudiantil y la plataforma.
- ¡Mantén las respuestas concisas y bien formateadas usando viñetas o negritas para que sean fáciles de leer en un chat pequeño!";

            // 3. Reconstruir historial desde la sesión
            var chatHistory = new ChatHistory();
            chatHistory.AddSystemMessage(systemPrompt);

            var historyJson = HttpContext.Session.GetString("ChatHistory");
            if (!string.IsNullOrEmpty(historyJson))
            {
                try
                {
                    var savedMessages = JsonSerializer.Deserialize<List<ChatMessageDto>>(historyJson);
                    if (savedMessages != null)
                    {
                        foreach (var msg in savedMessages)
                        {
                            if (msg.Role == "user")
                                chatHistory.AddUserMessage(msg.Content);
                            else if (msg.Role == "assistant")
                                chatHistory.AddAssistantMessage(msg.Content);
                        }
                    }
                }
                catch
                {
                    // Si falla la deserialización, empezamos de nuevo
                }
            }

            // Agregar el mensaje actual del usuario al historial
            chatHistory.AddUserMessage(request.Message);

            // 4. Clonar el Kernel y agregar el plugin con el DbContext scoped de esta petición
            var requestKernel = _kernel.Clone();
            requestKernel.Plugins.AddFromObject(new WebbyPointsPlugin(_dbContext));

            // 5. Configurar ejecución automática de funciones del plugin
            var executionSettings = new OpenAIPromptExecutionSettings
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            // 6. Obtener respuesta del LLM a través de Semantic Kernel
            var chatCompletionService = requestKernel.GetRequiredService<IChatCompletionService>();
            var response = await chatCompletionService.GetChatMessageContentAsync(
                chatHistory,
                executionSettings,
                requestKernel
            );

            string replyText = response.Content ?? "Lo siento, no pude procesar tu solicitud en este momento.";

            // 7. Guardar el nuevo par de mensajes en el historial de sesión (limitando a los últimos 10 mensajes para ahorrar espacio en sesión)
            var currentHistoryList = new List<ChatMessageDto>();
            // Extraer solo los User y Assistant de la sesión para volver a guardarlos
            if (!string.IsNullOrEmpty(historyJson))
            {
                try
                {
                    var prev = JsonSerializer.Deserialize<List<ChatMessageDto>>(historyJson);
                    if (prev != null) currentHistoryList.AddRange(prev);
                }
                catch { }
            }

            currentHistoryList.Add(new ChatMessageDto { Role = "user", Content = request.Message });
            currentHistoryList.Add(new ChatMessageDto { Role = "assistant", Content = replyText });

            // Limitar el historial a los últimos 10 mensajes
            if (currentHistoryList.Count > 10)
            {
                currentHistoryList = currentHistoryList.Skip(currentHistoryList.Count - 10).ToList();
            }

            HttpContext.Session.SetString("ChatHistory", JsonSerializer.Serialize(currentHistoryList));

            return Json(new ChatResponse { Response = replyText });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Chat Error] {ex.Message}\n{ex.StackTrace}");
            return Json(new ChatResponse { Response = "¡Hola! Estoy teniendo problemas para conectarme con mi cerebro de Inteligencia Artificial (Ollama). Por favor, asegúrate de haber ejecutado `ollama run llama3.2` en tu computadora antes de hablar conmigo." });
        }
    }

    [HttpPost]
    public IActionResult ResetChat()
    {
        HttpContext.Session.Remove("ChatHistory");
        return Json(new { success = true, message = "Chat reiniciado" });
    }
}

public class ChatRequest
{
    public string Message { get; set; } = string.Empty;
}

public class ChatResponse
{
    public string Response { get; set; } = string.Empty;
}

public class ChatMessageDto
{
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
