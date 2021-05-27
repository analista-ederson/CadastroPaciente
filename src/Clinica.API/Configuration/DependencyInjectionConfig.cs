using Clinica.Business.Interfaces;
using Clinica.Business.Notificacoes;
using Clinica.Business.Services;
using Clinica.Data.Context;
using Clinica.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinica.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ClinicaContext>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IPacienteService, PacienteService>();
           

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

           

            return services;
        }
    }
}
