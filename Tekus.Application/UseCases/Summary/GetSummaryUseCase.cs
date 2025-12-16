using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.DTOs.Summary;
using Tekus.Application.Interfaces.Repositories;

namespace Tekus.Application.UseCases.Summary
{
    public class GetSummaryUseCase
    {
        private readonly IProviderRepository _providerRepository;

        public GetSummaryUseCase(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<SummaryResponse> ExecuteAsync()
        {
            var providers = await _providerRepository.GetAllAsync();

            var servicesByCountry = providers
                .SelectMany(p => p.Services)
                .SelectMany(s => s.Countries)
                .GroupBy(c => new { c.Code, c.Name })
                .Select(g => new CountryServicesSummaryDto
                {
                    CountryCode = g.Key.Code,
                    CountryName = g.Key.Name,
                    ServicesCount = g.Count()
                })
                .OrderByDescending(x => x.ServicesCount)
                .ToList();

            var providersByCountry = providers
                .SelectMany(p =>
                    p.Services
                     .SelectMany(s => s.Countries)
                     .Select(c => new { p.Id, c.Code, c.Name })
                     .Distinct())
                .GroupBy(x => new { x.Code, x.Name })
                .Select(g => new CountryProvidersSummaryDto
                {
                    CountryCode = g.Key.Code,
                    CountryName = g.Key.Name,
                    ProvidersCount = g.Select(x => x.Id).Distinct().Count()
                })
                .OrderByDescending(x => x.ProvidersCount)
                .ToList();

            return new SummaryResponse
            {
                ServicesByCountry = servicesByCountry,
                ProvidersByCountry = providersByCountry
            };
        }
    }

}
