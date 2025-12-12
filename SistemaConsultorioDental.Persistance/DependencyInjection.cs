using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Infrastructure.Repositories;
using SistemaConsultorioDental.Persistance.Context;
using SistemaConsultorioDental.Persistance.Repositories;

namespace SistemaConsultorioDental.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositorios
            services.AddScoped<IDentistaRepository, DentistaRepository>();
            services.AddScoped<IMotivoRepository, MotivoRepository>();
            services.AddScoped<ICitaRepository, CitaRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Servicios de lógica de negocio (si los tienes)
            return services;
        }
    }
}
