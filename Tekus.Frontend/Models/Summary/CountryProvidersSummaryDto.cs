namespace Tekus.Frontend.Models.Summary
{
    public class CountryProvidersSummaryDto
    {
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public int ProvidersCount { get; set; }
    }
}
