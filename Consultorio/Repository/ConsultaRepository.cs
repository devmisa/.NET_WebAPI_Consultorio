﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Consultorio.Context;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Consultorio.Models.Dtos;

namespace Consultorio.Repository
{
    public class ConsultaRepository : BaseRepository, IConsultaRepository
    {
        private readonly ConsultorioContext _context;

        public ConsultaRepository(ConsultorioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consulta>> GetConsultas(ConsultaParams parametro)
        {
            var consultas = _context.Consultas
                .Include(x => x.Paciente)
                .Include(x => x.Profissional)
                .Include(x => x.Especialidade).AsQueryable();

            DateTime dataVazia = new DateTime();

            if (parametro.DataInicio != dataVazia)
                consultas = consultas.Where(x => x.DataHorario >= parametro.DataInicio);

            if (parametro.DataFinal != dataVazia)
                consultas = consultas.Where(x => x.DataHorario >= parametro.DataFinal);

            if (!string.IsNullOrEmpty(parametro.NomeEspecialidade))
            {
                string nomeEspecialidade = parametro.NomeEspecialidade.ToLower().Trim();
                consultas = consultas.Where(x => x.Especialidade.Nome.ToLower().Contains(nomeEspecialidade));
            }

            return await consultas.ToListAsync();
        }

        public async Task<Consulta> GetConsultaById(int id)
        {
            return await _context.Consultas
               .Include(x => x.Paciente)
               .Include(x => x.Profissional)
               .Include(x => x.Especialidade)
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();
        }
    }
}