using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Domain.Entities;

namespace SistemaConsultorioDental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteRepository _repo;

        public PacientesController(IPacienteRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Paciente model)
        {
            await _repo.AddAsync(model);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Paciente model)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();
            // mapear campos
            existing.NombreCompleto = model.NombreCompleto;
            existing.Telefono = model.Telefono;
            existing.Nacionalidad = model.Nacionalidad;
            existing.Password = model.Password;
            await _repo.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            await _repo.SaveChangesAsync();
            return NoContent();
        }
    }
}
