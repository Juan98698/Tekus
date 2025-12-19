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
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadFromJsonAsync<ProviderDto>();
            }

        public async Task UpdateProviderAsync(ProviderDto provider)
        {
            var response = await _http.PutAsJsonAsync(
                $"api/providers/{provider.Id}",
                provider
            );

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProviderAsync(Guid providerId)
        {
            var response = await _http.DeleteAsync(
                $"api/providers/{providerId}"
            );

            response.EnsureSuccessStatusCode();
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

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteServiceAsync(
        Guid providerId,
        Guid serviceId)
        {
            var response = await _http.DeleteAsync(
                $"api/providers/{providerId}/services/{serviceId}"
            );

            response.EnsureSuccessStatusCode();
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

            response.EnsureSuccessStatusCode();
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

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ServiceDto>()
                   ?? throw new Exception("Error creating service");
             }

    }
    }