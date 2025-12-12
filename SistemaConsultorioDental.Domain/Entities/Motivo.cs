using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaConsultorioDental.Domain.Entities
{
    public class Motivo
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}