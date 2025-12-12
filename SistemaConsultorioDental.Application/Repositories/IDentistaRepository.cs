using SistemaConsultorioDental.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaConsultorioDental.Application.Repositories
{
    public interface IDentistaRepository
    {
        Task<IEnumerable<Dentista>> GetAllAsync();
        Task<Dentista?> GetByIdAsync(int id);
        Task AddAsync(Dentista dentista);
        Task UpdateAsync(Dentista dentista);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
