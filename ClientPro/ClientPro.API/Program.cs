using ClientPro.Application.Interfaces;
using ClientPro.Application.Services;
using ClientPro.Domain.IRepositories;
using ClientPro.Infrastructure.DataContext;
using ClientPro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Loger configuration
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    logger.Information("Starting web application");

    // Add services to the container.

    // Db Configuration
    builder.Services.AddDbContext<ClientDbContext>(option =>
               option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // service configuration
    builder.Services.AddScoped<IClientRepository, ClientRepository>();
    builder.Services.AddScoped<IClientService, ClientService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}



