using SistemaConsultorioDental.Domain.Entities;

namespace SistemaConsultorioDental.Application.Repositories
{
    public interface IMotivoRepository
    {
        Task<List<Motivo>> GetAllAsync();
        Task<Motivo?> GetByIdAsync(int id);
        Task AddAsync(Motivo motivo);
        Task UpdateAsync(Motivo motivo);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
