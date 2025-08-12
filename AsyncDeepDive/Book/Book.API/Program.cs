using Book.API;
using Book.API.DbContexts;
using Book.API.Helpers;
using Book.API.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BooksDbContext>(option =>
    option.UseSqlite(builder.Configuration.GetConnectionString("BookSqlConnectionString")));

builder.Services.AddControllers();
builder.Services.AddRouting();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCompression(option => { 
    option.EnableForHttps = true; // Enabling this is risky in terms of security, need to study about this
});
builder.Services.Configure<BrotliCompressionProviderOptions>(option =>
{
    option.Level = CompressionLevel.Fastest;
});

builder.Services.AddResponseCaching();
builder.Services.AddOutputCache();

builder.Services.AddScoped<IBookREpository, BookREpository>();
builder.Services.AddScoped<IPropertyMappingService, PropertyMappingService>();
//builder.Services.AddScoped<IUriService, UriService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseResponseCompression();

app.UseResponseCaching();
app.UseOutputCache();

app.AddSeedData();

app.Run();
