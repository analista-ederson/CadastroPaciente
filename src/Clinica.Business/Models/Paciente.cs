using System;
using System.Collections.Generic;
using System.Text;

namespace Clinica.Business.Models
{
   public  class Paciente : Entity
    {
       
        public string Matricula { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string RG { get; set; }
        public Genero Genero { get; set; }
        public string CPF { get; set; }
        public Endereco Endereco { get; set; }
       
    }
}
