﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Consultorio.Context;
using Consultorio.Models.Dtos;
using Consultorio.Repository.Interfaces;
using Consultorio.Models.Entities;

namespace Consultorio.Repository
{
    public class ProfissionalRepository : BaseRepository, IProfissionalRepository
    {
        private readonly ConsultorioContext _context;

        public ProfissionalRepository(ConsultorioContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProfissionalDto>> GetProfissionais()
        {
            return await _context.Profissionais
                 .Select(x => new ProfissionalDto {Id = x.Id, Nome = x.Nome, Ativo = x.Ativo}).ToListAsync();
        }

        public async Task<Profissional> GetProfissionalById(int id)
        {
            return await _context.Profissionais
                .Include(x => x.Consultas)
                .Include(x => x.Especialidades)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}