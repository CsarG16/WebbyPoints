/* ==========================================================================
   INTERACTIVIDAD DEL WIDGET DE CHAT - WEBBYPOINTS IA
   ========================================================================== */

document.addEventListener("DOMContentLoaded", function () {
    const chatToggle = document.getElementById("chatWidgetToggle");
    const chatContainer = document.getElementById("chatWidgetContainer");
    const chatClose = document.getElementById("chatWidgetClose");
    const chatReset = document.getElementById("chatWidgetReset");
    const chatMessages = document.getElementById("chatWidgetMessages");
    const chatForm = document.getElementById("chatWidgetForm");
    const chatInput = document.getElementById("chatWidgetInput");
    const suggestionChips = document.querySelectorAll(".suggestion-chip");

    // 1. Alternar visualización del chat
    if (chatToggle && chatContainer) {
        chatToggle.addEventListener("click", function () {
            const isOpen = chatContainer.classList.toggle("open");
            chatToggle.classList.toggle("open");
            
            if (isOpen) {
                chatInput.focus();
                // Si está vacío, cargar mensaje de bienvenida
                if (chatMessages.children.length === 0) {
                    addWelcomeMessage();
                }
            }
        });
    }

    if (chatClose && chatContainer && chatToggle) {
        chatClose.addEventListener("click", function () {
            chatContainer.classList.remove("open");
            chatToggle.classList.remove("open");
        });
    }

    // 2. Reiniciar Chat
    if (chatReset) {
        chatReset.addEventListener("click", function () {
            if (confirm("¿Estás seguro de que deseas reiniciar la conversación?")) {
                fetch("/Chat/ResetChat", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    }
                })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        chatMessages.innerHTML = "";
                        addWelcomeMessage();
                    }
                })
                .catch(error => {
                    console.error("Error al reiniciar el chat:", error);
                });
            }
        });
    }

    // 3. Manejo del envío del formulario
    if (chatForm) {
        chatForm.addEventListener("submit", function (e) {
            e.preventDefault();
            const message = chatInput.value.trim();
            if (!message) return;

            sendMessage(message);
            chatInput.value = "";
        });
    }

    // 4. Configurar chips de sugerencia
    suggestionChips.forEach(chip => {
        chip.addEventListener("click", function () {
            const text = this.getAttribute("data-msg") || this.innerText;
            sendMessage(text);
        });
    });

    // 5. Función de envío de mensajes
    function sendMessage(text) {
        // Agregar mensaje del usuario a la pantalla
        appendMessage("user", text);
        
        // Agregar indicador de escritura
        const typingIndicator = showTypingIndicator();
        
        // Petición al backend
        fetch("/Chat/SendMessage", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ message: text })
        })
        .then(response => response.json())
        .then(data => {
            // Eliminar indicador de escritura
            removeTypingIndicator(typingIndicator);
            
            // Agregar respuesta del bot
            appendMessage("bot", data.response);
        })
        .catch(error => {
            console.error("Error al comunicarse con la IA:", error);
            removeTypingIndicator(typingIndicator);
            appendMessage("bot", "¡Uf! Hubo un error de red al contactar con Ollama. Asegúrate de tener Ollama abierto en tu PC.");
        });
    }

    // 6. Mensaje de bienvenida inicial
    function addWelcomeMessage() {
        const welcomeText = "¡Hola! 👋 Soy tu **Guía Universitario** de WebbyPoints. \n\nTe puedo sugerir los mejores lugares para estudiar, comer, hacer deportes o divertirte según tus intereses. También puedo decirte qué recompensas puedes canjear con tus puntos actuales. \n\n*¿De qué tienes ganas hoy?*";
        appendMessage("bot", welcomeText);
    }

    // 7. Renderizar mensajes en la pantalla
    function appendMessage(sender, text) {
        const messageDiv = document.createElement("div");
        messageDiv.classList.add("chat-message", sender);

        const contentDiv = document.createElement("div");
        contentDiv.classList.add("chat-message-content");
        contentDiv.innerHTML = formatMarkdown(text);

        const timeSpan = document.createElement("span");
        timeSpan.classList.add("chat-message-time");
        const now = new Date();
        timeSpan.innerText = now.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

        messageDiv.appendChild(contentDiv);
        messageDiv.appendChild(timeSpan);
        chatMessages.appendChild(messageDiv);
        
        // Auto-scroll al fondo
        chatMessages.scrollTop = chatMessages.scrollHeight;
    }

    // 8. Indicador de escritura animado
    function showTypingIndicator() {
        const messageDiv = document.createElement("div");
        messageDiv.classList.add("chat-message", "bot");
        messageDiv.id = "temp-typing-indicator";

        const contentDiv = document.createElement("div");
        contentDiv.classList.add("chat-message-content");
        
        const indicator = document.createElement("div");
        indicator.classList.add("typing-indicator");
        
        for (let i = 0; i < 3; i++) {
            const dot = document.createElement("div");
            dot.classList.add("typing-dot");
            indicator.appendChild(dot);
        }

        contentDiv.appendChild(indicator);
        messageDiv.appendChild(contentDiv);
        chatMessages.appendChild(messageDiv);
        chatMessages.scrollTop = chatMessages.scrollHeight;
        
        return messageDiv;
    }

    function removeTypingIndicator(indicator) {
        if (indicator && indicator.parentNode) {
            indicator.parentNode.removeChild(indicator);
        } else {
            const temp = document.getElementById("temp-typing-indicator");
            if (temp) temp.remove();
        }
    }

    // 9. Conversor básico de Markdown a HTML (Negritas, cursivas y viñetas)
    function formatMarkdown(text) {
        if (!text) return "";
        
        // Escapar caracteres HTML para prevenir inyecciones
        let html = text
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;");

        // Convertir negrita (**texto** o __texto__)
        html = html.replace(/\*\*(.*?)\*\*/g, "<strong>$1</strong>");
        html = html.replace(/__(.*?)__/g, "<strong>$1</strong>");

        // Convertir cursiva (*texto* o _texto_)
        html = html.replace(/\*(.*?)\*/g, "<em>$1</em>");
        html = html.replace(/_(.*?)_/g, "<em>$1</em>");

        // Convertir listas con viñetas (- elemento o * elemento)
        let lines = html.split("\n");
        let inList = false;
        let resultLines = [];

        for (let i = 0; i < lines.length; i++) {
            let line = lines[i];
            let trimmed = line.trim();

            if (trimmed.startsWith("- ") || trimmed.startsWith("* ")) {
                if (!inList) {
                    resultLines.push('<ul class="mb-2">');
                    inList = true;
                }
                let content = trimmed.substring(2);
                resultLines.push("<li>" + content + "</li>");
            } else {
                if (inList) {
                    resultLines.push("</ul>");
                    inList = false;
                }
                resultLines.push(line);
            }
        }
        if (inList) {
            resultLines.push("</ul>");
        }

        let formatted = resultLines.join("\n");

        // Reemplazar saltos de línea por <br> (excepto los de listas)
        formatted = formatted.replace(/\n/g, "<br>");
        
        // Limpiar br duplicados alrededor de listas
        formatted = formatted.replace(/<br><ul/g, "<ul").replace(/<\/ul><br>/g, "</ul>");
        
        return formatted;
    }
});
