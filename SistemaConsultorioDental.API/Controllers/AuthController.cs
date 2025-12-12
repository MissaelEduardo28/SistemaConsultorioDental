using Microsoft.AspNetCore.Mvc;
using SistemaConsultorioDental.API.Models;
using SistemaConsultorioDental.Application.Repositories;
using SistemaConsultorioDental.Application.Services;
using SistemaConsultorioDental.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace SistemaConsultorioDental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IPacienteRepository pacienteRepository, ITokenService tokenService)
        {
            _pacienteRepository = pacienteRepository;
            _tokenService = tokenService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequest req)
        {
            var existing = (await _pacienteRepository.GetAllAsync())
                .FirstOrDefault(p => p.NombreCompleto == req.NombreCompleto);

            if (existing != null)
                return BadRequest(new { message = "El paciente ya existe." });

            // Hashear la contraseña
            var hashedPassword = HashPassword(req.Password);

            var paciente = new Paciente
            {
                NombreCompleto = req.NombreCompleto,
                Password = hashedPassword,
                Telefono = req.Telefono,
                Nacionalidad = req.Nacionalidad
            };

            await _pacienteRepository.AddAsync(paciente);

            var token = _tokenService.GenerateToken(paciente.NombreCompleto!, paciente.Id);

            return Ok(new
            {
                token,
                id = paciente.Id,
                nombreCompleto = paciente.NombreCompleto
            });
        }

        // POST: api/auth/login
        // Iniciar sesión verificando hash
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthResponse req)
        {
            var paciente = (await _pacienteRepository.GetAllAsync())
                .FirstOrDefault(p => p.NombreCompleto == req.NombreCompleto);

            if (paciente == null)
                return Unauthorized("Paciente no encontrado.");

            if (paciente.Password != req.Password)
                return Unauthorized("Contraseña incorrecta.");

            var token = _tokenService.GenerateToken(paciente.NombreCompleto!, paciente.Id);

            return Ok(new
            {
                token,
                id = paciente.Id,
                nombreCompleto = paciente.NombreCompleto
            });
        }

        // Métodos de hashing
        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private static bool VerifyHashedPassword(string hashed, string plainPassword)
        {
            var providedHash = HashPassword(plainPassword);
            return hashed == providedHash;
        }
    }
}
