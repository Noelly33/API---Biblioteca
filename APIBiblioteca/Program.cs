using APIBiblioteca.Controllers;
using DataBaseFirst.Data;
using DataBaseFirst.Repository;
using DataBaseFirst.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBContext>(options =>

   options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"))

);

builder.Services.AddScoped<AutorRepository>();
builder.Services.AddScoped<AutorService>();
builder.Services.AddScoped<LibroRepository>();
builder.Services.AddScoped<LibroService>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<PrestamoRepository>();
builder.Services.AddScoped<PrestamoService>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{ 
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
