using System.Collections.Generic;

namespace Consultorio.Models.Dtos
{
    public class PacienteDetalhesDto
    {
        public int Id { get; set; }   
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Celular { get; set; }
        public string Cpf { get; set; }
        public List<ConsultaDto> Consultas { get; set; }
    }

}

