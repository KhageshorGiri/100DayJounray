using Microsoft.EntityFrameworkCore;
using WebHookExample.Data;
using WebHookExample.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<WebHookDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/getall", async (contex) => {
    var reuestBody = await contex.Request.ReadFromJsonAsync<Event>();
    Console.WriteLine($"Header: {reuestBody?.EventId} and Body: {reuestBody?.Payload}");
    contex.Response.StatusCode = 200;
    await contex.Response.WriteAsync("Web hook Ack!");
}).WithName("GetAllValue");

app.Run();
