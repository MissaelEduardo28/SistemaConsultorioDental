using Microsoft.AspNetCore.Mvc;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Domain.Entities;

namespace SistemaConsultorioDental.Web.Controllers
{
    public class CitasController : Controller
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IDentistaRepository _dentistaRepository;
        private readonly IMotivoRepository _motivoRepository;

        public CitasController(
            ICitaRepository citaRepository,
            IPacienteRepository pacienteRepository,
            IDentistaRepository dentistaRepository,
            IMotivoRepository motivoRepository
        )
        {
            _citaRepository = citaRepository;
            _pacienteRepository = pacienteRepository;
            _dentistaRepository = dentistaRepository;
            _motivoRepository = motivoRepository;
        }

        // LISTAR CITAS DEL PACIENTE
        public async Task<IActionResult> MostrarCitas(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);

            if (paciente == null)
                return NotFound();

            var citas = await _citaRepository.GetByPacienteIdAsync(id);

            ViewBag.Paciente = paciente;

            return View(citas);
        }

        // Agregar Cita
        [HttpGet]
        public async Task<IActionResult> AgregarCitas(int pacienteId)
        {
            ViewBag.PacienteId = pacienteId;

            ViewBag.Dentistas = await _dentistaRepository.GetAllAsync();
            ViewBag.Motivos = await _motivoRepository.GetAllAsync();

            return View(new Cita
            {
                PacienteId = pacienteId
            });
        }

        // Agregar Cita
        [HttpPost]
        public async Task<IActionResult> AgregarCitas(Cita cita)
        {
           
            await _citaRepository.AddAsync(cita);
            await _citaRepository.SaveChangesAsync();

            return RedirectToAction("MostrarCitas", new { id = cita.PacienteId });
        }


        // Editar Cita
        [HttpGet]
        public async Task<IActionResult> EditarCitas(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);
            if (cita == null) return NotFound();

            ViewBag.Dentistas = await _dentistaRepository.GetAllAsync();
            ViewBag.Motivos = await _motivoRepository.GetAllAsync();

            return View(cita);
        }

        // Editar Cita
        [HttpPost]
        public async Task<IActionResult> EditarCitas(Cita cita)
        {
            var citaOriginal = await _citaRepository.GetByIdAsync(cita.Id);
            if (citaOriginal == null)
                return NotFound();

            citaOriginal.DentistaId = cita.DentistaId;
            citaOriginal.MotivoId = cita.MotivoId;
            citaOriginal.DuracionMinutos = cita.DuracionMinutos;

            citaOriginal.Fecha = cita.Fecha;     
            citaOriginal.Hora = cita.Hora;      

            await _citaRepository.UpdateAsync(citaOriginal);
            await _citaRepository.SaveChangesAsync();

            return RedirectToAction("MostrarCitas", new { id = cita.PacienteId });
        }


        // Eliminar Cita
        [HttpGet]
        public async Task<IActionResult> EliminarCitas(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);
            if (cita == null) return NotFound();

            return View(cita);
        }

        // Confirmar Eliminación
        [HttpPost]
        public async Task<IActionResult> EliminarCitasConfirmado(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);

            if (cita != null)
            {
                await _citaRepository.DeleteAsync(id);
                await _citaRepository.SaveChangesAsync();

                return RedirectToAction("MostrarCitas", new { id = cita.PacienteId });
            }

            return NotFound();
        }
    }
}
