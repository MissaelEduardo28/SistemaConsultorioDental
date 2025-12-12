using Microsoft.EntityFrameworkCore;
using SistemaConsultorioDental.Domain.Entities;

namespace SistemaConsultorioDental.Persistance.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Dentista> Dentistas { get; set; }
        public DbSet<Motivo> Motivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Citas)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Dentista)
                .WithMany(d => d.Citas)
                .HasForeignKey(c => c.DentistaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Motivo)
                .WithMany(m => m.Citas)
                .HasForeignKey(c => c.MotivoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ignorar propiedades calculadas
            modelBuilder.Entity<Cita>()
                .Ignore(c => c.TiempoRestante)
                .Ignore(c => c.Estado);

            modelBuilder.Entity<Cita>()
           .Property(c => c.Fecha)
           .HasConversion(
           v => v.ToDateTime(TimeOnly.MinValue),
           v => DateOnly.FromDateTime(v)
           );

            modelBuilder.Entity<Cita>()
                .Property(c => c.Hora)
                .HasConversion(
                    v => v.ToTimeSpan(),
                    v => TimeOnly.FromTimeSpan(v)
                );

        }
    }
}
