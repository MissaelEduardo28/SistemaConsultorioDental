using Microsoft.AspNetCore.Mvc;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace SistemaConsultorioDental.Web.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteController(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<IActionResult> MostrarPaciente()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return View("MostrarPaciente", pacientes);
        }

        [HttpGet]
        public IActionResult AgregarPaciente()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPaciente(Paciente model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _pacienteRepository.AddAsync(model);

            TempData["SuccessMessage"] = "¡Registro exitoso! Ya puedes iniciar sesión.";
            return RedirectToAction("Menu", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditarPaciente(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        [HttpPost]
        public async Task<IActionResult> EditarPaciente(Paciente model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _pacienteRepository.UpdateAsync(model);
            return RedirectToAction(nameof(MostrarPaciente));
        }

        [HttpGet]
        public async Task<IActionResult> EliminarPaciente(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        [HttpPost, ActionName("EliminarPaciente")]
        public async Task<IActionResult> EliminarPacienteConfirmado(int id)
        {
            await _pacienteRepository.DeleteAsync(id);
            return RedirectToAction(nameof(MostrarPaciente));
        }

        public IActionResult Menu()
        {
            return View();
        }
    }
}
