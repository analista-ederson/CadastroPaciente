using Clinica.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinica.Data.Mappings
{
    public class PacienteMapping : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Telefone)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.RG)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.CPF)
                .IsRequired()
                .HasColumnType("varchar(14)");




            // 1 : 1 => Paciente : Endereco
            builder.HasOne(p => p.Endereco)
                .WithOne(e => e.Paciente);

           

            builder.ToTable("Paciente");
        }
    }
}
