using System.Threading.Tasks;
using System.Collections.Generic;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;

namespace Consultorio.Repository.Interfaces
{
    public interface IEspecialidadeRepository : IBaseRepository
    {
        Task<IEnumerable<EspecialidadeDto>> GetEspecialidades();
        Task<Especialidade> GetEspecialidadeById(int id);
    }
}
