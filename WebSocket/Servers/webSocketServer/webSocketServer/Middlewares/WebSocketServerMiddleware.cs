using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace webSocketServer.Middlewares
{
    public class WebSocketServerMiddleware
    {
        public RequestDelegate _next;
        private WebSocketConnectionManager _manager;

        public WebSocketServerMiddleware(RequestDelegate next, WebSocketConnectionManager manager)
        {
            _next = next;
            _manager = manager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                Console.WriteLine("WebSocket Connected");

                string conn = _manager.AddSocket(webSocket);

                //Send ConnID Back
                await SendConnID(webSocket, conn);

                await ReciveMessage(webSocket, async (result, buffer) =>
                {
                    if(result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine("Message Recived.");
                        Console.WriteLine($"Message: {Encoding.UTF8.GetString(buffer, 0, result.Count)}");
                        await RouteJSONMessageAsync(Encoding.UTF8.GetString(buffer, 0, result.Count));
                        return;
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Console.WriteLine("Recived Close Message.");
                        string id = _manager.GetAllSockets().FirstOrDefault(s => s.Value == webSocket).Key;
                        Console.WriteLine($"Receive->Close on: " + id);

                        WebSocket sock;
                        _manager.GetAllSockets().TryRemove(id, out sock);
                        Console.WriteLine("Managed Connections: " + _manager.GetAllSockets().Count.ToString());

                        await sock.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
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


        private async Task RouteJSONMessageAsync(string message)
        {

            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
            Console.WriteLine("To: " + routeOb.To);
            Guid guidOutput;

            if (Guid.TryParse(routeOb.To.ToString(), out guidOutput))
            {
                Console.WriteLine("Targeted");
                var sock = _manager.GetAllSockets().FirstOrDefault(s => s.Key == routeOb.To.ToString());
                if (sock.Value != null)
                {
                    if (sock.Value.State == WebSocketState.Open)
                        await sock.Value.SendAsync(Encoding.UTF8.GetBytes(routeOb.Message.ToString()), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    Console.WriteLine("Invalid Recipient");
                }
            }
            else
            {
                Console.WriteLine("Broadcast");
                foreach (var sock in _manager.GetAllSockets())
                {
                    if (sock.Value.State == WebSocketState.Open)
                        await sock.Value.SendAsync(Encoding.UTF8.GetBytes(routeOb.Message.ToString()), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }


        }


        private async Task SendConnID(WebSocket socket, string connID)
        {
            var buffer = Encoding.UTF8.GetBytes("ConnID: " + connID);
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

      
    }
}
