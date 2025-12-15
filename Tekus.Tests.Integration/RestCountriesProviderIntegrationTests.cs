using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Infrastructure.ExternalServices;


//probamos que nuestra implementacion de RestCountriesProvider
//puede consumir correctamente el servicio externo de países
//y mapear la respuesta a nuestro modelo de Country
namespace Tekus.Tests.Integration
{
    public class RestCountriesProviderIntegrationTests
    {
        [Fact]
        public async Task Should_get_countries_from_external_api()
        {
            var httpClient = new HttpClient();
            var provider = new RestCountriesProvider(httpClient);

            var countries = await provider.GetAllAsync();

            Assert.NotEmpty(countries);
            Assert.Contains(countries, c => c.Code == "CO");
        }
    }
}
