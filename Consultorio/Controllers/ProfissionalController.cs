using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Consultorio.Repository.Interfaces;
using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;

namespace Consultorio.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalRepository _repository;
        private readonly IMapper _mapper;

        public ProfissionalController(IProfissionalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profissionais = await _repository.GetProfissionais();

            return profissionais.Any()
                ? Ok(profissionais)
                : NotFound("Profissionais não encontrados");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("Profissional inválido");

            var profissional = await _repository.GetProfissionalById(id);

            var profissionalRetorno = _mapper.Map<ProfissionalDetalhesDto>(profissional);

            return profissionalRetorno != null
                ? Ok(profissionalRetorno)
                : NotFound("Profissional não existe na base");
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProfissionalAdicionarDto profissional)
        {
            if (string.IsNullOrEmpty(profissional.Nome))
                return BadRequest("Dados inválidos");

            var profissionalAdicionar = _mapper.Map<Profissional>(profissional);

            _repository.Add(profissionalAdicionar);

            return await _repository.SaveChangesAsync()
                ? Ok("Profissional adicionado com êxito!")
                : BadRequest("Erro ao adicionar o Profissional");

        }

        [HttpPut("id")]

        public async Task<IActionResult> Put(int id, ProfissionalAtualizarDto profissional)
        {
            if (id <= 0)
                return BadRequest("Profissional inválido");

            var profissionalBanco = await _repository.GetProfissionalById(id);

            if (profissionalBanco == null)
                return NotFound("Profissional não encontrado na base de dados");

            var profissionalAtualizar = _mapper.Map(profissional, profissionalBanco);

            _repository.Update(profissionalAtualizar);

            return await _repository.SaveChangesAsync()
                ? Ok("Profissional atualizado com êxito!")
                : BadRequest("Erro ao atualizar o Profissional");
        }

        [HttpDelete("id")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Profissional inválido");

            var profissionalBanco = await _repository.GetProfissionalById(id);

            if (profissionalBanco == null)
                return NotFound("Profissional não encontrado na base de dados");

            _repository.Delete(profissionalBanco);

            return await _repository.SaveChangesAsync()
                ? Ok("Profissional deletado com êxito!")
                : BadRequest("Erro ao deletar o Profissional");
        }

        [HttpPost("adicionar-profissional")]

        public async Task<IActionResult> PostProfissionalEspecialidade(ProfissionalEspecialidadeAdicionarDto profissional)
        {
            int profissionalId = profissional.ProfissionalId;
            int especialidadeId = profissional.EspecialidadeId;

            if (profissionalId <= 0 || especialidadeId <= 0)
                return BadRequest("Dados inválidos");

            var profissionalEspecialidade = await _repository.GetProfissionalEspecialidade(profissionalId, especialidadeId);

            if (profissionalEspecialidade != null)
                return Ok("Especialidade já cadastrada");

            var especialidadeAdicionar = new ProfissionalEspecialidade
            {
                EspecialidadeId = especialidadeId,
                ProfissionalId = profissionalId
            };

            _repository.Add(especialidadeAdicionar);

            return await _repository.SaveChangesAsync()
                ? Ok("Especialidade adicionada com êxito!")
                : BadRequest("Erro ao adicionar especialidade");
        }

        [HttpDelete("{profissionalId}/deletar-especialidade/{especialidadeId}")]

        public async Task<IActionResult> DeleteProfissionalEspecialidade(int profissionalId, int especialidadeId)
        {
            if (profissionalId <= 0 || especialidadeId <= 0)
                BadRequest("Dados inválidos");

            var profissionalEspecialidade = await _repository.GetProfissionalEspecialidade(profissionalId, especialidadeId);

            if (profissionalEspecialidade != null)
                return Ok("Especialidade não cadastrada");

            _repository.Delete(profissionalEspecialidade);

            return await _repository.SaveChangesAsync()
                ? Ok("Especialidade deletada do profissional!")
                : BadRequest("Houve um erro ao deletar especialidade do profissional");
        }
    }
}
