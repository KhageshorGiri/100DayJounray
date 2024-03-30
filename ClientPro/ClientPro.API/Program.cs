using ClientPro.API.HealthCheck;
using ClientPro.Application.Interfaces;
using ClientPro.Application.Services;
using ClientPro.Domain.IRepositories;
using ClientPro.Infrastructure.DataContext;
using ClientPro.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net.Mime;
using System.Text.Json;

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

    //health checks
    builder.Services.AddHealthChecks()
        //.AddCheck<ApplicationHealthCheck>("application_health_check")
        .AddSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));

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

    // Health check Middleware configuration
    app.MapHealthChecks("/health",new HealthCheckOptions()
    {
        //Predicate = (_) => false,
        ResponseWriter = async(context, report) =>
        {
            var result = JsonSerializer.Serialize(
                new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(entry => new
                    {
                        name = entry.Key.ToString(),
                        status = entry.Value.Status.ToString(),
                        exception = entry.Value.Exception is not null ? entry.Value.Exception.Message : "none",
                        duration = entry.Value.Duration.ToString()
                    })
                });

            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(result);
        }
    });

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



