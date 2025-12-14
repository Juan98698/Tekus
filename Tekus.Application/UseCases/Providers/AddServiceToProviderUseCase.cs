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

        public async Task ExecuteAsync(AddServiceRequest request)
        {
            var provider = await _providerRepository.GetByIdAsync(request.ProviderId);

            if (provider == null)
                throw new NotFoundException("Proveedor");
            var service = new Service(
                request.Name,
                request.HourValueUsd
            );

            provider.AddService(service);

            await _providerRepository.UpdateAsync(provider);
        }
    }
}
