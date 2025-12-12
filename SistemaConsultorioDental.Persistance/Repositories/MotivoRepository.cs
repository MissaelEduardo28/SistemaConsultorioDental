using Microsoft.EntityFrameworkCore;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Domain.Entities;
using SistemaConsultorioDental.Persistance.Context;

namespace SistemaConsultorioDental.Infrastructure.Repositories
{
    public class MotivoRepository : IMotivoRepository
    {
        private readonly ApplicationDbContext _context;

        public MotivoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Motivo>> GetAllAsync()
        {
            return await _context.Motivos.ToListAsync();
        }

        public async Task<Motivo?> GetByIdAsync(int id)
        {
            return await _context.Motivos.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Motivo motivo)
        {
            await _context.Motivos.AddAsync(motivo);
        }

        public async Task UpdateAsync(Motivo motivo)
        {
            _context.Motivos.Update(motivo);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var motivo = await _context.Motivos.FindAsync(id);
            if (motivo != null)
            {
                _context.Motivos.Remove(motivo);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
