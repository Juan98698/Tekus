using System.Net.Http.Json;
using Tekus.Frontend.Models;

namespace Tekus.Frontend.Services
{
    public class CountryApiService
    {
        private readonly HttpClient _http;

        public CountryApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CountryDto>> GetCountriesAsync()
        {
            return await _http.GetFromJsonAsync<List<CountryDto>>("/api/countries")
                   ?? new();
        }
    }

}
