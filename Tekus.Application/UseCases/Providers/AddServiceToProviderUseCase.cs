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
        private readonly IProviderRepository _repository;

        public AddServiceToProviderUseCase(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(AddServiceRequest request)
        {
            var provider = await _repository.GetByIdAsync(request.ProviderId);

            if (provider == null)
                throw new NotFoundException("Provider");

            var service = new Service(request.Name, request.HourValueUsd);

            provider.AddService(service);

            await _repository.UpdateAsync(provider);
        }
    }
}
