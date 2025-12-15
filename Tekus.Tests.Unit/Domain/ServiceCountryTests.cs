using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;
using Tekus.Domain.Exceptions;
using Tekus.Domain.ValueObjects;

//Probamos no duplicar paises, agregar un pais valido, dominio protegido, invariantes claras
namespace Tekus.Tests.Unit.Domain
{
    public class ServiceCountryTests
    {
        [Fact]
        public void Should_add_country_to_service()
        {
            var service = new Service("Hosting", 100);
            var country = new Country("CO", "Colombia");

            service.AddCountry(country);

            Assert.Single(service.Countries);
        }

        [Fact]
        public void Should_not_allow_duplicate_country()
        {
            var service = new Service("Hosting", 100);
            var country = new Country("CO", "Colombia");

            service.AddCountry(country);

            Assert.Throws<DuplicateEntityException>(() =>
                service.AddCountry(new Country("CO", "Colombia")));
        }
    }
}
