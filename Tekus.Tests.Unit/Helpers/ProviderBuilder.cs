using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;
using Tekus.Domain.ValueObjects;

namespace Tekus.Tests.Unit.Helpers
{
    public static class ProviderBuilder
    {
        public static Provider WithService(out Service service)
        {
            var provider = new Provider("123", "Test Provider", "test@test.com");

            service = Service.Create("Hosting", 100); // 👈 ASIGNAS el out
            provider.AddService(service);

            return provider;
        }

        public static Provider WithServiceAndCountry(string countryCode)
        {
            var provider = WithService(out var service);

            service.AddCountry(new Country(countryCode, "Country"));

            return provider;
        }
    }
}
