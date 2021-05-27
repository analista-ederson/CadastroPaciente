using Clinica.Business.Interfaces;
using Clinica.Business.Models;
using Clinica.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Business.Services
{
    public class PacienteService : BaseService, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        private readonly IUser _user;

        public PacienteService(IPacienteRepository pacienteRepository,
                                 IEnderecoRepository enderecoRepository,
                                 INotificador notificador,
                                 IUser user) : base(notificador)
        {
            _pacienteRepository = pacienteRepository;
            _enderecoRepository = enderecoRepository;
            _user = user;
        }
        public async Task<bool> Adicionar(Paciente paciente)
        {
            if (!ExecutarValidacao(new PacienteValidation(), paciente)
                || !ExecutarValidacao(new EnderecoValidation(), paciente.Endereco)) return false;

            if (_pacienteRepository.Buscar(f => f.CPF == paciente.CPF).Result.Any())
            {
                Notificar("Já existe um Paciente com este CPF informado.");
                return false;
            }

            await _pacienteRepository.Adicionar(paciente);
            //pegando o nome do usuário
            //var user = _user.Name;
            return true;
            
        }

        public async Task<bool> Atualizar(Paciente paciente)
        {
            if (!ExecutarValidacao(new PacienteValidation(), paciente)) return false;

            if (_pacienteRepository.Buscar(f => f.CPF == paciente.CPF && f.Id != paciente.Id).Result.Any())
            {
                Notificar("Já existe um Paciente com este CPF informado.");
                return false;
            }

            await _pacienteRepository.Atualizar(paciente);
            return true;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public void Dispose()
        {
            _pacienteRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }

        public async Task<bool> Remover(Guid id)
        {
           
            var endereco = await _enderecoRepository.ObterEnderecoPorPaciente(id);

            if (endereco != null)
            {
                await _enderecoRepository.Remover(endereco.Id);
            }

            await _pacienteRepository.Remover(id);
            return true;
        }
    }
}
