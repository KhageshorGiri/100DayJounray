using BackGrounds.JOBS.BackgroundServices;
using BackGrounds.JOBS.HostedServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Bg Task Registraiton

// Configuration
builder.Services.Configure<HostOptions>(option =>
{
    option.ServicesStartConcurrently = true;
    option.ServicesStopConcurrently = false;
});

builder.Services.AddHostedService<ExampleHostedLifeCycleService>();

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
