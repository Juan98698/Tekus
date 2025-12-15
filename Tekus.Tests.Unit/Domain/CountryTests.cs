using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.ValueObjects;

//probamos validacion, Inmutabilidad implicita y Normalizacion
namespace Tekus.Tests.Unit.Domain
{
    public class CountryTests
    {
        [Fact]
        public void Should_create_valid_country()
        {
            var country = new Country("co", "Colombia");

            Assert.Equal("CO", country.Code);
            Assert.Equal("Colombia", country.Name);
        }

        [Fact]
        public void Should_not_allow_empty_code()
        {
            Assert.Throws<ArgumentException>(() =>
                new Country("", "Colombia"));
        }
    }
}
