using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Infrastructure.Persistence;
using Tekus.Infrastructure.Persistence.Context;
using Tekus.Infrastructure.Persistence.Repositories;


namespace Tekus.Infrastructure.DI
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
         this IServiceCollection services,
         IConfiguration configuration)
        {
            services.AddDbContext<TekusDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            return services;
        }
    }
}
