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
    public class PacienteRepository: Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(ClinicaContext context) : base(context) { }
        public async Task<Paciente> ObterPacienteEndereco(Guid id)
        {
            return await Db.Paciente.AsNoTracking()
               .Include(p => p.Endereco)
               .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
