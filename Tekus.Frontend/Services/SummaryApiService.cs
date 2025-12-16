using System.Net.Http.Json;
using Tekus.Application.DTOs.Summary;
namespace Tekus.Frontend.Services
{
    public class SummaryApiService
    {
        private readonly HttpClient _http;

        public SummaryApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<SummaryResponse> GetSummaryAsync()
        {
            return await _http.GetFromJsonAsync<SummaryResponse>("/api/summary")
                   ?? new();
        }
    }
}
