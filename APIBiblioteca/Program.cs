using APIBiblioteca.Controllers;
using DataBaseFirst.Data;
using DataBaseFirst.Helpers;
using DataBaseFirst.Repository;
using DataBaseFirst.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSetting = builder.Configuration.GetSection("Jwt");
var claveSecreta = jwtSetting.GetValue<string>("Key");

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
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<GeneroRepository>();
builder.Services.AddScoped<GeneroService>();
builder.Services.AddScoped<MenuService>();

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddSingleton<Token>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSetting["Issuer"],
        ValidAudience = jwtSetting["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta))
    };
});


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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Biblioteca",
        Version = "v1",
        Description = "API para gestionar autores, géneros, libros, usuarios y préstamos"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Biblioteca v1");
    });
}

app.UseCors("NuevaPolitica");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
