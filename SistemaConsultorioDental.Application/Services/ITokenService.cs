using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaConsultorioDental.Domain.Entities;

namespace SistemaConsultorioDental.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username, int userId);
    }
}
