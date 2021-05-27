using Clinica.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Business.Interfaces
{
   public interface IPacienteService : IDisposable
    {
        Task<bool> Adicionar(Paciente paciente);

        Task<bool> Atualizar(Paciente fornecedor);
        Task<bool> Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}
