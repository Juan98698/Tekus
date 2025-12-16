namespace Tekus.Frontend.Models
{
    public class AddServiceRequest
    {
        public string Name { get; set; }
        public decimal HourValueUsd { get; set; }
        public List<string> CountryCodes { get; set; } = new();
    }
}
