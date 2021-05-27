using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinica.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                                  //builder => builder.AllowAnyOrigin()
                                  builder => builder.WithOrigins("localhost")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .SetIsOriginAllowed(origin=>true)
                                  .AllowCredentials());
            });

            return services;
        }




    }
}
