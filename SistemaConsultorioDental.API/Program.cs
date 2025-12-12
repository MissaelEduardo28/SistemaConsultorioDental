using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using SistemaConsultorioDental.Application;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Infrastructure.Repositories;
using SistemaConsultorioDental.Persistance;
using SistemaConsultorioDental.Persistance.Context;
using SistemaConsultorioDental.Persistance.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// CONFIGURACIÓN DE SERVICIOS 

builder.Services.AddApplication(); 
builder.Services.AddPersistence(configuration); 
builder.Services.AddScoped<SistemaConsultorioDental.Application.Services.ITokenService,
                           SistemaConsultorioDental.Infrastructure.Services.TokenService>();


builder.Services.AddScoped<IDentistaRepository, DentistaRepository>();
builder.Services.AddScoped<IMotivoRepository, MotivoRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Configuración del DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var cs = configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(cs);
});

// Configuración de JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidAudience = configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

// Controladores
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Esquema de Seguridad (Bearer JWT)
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa el token JWT obtenido en el login en el formato: Bearer {token}"
    });

    // Requisito de Seguridad
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
