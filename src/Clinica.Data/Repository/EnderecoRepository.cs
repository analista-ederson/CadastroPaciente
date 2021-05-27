using Clinica.Business.Interfaces;
using Clinica.Business.Models;
using Clinica.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(ClinicaContext context) : base(context) { }

       

        public async Task<Endereco> ObterEnderecoPorPaciente(Guid pacienteId)
        {
            return await Db.Endereco.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PacienteId == pacienteId);
        }
    }


}
