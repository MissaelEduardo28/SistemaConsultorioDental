using SistemaConsultorioDental.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaConsultorioDental.Application.Repositories
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task<Paciente?> GetByIdAsync(int id);
        Task AddAsync(Paciente paciente);
        Task UpdateAsync(Paciente paciente);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
