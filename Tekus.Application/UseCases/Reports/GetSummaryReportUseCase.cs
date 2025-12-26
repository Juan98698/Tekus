using Tekus.Application.Common;
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

            var providersByCountry = providers
                .SelectMany(p =>
                    p.Services
                     .SelectMany(s => s.Countries)
                     .Select(c => new { p.Id, c.Code })
                     .Distinct()
                )
                .GroupBy(x => x.Code)
                .Select(g => new CountryCountResponse
                {
                    CountryCode = g.Key,
                    Count = g.Select(x => x.Id).Distinct().Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            return new SummaryReportResponse
            {
                ServicesByCountry = servicesByCountry,
                ProvidersByCountry = providersByCountry
            };
        }


        //  MÉTODO DE ORDENAMIENTO
    
        private static IQueryable<CountryCountResponse> ApplyOrdering(
            IQueryable<CountryCountResponse> query,
            PagedRequest request)
        {
            return request.OrderBy?.ToLower() switch
            {
                "countrycode" =>
                    request.OrderAsc
                        ? query.OrderBy(x => x.CountryCode)
                        : query.OrderByDescending(x => x.CountryCode),

                "count" or _ =>
                    request.OrderAsc
                        ? query.OrderBy(x => x.Count)
                        : query.OrderByDescending(x => x.Count)
            };
        }
    }
}