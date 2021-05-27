using AutoMapper;
using Clinica.API.Extensions;
using Clinica.API.ViewModels;
using Clinica.Business.Interfaces;
using Clinica.Business.Models;
using Clinica.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinica.API.Controllers
{
    //diz que somente pessoas autorizadas podem ter acesso
    [Authorize]
    [Route("api/pacientes")]
    public class PacienteController : MainController
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteRepository pacienteRepository,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IEnderecoRepository enderecoRepository,
                                  IPacienteService pacienteService) : base(notificador)
        {
             _pacienteRepository = pacienteRepository;
            _mapper = mapper;
            _pacienteService = pacienteService;
            _enderecoRepository = enderecoRepository;
        }

       
        [HttpGet]
        public async Task<IEnumerable<PacienteViewModel>> ObterTodos()
        {
            var paciente = _mapper.Map<IEnumerable<PacienteViewModel>>(await _pacienteRepository.ObterTodos());
            return paciente;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PacienteViewModel>> ObterPorId(Guid id)
        {
            var paciente = await ObterPacienteEndereco(id);
            if (paciente == null) return NotFound();

            return paciente;
        }
        [ClaimsAuthorize("Funcionario","Adicionar")]
        [HttpPost]
        public async Task<ActionResult<PacienteViewModel>> Adicionar (PacienteViewModel pacienteViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _pacienteService.Adicionar(_mapper.Map<Paciente>(pacienteViewModel));
            return CustomResponse(pacienteViewModel);

        }
        [ClaimsAuthorize("Funcionario", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PacienteViewModel>> Atualizar(Guid id, PacienteViewModel pacienteViewModel)
        {
            if (id != pacienteViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(pacienteViewModel);
            }
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _pacienteService.Atualizar(_mapper.Map<Paciente>(pacienteViewModel));
            return CustomResponse(pacienteViewModel);
        }

        [ClaimsAuthorize("Funcionario", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PacienteViewModel>> Excluir(Guid id)
        {
            var pacienteViewModel = await ObterPacienteEndereco(id);
            if (pacienteViewModel == null) return NotFound();
            await _pacienteService.Remover(id);

            return CustomResponse(pacienteViewModel);
        }

        private async Task<PacienteViewModel> ObterPacienteEndereco(Guid id)
        {
            return _mapper.Map<PacienteViewModel>(await _pacienteRepository.ObterPacienteEndereco(id));
        }

        [HttpGet("obter-endereco/{id:guid}")]
        private async Task<EnderecoViewModel> ObterEnderecoPorId(Guid id)
        {
            var enderecoViewModel = _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterPorId(id));
            return enderecoViewModel;
        }
        [ClaimsAuthorize("Funcionario", "Atualizar")]
        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoViewModel enderecoViewModel)
        {
            if (id != enderecoViewModel.Id) {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(enderecoViewModel);
            }
            if (!ModelState.IsValid) return CustomResponse(ModelState);
          
            await _pacienteService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoViewModel));
            return CustomResponse(enderecoViewModel);
        }


    }
}
