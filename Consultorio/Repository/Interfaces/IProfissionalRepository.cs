using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;

namespace Consultorio.Repository.Interfaces
{
    public interface IProfissionalRepository : IBaseRepository
    {
        Task<IEnumerable<ProfissionalDto>> GetProfissionais();
        Task<Profissional> GetProfissionalById(int id);

    }
}
