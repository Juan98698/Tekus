using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.DTOs.Reports;
using Tekus.Application.Interfaces.Repositories;

namespace Tekus.Application.UseCases.Reports
{
    public class GetSummaryReportUseCase
    {
        private readonly IProviderRepository _providerRepository;

        public GetSummaryReportUseCase(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<SummaryReportResponse> ExecuteAsync()
        {
            var providers = await _providerRepository.GetAllAsync();

            // Servicios por país
            var servicesByCountry = providers
                .SelectMany(p => p.Services)
                .SelectMany(s => s.Countries)
                .GroupBy(c => c.Code)
                .Select(g => new CountryCountResponse
                {
                    CountryCode = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            // Proveedores por país
            var providersByCountry = providers
                .SelectMany(p => p.Services)
                .SelectMany(s => s.Countries)
                .GroupBy(c => c.Code)
                .Select(g => new CountryCountResponse
                {
                    CountryCode = g.Key,
                    Count = g
                        .Select(sc => sc) // país
                        .Distinct()
                        .Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            return new SummaryReportResponse
            {
                ServicesByCountry = servicesByCountry,
                ProvidersByCountry = providersByCountry
            };
        }
    }
}
