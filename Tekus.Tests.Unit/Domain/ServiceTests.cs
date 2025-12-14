using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;
using Tekus.Domain.ValueObjects;
using Tekus.Domain.Exceptions;
using Xunit;

namespace Tekus.Tests.Unit.Domain
{
    public class serviceTests
    {
        [Fact]
        public void Do_not_allow_adding_duplicate_countries()
        {
            // Arrange
            var service = new Service("Descarga espacial", 100);
            service.AddCountry(new Country("CO", "Colombia"));

            // Act & Assert
            Assert.Throws<DuplicateEntityException>(() =>
                service.AddCountry(new Country("CO", "Colombia"))
            );
        }
    }
}
