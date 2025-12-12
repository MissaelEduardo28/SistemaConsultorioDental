using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Domain.Entities;

namespace SistemaConsultorioDental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitasController : ControllerBase
    {
        private readonly ICitaRepository _repo;

        public CitasController(ICitaRepository repo) => _repo = repo;

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
        public async Task<IActionResult> Create(Cita model)
        {
            await _repo.AddAsync(model);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Cita model)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.PacienteId = model.PacienteId;
            existing.DentistaId = model.DentistaId;
            existing.MotivoId = model.MotivoId;
            existing.Fecha = model.Fecha;
            existing.Hora = model.Hora;
            existing.DuracionMinutos = model.DuracionMinutos;

            await _repo.UpdateAsync(existing);
            await _repo.SaveChangesAsync();

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
