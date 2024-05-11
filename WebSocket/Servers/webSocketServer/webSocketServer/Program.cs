using System.Net.WebSockets;
using webSocketServer;
using webSocketServer.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<WebSocketConnectionManager>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseWebSockets();

app.UseMiddleware<WebSocketServerMiddleware>();

app.Run();
