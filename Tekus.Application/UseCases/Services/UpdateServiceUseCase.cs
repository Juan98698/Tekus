using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Domain.Exceptions;

namespace Tekus.Application.UseCases.Services
{
    public class UpdateServiceUseCase
    {
        private readonly IProviderRepository _repository;

        public UpdateServiceUseCase(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(UpdateServiceRequest request)
        {
            var provider = await _repository.GetByIdAsync(request.ProviderId)
                ?? throw new NotFoundException("Provider");

            var service = provider.Services
                .FirstOrDefault(s => s.Id == request.ServiceId)
                ?? throw new NotFoundException("Service");

            service.Update(request.Name, request.HourValueUsd);

            await _repository.UpdateAsync(provider);
        }
    }
}
