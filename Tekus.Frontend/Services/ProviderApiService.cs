namespace Tekus.Frontend.Services
{
    using System.Net.Http.Json;
    using Tekus.Frontend.Models;

    public class ProviderApiService
    {
        private readonly HttpClient _http;

        public ProviderApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<PagedResult<ProviderDto>> GetProvidersAsync(
         int page = 1,
         int pageSize = 10)
        {
            return await _http.GetFromJsonAsync<PagedResult<ProviderDto>>(
                $"api/providers?page={page}&pageSize={pageSize}");
        }

        public async Task CreateProviderAsync(CreateProviderRequest request)
        {
            await _http.PostAsJsonAsync("api/providers", request);
        }

        public async Task AddServiceAsync(Guid providerId, AddServiceRequest request)
        {
            await _http.PostAsJsonAsync(
                $"api/providers/{providerId}/services", request);
        }
    }

}
