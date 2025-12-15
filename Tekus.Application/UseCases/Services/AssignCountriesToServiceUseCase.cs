using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.Interfaces.Services;
using Tekus.Domain.Exceptions;

namespace Tekus.Application.UseCases.Services
{
    public class AssignCountriesToServiceUseCase
    {
        private readonly IProviderRepository _providerRepository;
        private readonly ICountryProvider _countryProvider;

        public AssignCountriesToServiceUseCase(
            IProviderRepository providerRepository,
            ICountryProvider countryProvider)
        {
            _providerRepository = providerRepository;
            _countryProvider = countryProvider;
        }

        public async Task ExecuteAsync(AssignCountriesToServiceRequest request)
        {
            var provider = await _providerRepository.GetByIdAsync(request.ProviderId)
                ?? throw new NotFoundException("Provider");

            var service = provider.Services
                .FirstOrDefault(s => s.Id == request.ServiceId)
                ?? throw new NotFoundException("Service");

            var countries = await _countryProvider.GetAllAsync();

            foreach (var code in request.CountryCodes)
            {
                var country = countries.FirstOrDefault(c => c.Code == code)
                    ?? throw new NotFoundException($"Country {code}");

                service.AddCountry(country);
            }

            await _providerRepository.UpdateAsync(provider);
        }
    }
}
