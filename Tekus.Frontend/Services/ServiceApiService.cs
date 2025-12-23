using System.Net.Http;
using System.Net.Http.Json;
using Tekus.Frontend.Models;
using Tekus.Frontend.Models.Common;
namespace Tekus.Frontend.Services
{
    public class ServiceApiService
    {
        private readonly HttpClient _http;

        public ServiceApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<PagedResult<ServiceDto>> GetServicesAsync(
            int page,
            int pageSize,
            string search,
            string orderBy,
            bool orderAsc)
        {
            var url =
                $"api/services?" +
                $"page={page}" +
                $"&pageSize={pageSize}" +
                $"&search={search}" +
                $"&orderBy={orderBy}" +
                $"&orderAsc={orderAsc}";

            return await _http.GetFromJsonAsync<PagedResult<ServiceDto>>(url)
                   ?? new PagedResult<ServiceDto>();
        }
    }
}
