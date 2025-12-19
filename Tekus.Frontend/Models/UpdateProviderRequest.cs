namespace Tekus.Frontend.Models
{
    public class UpdateProviderRequest
    {
        public Guid Id { get; set; }
        public string Nit { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Dictionary<string, string> CustomFields { get; set; } = new();
    }
}
