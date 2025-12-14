using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Domain.Exceptions;

namespace Tekus.Application.UseCases.Services
{
    public class DeleteServiceUseCase
    {
        private readonly IProviderRepository _repository;

        public DeleteServiceUseCase(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid providerId, Guid serviceId)
        {
            var provider = await _repository.GetByIdAsync(providerId)
                ?? throw new NotFoundException("Provider");

            provider.RemoveService(serviceId);

            await _repository.UpdateAsync(provider);
        }
    }
}
