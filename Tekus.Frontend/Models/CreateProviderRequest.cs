namespace Tekus.Frontend.Models
{
    public class CreateProviderRequest
    {
        public string Nit { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> CustomFields { get; set; } = new();
    }

}
