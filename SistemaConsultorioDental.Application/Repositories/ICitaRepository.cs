using SistemaConsultorioDental.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaConsultorioDental.Application.Repositories
{
    public interface ICitaRepository
    {
        Task<IEnumerable<Cita>> GetAllAsync();
        Task<Cita?> GetByIdAsync(int id);
        Task<IEnumerable<Cita>> GetByPacienteIdAsync(int pacienteId);

        Task AddAsync(Cita cita);
        Task UpdateAsync(Cita cita);
        Task DeleteAsync(int id);

        Task SaveChangesAsync();
    }
}
