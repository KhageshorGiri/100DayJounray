using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseWebSockets();
app.Use(async (context, next) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        try
        {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            Console.WriteLine("WebSocket Connected");
        }
        catch(Exception ex)
        {
            throw;
        }

    }
    else
    {
        await next();
    }
});

app.Run();
