using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Domain.Exceptions;

namespace Tekus.Application.UseCases
{
    public class DeleteProviderUseCase
    {
        private readonly IProviderRepository _repository;

        public DeleteProviderUseCase(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var provider = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Provider");

            await _repository.DeleteAsync(provider);
        }
    }
}
