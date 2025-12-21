    using System.Net.Http;
    using System.Net.Http.Json;
    using Tekus.Frontend.Models;
    using System.Text.Json;
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

        public async Task<PagedResult<ServiceDto>> GetServicesByProviderAsync(
        Guid providerId,
        int page = 1,
        int pageSize = 10)
        {
            return await _http.GetFromJsonAsync<PagedResult<ServiceDto>>(
                $"api/providers/{providerId}/services?page={page}&pageSize={pageSize}"
            ) ?? new();
        }


        public async Task<ProviderDto> CreateProviderAsync(CreateProviderRequest request)
            {
                var resp = await _http.PostAsJsonAsync("api/providers", request);
            await EnsureSuccessAsync(resp);
            return await resp.Content.ReadFromJsonAsync<ProviderDto>();
            }

        public async Task UpdateProviderAsync(UpdateProviderRequest provider)
        {
            var response = await _http.PutAsJsonAsync(
                $"api/providers/{provider.Id}",
                provider
            );

            await EnsureSuccessAsync(response);
        }

        public async Task DeleteProviderAsync(Guid providerId)
        {
            var response = await _http.DeleteAsync(
                $"api/providers/{providerId}"
            );

            await EnsureSuccessAsync(response);
        }

        public async Task UpdateServiceAsync(
        Guid providerId,
        Guid serviceId,
        UpdateServiceRequest request)
        {
            var response = await _http.PutAsJsonAsync(
                $"api/providers/{providerId}/services/{serviceId}",
                request
            );

            await EnsureSuccessAsync(response);
        }

        public async Task DeleteServiceAsync(
        Guid providerId,
        Guid serviceId)
        {
            var response = await _http.DeleteAsync(
                $"api/providers/{providerId}/services/{serviceId}"
            );

            await EnsureSuccessAsync(response);
        }
        public async Task SyncCountriesAsync(
    Guid providerId,
    Guid serviceId,
    List<string> countryCodes)
        {
            var response = await _http.PutAsJsonAsync(
                $"api/providers/{providerId}/services/{serviceId}/countries",
                countryCodes
            );

            await EnsureSuccessAsync(response);
        }


        public async Task AssignCountriesAsync(
            Guid providerId,
            Guid serviceId,
            List<string> countryCodes)
             {
            var response = await _http.PostAsJsonAsync(
                $"/api/providers/{providerId}/services/{serviceId}/countries",
                countryCodes
            );

             response.EnsureSuccessStatusCode();
             }
        public async Task<ServiceDto> GetServiceByIdAsync(
         Guid providerId,
         Guid serviceId)
        {
            return await _http.GetFromJsonAsync<ServiceDto>(
                $"api/providers/{providerId}/services/{serviceId}"
            ) ?? throw new Exception("Service not found");
        }

        public async Task<ServiceDto> AddServiceAsync(
            Guid providerId,
            AddServiceRequest request)
             {
            var response = await _http.PostAsJsonAsync(
                $"/api/providers/{providerId}/services",
                request
              );

            await EnsureSuccessAsync(response);

            return await response.Content.ReadFromJsonAsync<ServiceDto>()
                   ?? throw new Exception("Error creating service");
             }


        private async Task EnsureSuccessAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            var content = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(content))
            {
                try
                {
                    var json = JsonDocument.Parse(content);

                    if (json.RootElement.TryGetProperty("error", out var error))
                    {
                        throw new HttpRequestException(
                            error.GetString(),
                            null,
                            response.StatusCode
                        );
                    }
                }
                catch (JsonException)
                {
                    // Ignorar JSON inválido
                }
            }

                         throw new HttpRequestException(
                         $"Error HTTP {(int)response.StatusCode}",
                             null,
                                 response.StatusCode
                                );
        }

    }
    }