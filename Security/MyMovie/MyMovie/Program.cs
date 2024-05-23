using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMovie.Data;
using MyMovie.Models;
using MyMovie.Services;
using MyMovie.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adding Db Configuration
builder.Services.AddDbContext<MyMovieDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgraceSqlServerConnection")));

// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MyMovieDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITokenService, TokenService>();

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
