namespace Tekus.Frontend.Models
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal HourValueUsd { get; set; }

        public List<CountryDto> Countries { get; set; } = new();
    }
}
