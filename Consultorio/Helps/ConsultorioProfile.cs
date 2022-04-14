using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;

namespace Consultorio.Helps
{
    public class ConsultorioProfile : Profile
    {
        public ConsultorioProfile()
        {
            CreateMap<Paciente, PacienteDetalhesDto>()
                     .ForMember(dest => dest.Email, opt => opt.Ignore());
            CreateMap<Consulta, ConsultaDto>()
                .ForMember(dest => dest.Especialidade, opt => opt.MapFrom(src => src.Especialidade.Nome))
                .ForMember(dest => dest.Profissional, opt => opt.MapFrom(src => src.Profissional.Nome));
        }
    }
}
