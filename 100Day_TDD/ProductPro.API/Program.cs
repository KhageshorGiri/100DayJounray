using Microsoft.EntityFrameworkCore;
using ProductPro.Domain.IRepositories;
using ProductProd.Infrastructure.Data;
using ProductProd.Infrastructure.Repositories;
using ProjectPro.Application.IServices;
using ProjectPro.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// db registration
builder.Services.AddDbContext<ProductProDbContext>(option => 
        option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));    

// service regrestration
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

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
