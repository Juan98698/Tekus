using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Domain.Entities;
using Tekus.Domain.Exceptions;


namespace Tekus.Application.UseCases.Providers
{
    public class AddServiceToProviderUseCase
    {
        private readonly IProviderRepository _providerRepository;

        public AddServiceToProviderUseCase(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<ServiceListItemResponse> ExecuteAsync(AddServiceRequest request)
        {
            
            var provider = await _providerRepository.GetByIdAsync(request.ProviderId);

            if (provider is null)
                throw new Exception("Provider not found");

            
            var service = Service.Create(
                request.Name,
                request.HourValueUsd
            );

            
            provider.AddService(service);

            
            await _providerRepository.UpdateAsync(provider);

            
            return new ServiceListItemResponse
            {
                Id = service.Id,
                Name = service.Name,
                HourValueUsd = service.HourValueUsd
            };
        }
    }
}
