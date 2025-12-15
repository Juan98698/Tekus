using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Interfaces.Services;
using Tekus.Domain.ValueObjects;
using Tekus.Infrastructure.ExternalServices.DTOs;


namespace Tekus.Infrastructure.ExternalServices
{
    public class RestCountriesProvider : ICountryProvider
    {
        private readonly HttpClient _httpClient;

        public RestCountriesProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<RestCountryDto>>(
                "https://restcountries.com/v3.1/all?fields=cca2,name");

            return response!.Select(r =>
                new Country(r.Cca2, r.Name.Common));
        }
    }
}
