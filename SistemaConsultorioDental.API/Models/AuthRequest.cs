namespace SistemaConsultorioDental.API.Models
{
    public class AuthRequest
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Nacionalidad { get; set; }
    }

}
