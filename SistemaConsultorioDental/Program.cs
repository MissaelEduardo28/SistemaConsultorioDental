using Microsoft.EntityFrameworkCore;
using SistemaConsultorioDental.Application;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Infrastructure;
using SistemaConsultorioDental.Infrastructure.Repositories;
using SistemaConsultorioDental.Persistance;
using SistemaConsultorioDental.Persistance.Context;
using SistemaConsultorioDental.Persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuración de DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<IDentistaRepository, DentistaRepository>();
builder.Services.AddScoped<IMotivoRepository, MotivoRepository>();


builder.Services.AddControllersWithViews();

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
