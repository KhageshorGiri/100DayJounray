using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace webSocketServer
{
    public class WebSocketConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public string AddSocket(WebSocket socket)
        {
            string ConnId = Guid.NewGuid().ToString();
            _sockets.TryAdd(ConnId, socket);
            Console.WriteLine("WebSocketServerConnectionManager-> AddSocket: WebSocket added with ID: " + ConnId);
            return ConnId;
        }

        public ConcurrentDictionary<string, WebSocket> GetAllSockets()
        {
            return _sockets;
        }
    }
}

