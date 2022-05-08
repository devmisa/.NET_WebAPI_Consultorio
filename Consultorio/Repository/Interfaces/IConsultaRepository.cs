using System.Threading.Tasks;
using System.Collections.Generic;
using Consultorio.Models.Entities;
using Consultorio.Models.Dtos;

namespace Consultorio.Repository.Interfaces
{
    public interface IConsultaRepository : IBaseRepository
    {
        Task<IEnumerable<Consulta>> GetConsultas(ConsultaParams parametro);
        Task<Consulta> GetConsultaById(int id);
    }
}
