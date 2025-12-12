using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaConsultorioDental.Domain.Entities
{
    public class Cita
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int DentistaId { get; set; }
        public int MotivoId { get; set; }

        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }

        public int DuracionMinutos { get; set; }

        // Propiedades calculadas
        public TimeSpan TiempoRestante
        {
            get
            {
                var fechaHoraInicio = Fecha.ToDateTime(Hora);
                return fechaHoraInicio - DateTime.Now;
            }
        }

        public string Estado
        {
            get
            {
                var fechaHoraInicio = Fecha.ToDateTime(Hora);
                var fechaHoraFin = fechaHoraInicio.AddMinutes(DuracionMinutos);

                if (DateTime.Now < fechaHoraInicio)
                    return "Vigente";
                else if (DateTime.Now >= fechaHoraInicio && DateTime.Now <= fechaHoraFin)
                    return "En proceso";
                else
                    return "Finalizado";
            }
        }

        public Paciente? Paciente { get; set; }
        public Dentista? Dentista { get; set; }
        public Motivo? Motivo { get; set; }
    }
}
