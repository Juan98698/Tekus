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
        private readonly IProviderRepository _repository;

        public CreateProviderUseCase(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProviderResponse> ExecuteAsync(CreateProviderRequest request)
        {
            var existingByNit = await _repository.GetByNitAsync(request.Nit);
            if (existingByNit != null)
                throw new DuplicateEntityException("Ya existe un proveedor con este NIT");

            var existingByEmail = await _repository.GetByEmailAsync(request.Email);
            if (existingByEmail != null)
                throw new DuplicateEntityException("Ya existe un proveedor con este email");

            var provider = new Provider(request.Nit, request.Name, request.Email);

            if (request.CustomFields != null)
            {
                foreach (var field in request.CustomFields)
                {
                    provider.AddCustomField(field.Key);
                    provider.AssignCustomFieldValue(field.Key, field.Value);
                }
            }

          

            await _repository.AddAsync(provider);

            return ProviderResponse.From(provider);
        }
    }
}
