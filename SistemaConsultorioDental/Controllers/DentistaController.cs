using Microsoft.AspNetCore.Mvc;
using SistemaConsultorioDental.Domain.Entities;
using SistemaConsultorioDental.Application.Repositories;

namespace SistemaConsultorioDental.Web.Controllers
{
    public class DentistaController : Controller
    {
        private readonly IDentistaRepository _dentistaRepository;

        public DentistaController(IDentistaRepository dentistaRepository)
        {
            _dentistaRepository = dentistaRepository;
        }

        // MOSTRAR LISTA
        public async Task<IActionResult> MostrarDentista()
        {
            var dentistas = await _dentistaRepository.GetAllAsync();
            return View(dentistas);
        }

        // AGREGAR 

        [HttpGet]
        public IActionResult AgregarDentista()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDentista(Dentista dentista)
        {
            if (!ModelState.IsValid)
                return View(dentista);

            await _dentistaRepository.AddAsync(dentista);
            await _dentistaRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Dentista agregado correctamente.";
            return RedirectToAction("MostrarDentista");
        }

        // EDITAR 

        [HttpGet]
        public async Task<IActionResult> EditarDentista(int id)
        {
            var dentista = await _dentistaRepository.GetByIdAsync(id);
            if (dentista == null)
                return NotFound();

            return View(dentista);
        }

        [HttpPost]
        public async Task<IActionResult> EditarDentista(Dentista dentista)
        {
            if (!ModelState.IsValid)
                return View(dentista);

            await _dentistaRepository.UpdateAsync(dentista);
            await _dentistaRepository.SaveChangesAsync();


            TempData["SuccessMessage"] = "Dentista actualizado correctamente.";
            return RedirectToAction("MostrarDentista");
        }

        // ELIMINAR

        [HttpGet]
        public async Task<IActionResult> EliminarDentista(int id)
        {
            var dentista = await _dentistaRepository.GetByIdAsync(id);
            if (dentista == null)
                return NotFound();

            return View(dentista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarDentistaConfirmado(int id)
        {
            var dentista = await _dentistaRepository.GetByIdAsync(id);
            if (dentista == null)
                return NotFound();

            await _dentistaRepository.DeleteAsync(id);
            await _dentistaRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Dentista eliminado correctamente.";

            return RedirectToAction("MostrarDentista");
        }


    }
}
