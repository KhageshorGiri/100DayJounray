using BackGrounds.JOBS.BackgroundServices;
using BackGrounds.JOBS.HostedServices;
using BackgroundsTask.API.Data;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Hangfire Confiuration
// Client
builder.Services.AddHangfire(configuration => configuration
                     .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                     .UseSimpleAssemblyNameTypeSerializer()
                     .UseRecommendedSerializerSettings()
                     .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
/*
builder.Services.AddHangfire(configuration => configuration
                     .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                     .UseSimpleAssemblyNameTypeSerializer()
                     .UseRecommendedSerializerSettings()
                     .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                     {
                         CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),

                     }));*/

// Server
builder.Services.AddHangfireServer();

// Bg Task Registraiton

// Configuration
builder.Services.Configure<HostOptions>(option =>
{
    option.ServicesStartConcurrently = true;
    option.ServicesStopConcurrently = false;
});

builder.Services.AddTransient<TestHostServiceJobs>();

builder.Services.AddHostedService<TestHostServiceJobs>();

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

// HangFire dashbor
app.UseHangfireDashboard();

app.Run();
