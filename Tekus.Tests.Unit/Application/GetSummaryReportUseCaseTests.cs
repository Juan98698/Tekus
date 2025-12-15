using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.UseCases.Reports;
using Tekus.Domain.Entities;
using Tekus.Tests.Unit.Helpers;


//Probamos Calculo correcto del reporte y agrupacion por pais
namespace Tekus.Tests.Unit.Application
{
    public class GetSummaryReportUseCaseTests
    {
        [Fact]
        public async Task Should_return_services_by_country()
        {
            var provider = ProviderBuilder.WithServiceAndCountry("CO");

            var repoMock = new Mock<IProviderRepository>();
            repoMock.Setup(r => r.GetAllAsync())
                    .ReturnsAsync(new List<Provider> { provider });

            var useCase = new GetSummaryReportUseCase(repoMock.Object);

            var result = await useCase.ExecuteAsync();

            Assert.Single(result.ServicesByCountry);
            Assert.Equal("CO", result.ServicesByCountry[0].CountryCode);
            Assert.Equal(1, result.ServicesByCountry[0].Count);
        }
    }
}
