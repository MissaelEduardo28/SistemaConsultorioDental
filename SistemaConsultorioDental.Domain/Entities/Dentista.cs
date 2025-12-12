using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaConsultorioDental.Domain.Entities
{
    public class Dentista
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
