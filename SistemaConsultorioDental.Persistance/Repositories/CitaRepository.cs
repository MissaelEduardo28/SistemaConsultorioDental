using Microsoft.EntityFrameworkCore;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Domain.Entities;
using SistemaConsultorioDental.Persistance.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaConsultorioDental.Persistance.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly ApplicationDbContext _context;

        public CitaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cita>> GetAllAsync()
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Dentista)
                .Include(c => c.Motivo)
                .ToListAsync();
        }

        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Dentista)
                .Include(c => c.Motivo)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cita>> GetByPacienteIdAsync(int pacienteId)
        {
            return await _context.Citas
                .Where(c => c.PacienteId == pacienteId)
                .Include(c => c.Dentista)
                .Include(c => c.Motivo)
                .OrderBy(c => c.Fecha)
                .ToListAsync();
        }

        public async Task AddAsync(Cita cita)
        {
            await _context.Citas.AddAsync(cita);
        }

        public async Task UpdateAsync(Cita cita)
        {
            _context.Citas.Update(cita);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
                _context.Citas.Remove(cita);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
