using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.UseCases.Providers;
using Tekus.Application.UseCases.Reports;
using Tekus.Application.UseCases.Services;
using Tekus.Application.UseCases.Summary;



namespace Tekus.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateProviderUseCase>();
            services.AddScoped<ListProvidersUseCase>();
            services.AddScoped<AddServiceToProviderUseCase>();
            services.AddScoped<UpdateProviderUseCase>();
            services.AddScoped<DeleteProviderUseCase>();
            services.AddScoped<UpdateServiceUseCase>();
            services.AddScoped<DeleteServiceUseCase>();
            services.AddScoped<ListServicesByProviderPagedUseCase>();
            services.AddScoped<AssignCountriesToServiceUseCase>();
            services.AddScoped<GetSummaryReportUseCase>();
            services.AddScoped<GetSummaryUseCase>();
            services.AddScoped<SyncCountriesToServiceUseCase>();
           
            return services;
        }
    }
}
