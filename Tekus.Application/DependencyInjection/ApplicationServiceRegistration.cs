using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.UseCases.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Tekus.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateProviderUseCase>();
            // agrega aquí otros UseCases cuando los crees
            return services;
        }
    }
}
