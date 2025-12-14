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
    public class CreateProviderUseCase
    {
        private readonly IProviderRepository _providerRepository;

        public CreateProviderUseCase(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<ProviderResponse> ExecuteAsync(CreateProviderRequest request)
        {
            var ExistingProvider = await _providerRepository.GetByNitAsync(request.Nit);
            if (ExistingProvider != null)
                throw new DuplicateEntityException("Proveedor");

            var provider = new Provider(
                request.Nit,
                request.Name,
                request.Email
            );

            await _providerRepository.AddAsync(provider);

            return new ProviderResponse
            {
                Id = provider.Id,
                Nit = provider.Nit,
                Name = provider.Name,
                Email = provider.Email
            };
        }
    }
}
