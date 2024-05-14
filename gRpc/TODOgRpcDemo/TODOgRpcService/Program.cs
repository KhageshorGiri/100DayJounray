using Microsoft.EntityFrameworkCore;
using TODOgRpcService.Data;
using TODOgRpcService.Services;

var builder = WebApplication.CreateBuilder(args);

// DbConfiguration
builder.Services.AddDbContext<gRpcDbcontext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseConnection")));

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<ToDoItemsService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
