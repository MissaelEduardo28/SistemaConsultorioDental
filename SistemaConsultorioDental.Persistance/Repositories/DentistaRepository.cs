using Microsoft.EntityFrameworkCore;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Domain.Entities;
using SistemaConsultorioDental.Persistance.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SistemaConsultorioDental.Infrastructure.Repositories
{
    public class DentistaRepository : IDentistaRepository
    {
        private readonly ApplicationDbContext _context;

        public DentistaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dentista>> GetAllAsync()
        {
            return await _context.Dentistas
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Dentista?> GetByIdAsync(int id)
        {
            return await _context.Dentistas
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Dentista dentista)
        {
            await _context.Dentistas.AddAsync(dentista);
        }

        public async Task UpdateAsync(Dentista dentista)
        {
            _context.Dentistas.Update(dentista);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var dentista = await _context.Dentistas.FindAsync(id);
            if (dentista != null)
            {
                _context.Dentistas.Remove(dentista);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
