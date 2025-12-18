using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.UseCases.Reports;
using Tekus.Domain.Entities;
using Tekus.Domain.ValueObjects;
using Tekus.Infrastructure.Persistence;
using Tekus.Infrastructure.Persistence.Context;

//probamos que EF Core guarda provider, service, Country
//y que el caso de uso GetSummaryReportUseCase funciona correctamente con una Db en memoria
namespace Tekus.Tests.Integration.Persistence
{
    public class SummaryReportInMemoryTests
    {
        [Fact]
        public async Task Should_generate_summary_from_inmemory_database()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var repository = new ProviderRepository(context);

            var provider = new Provider("123", "Provider Test", "test@test.com");
            var service = Service.Create("Hosting", 100);
            service.AddCountry(new Country("CO", "Colombia"));
            service.AddCountry(new Country("MX", "Mexico"));

            provider.AddService(service);

            context.Providers.Add(provider);
            await context.SaveChangesAsync();

            var useCase = new GetSummaryReportUseCase(repository);

            
            var result = await useCase.ExecuteAsync();

            
            Assert.Equal(2, result.ServicesByCountry.Count);
            Assert.Contains(result.ServicesByCountry, x => x.CountryCode == "CO");
            Assert.Contains(result.ServicesByCountry, x => x.CountryCode == "MX");
        }

        private static TekusDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TekusDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new TekusDbContext(options);
        }
    }

}
