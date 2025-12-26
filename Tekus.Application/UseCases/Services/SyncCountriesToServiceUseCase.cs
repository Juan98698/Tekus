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
    public class SyncCountriesToServiceUseCase
    {
        private readonly IProviderRepository _providerRepository;
        private readonly ICountryProvider _countryProvider;

        public SyncCountriesToServiceUseCase(
            IProviderRepository providerRepository,
            ICountryProvider countryProvider)
        {
            _providerRepository = providerRepository;
            _countryProvider = countryProvider;
        }

        public async Task ExecuteAsync(SyncCountriesToServiceRequest request)
        {
            var provider = await _providerRepository.GetByIdAsync(request.ProviderId)
                ?? throw new NotFoundException("Provider");

            var service = provider.Services
                .FirstOrDefault(s => s.Id == request.ServiceId)
                ?? throw new NotFoundException("Service");

            var allCountries = await _countryProvider.GetAllAsync();

            //  sincronización real (no solo agregar)
            service.ClearCountries();

            foreach (var code in request.CountryCodes)
            {
                var country = allCountries.FirstOrDefault(c => c.Code == code)
                    ?? throw new NotFoundException($"Country {code}");

                service.AddCountry(country);
            }

            await _providerRepository.UpdateAsync(provider);
        }
    }
}
