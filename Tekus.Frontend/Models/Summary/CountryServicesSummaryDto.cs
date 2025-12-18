namespace Tekus.Frontend.Models.Summary
{
    public class CountryServicesSummaryDto
    {
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public int ServicesCount { get; set; }
    }
}
