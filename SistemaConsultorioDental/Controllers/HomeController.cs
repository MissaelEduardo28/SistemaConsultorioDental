using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Domain.Entities;
using SistemaConsultorioDental.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SistemaConsultorioDental.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPacienteRepository _pacienteRepository;

        public HomeController(ILogger<HomeController> logger, IPacienteRepository pacienteRepository)
        {
            _logger = logger;
            _pacienteRepository = pacienteRepository;
        }

       

        // LOGIN 
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string NombreCompleto, string Password)
        {
            var paciente = (await _pacienteRepository.GetAllAsync())
                            .FirstOrDefault(p => p.NombreCompleto == NombreCompleto
                                              && p.Password == Password);

            if (paciente != null)
            {
                // Mensaje de bienvenida
                TempData["InfoMessage"] = $"¡Bienvenido, {paciente.NombreCompleto}!";

                // Guardar nombre e ID en sesión
                HttpContext.Session.SetString("PacienteNombre", paciente.NombreCompleto);
                HttpContext.Session.SetInt32("PacienteId", paciente.Id);

                return RedirectToAction("Menu", "Home");
            }

            TempData["ErrorMessage"] = "Nombre o contraseña incorrectos.";
            return View();
        }


        // REGISTRO 
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Paciente model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _pacienteRepository.AddAsync(model);

            TempData["SuccessMessage"] = "¡Registro exitoso! Ya puedes iniciar sesión.";

            return RedirectToAction("Login", "Home");
        }

        public IActionResult Menu()
        {
            var nombre = HttpContext.Session.GetString("PacienteNombre");
            var pacienteId = HttpContext.Session.GetInt32("PacienteId");

            if (pacienteId == null)
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión.";
                return RedirectToAction("Login");
            }

            ViewBag.PacienteNombre = nombre;
            ViewBag.PacienteId = pacienteId;

            return View();
        }

        [HttpPost]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear(); // Elimina todos los datos de sesión
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Cierra toda la sesión
            return RedirectToAction("Login", "Home");
        }


    }
}
