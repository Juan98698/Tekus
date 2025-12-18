namespace Tekus.Frontend.Models.Summary
{
    public class SummaryResponse
    {
        public List<CountryServicesSummaryDto> ServicesByCountry { get; set; } = new();
        public List<CountryProvidersSummaryDto> ProvidersByCountry { get; set; } = new();
    }
}
