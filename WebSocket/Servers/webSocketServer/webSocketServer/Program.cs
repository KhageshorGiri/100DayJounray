using System.Net.WebSockets;
using webSocketServer.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseWebSockets();

app.UseMiddleware<WebSocketServerMiddleware>();

app.Run();
