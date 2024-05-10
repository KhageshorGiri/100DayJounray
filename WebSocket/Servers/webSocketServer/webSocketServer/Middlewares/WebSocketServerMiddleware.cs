using System.Net.WebSockets;
using System.Text;

namespace webSocketServer.Middlewares
{
    public class WebSocketServerMiddleware
    {
        public RequestDelegate _next;

        public WebSocketServerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                Console.WriteLine("WebSocket Connected");

                await ReciveMessage(webSocket, (result, buffer) =>
                {
                    if(result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine("Message Recived.");
                        Console.WriteLine($"Message: {Encoding.UTF8.GetString(buffer, 0, result.Count)}");
                        return;
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Console.WriteLine("Recived Close Message.");
                        return;
                    }
                });
            }
            else
            {
                Console.WriteLine("2nd Request called.");
                await _next(context);
            }
        }

        private async Task ReciveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while(socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), 
                    cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}
