using Microsoft.AspNetCore.Mvc;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Domain.Entities;

namespace SistemaConsultorioDental.Web.Controllers
{
    public class MotivoController : Controller
    {
        private readonly IMotivoRepository _motivoRepository;

        public MotivoController(IMotivoRepository motivoRepository)
        {
            _motivoRepository = motivoRepository;
        }

        // MOSTRAR 
        public async Task<IActionResult> MostrarMotivo()
        {
            var motivos = await _motivoRepository.GetAllAsync();
            return View(motivos);
        }

        // AGREGAR
        [HttpGet]
        public IActionResult AgregarMotivo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarMotivo(Motivo motivo)
        {
            if (!ModelState.IsValid)
                return View(motivo);

            await _motivoRepository.AddAsync(motivo);
            await _motivoRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Motivo agregado correctamente.";
            return RedirectToAction("MostrarMotivo");
        }

        // EDITAR 
        [HttpGet]
        public async Task<IActionResult> EditarMotivo(int id)
        {
            var motivo = await _motivoRepository.GetByIdAsync(id);
            if (motivo == null)
                return NotFound();

            return View(motivo);
        }

        [HttpPost]
        public async Task<IActionResult> EditarMotivo(Motivo motivo)
        {
            if (!ModelState.IsValid)
                return View(motivo);

            await _motivoRepository.UpdateAsync(motivo);
            await _motivoRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Motivo actualizado correctamente.";
            return RedirectToAction("MostrarMotivo");
        }

        // ELIMINAR 
        [HttpGet]
        public async Task<IActionResult> EliminarMotivo(int id)
        {
            var motivo = await _motivoRepository.GetByIdAsync(id);
            if (motivo == null)
                return NotFound();

            return View(motivo);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            var motivo = await _motivoRepository.GetByIdAsync(id);
            if (motivo == null)
                return NotFound();

            await _motivoRepository.DeleteAsync(id);
            await _motivoRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Motivo eliminado correctamente.";
            return RedirectToAction("MostrarMotivo");
        }
    }
}
