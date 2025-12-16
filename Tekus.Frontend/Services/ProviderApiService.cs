    using System.Net.Http;
    using System.Net.Http.Json;
    using Tekus.Frontend.Models;

    namespace Tekus.Frontend.Services
    {
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

            public async Task<ProviderDto> CreateProviderAsync(CreateProviderRequest request)
            {
                var resp = await _http.PostAsJsonAsync("api/providers", request);
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadFromJsonAsync<ProviderDto>();
            }

            public async Task AddServiceAsync(Guid providerId, AddServiceRequest request)
            {
                var resp = await _http.PostAsJsonAsync(
                    $"api/providers/{providerId}/services", request);

                resp.EnsureSuccessStatusCode();
            }
        }
    }