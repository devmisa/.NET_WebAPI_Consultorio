using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Services
{
    public interface IEmailService
    {
        void EnviarEmail(string email);
    }
}
