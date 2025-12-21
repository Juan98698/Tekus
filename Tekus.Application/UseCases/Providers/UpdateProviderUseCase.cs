using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Domain.Exceptions;

namespace Tekus.Application.UseCases.Providers
{
    public class UpdateProviderUseCase
    {
        private readonly IProviderRepository _repository;

        public UpdateProviderUseCase(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(UpdateProviderRequest request)
        {
            var provider = await _repository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Proveedor");

            var existing = await _repository.GetByNitAsync(request.Nit);
            if (existing != null && existing.Id != request.Id)
                throw new DuplicateEntityException(
                    "Ya existe un proveedor con este NIT"
                );

            var existingByEmail = await _repository.GetByEmailAsync(request.Email);
            if (existingByEmail != null && existingByEmail.Id != request.Id)
                throw new DuplicateEntityException("Ya existe un proveedor con este email");



            provider.UpdateBasicInfo(request.Nit, request.Name, request.Email);

            if (request.CustomFields != null)
                provider.ReplaceCustomFields(request.CustomFields);

            await _repository.UpdateAsync(provider);
        }
    }
}
