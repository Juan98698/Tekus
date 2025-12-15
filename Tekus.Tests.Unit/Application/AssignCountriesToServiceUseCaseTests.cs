using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.Interfaces.Services;
using Tekus.Application.UseCases.Services;
using Tekus.Domain.Exceptions;
using Tekus.Domain.ValueObjects;
using Tekus.Tests.Unit.Helpers;

//probamos el uso del servicio externo(mock), rechaza paises inexistentes y asigna los validos
namespace Tekus.Tests.Unit.Application
{
    public class AssignCountriesToServiceUseCaseTests
    {
        [Fact]
        public async Task Should_assign_valid_country()
        {
            var provider = ProviderBuilder.WithService(out var service);

            var repoMock = new Mock<IProviderRepository>();
            repoMock.Setup(r => r.GetByIdAsync(provider.Id))
                    .ReturnsAsync(provider);

            var countryProviderMock = new Mock<ICountryProvider>();
            countryProviderMock.Setup(c => c.GetAllAsync())
                .ReturnsAsync(new List<Country>
                {
                new Country("CO", "Colombia")
                });

            var useCase = new AssignCountriesToServiceUseCase(
                repoMock.Object,
                countryProviderMock.Object);

            await useCase.ExecuteAsync(new AssignCountriesToServiceRequest
            {
                ProviderId = provider.Id,
                ServiceId = service.Id,
                CountryCodes = new() { "CO" }
            });

            Assert.Single(service.Countries);
        }

        [Fact]
        public async Task Should_reject_invalid_country_code()
        {
            var provider = ProviderBuilder.WithService(out var service);

            var repoMock = new Mock<IProviderRepository>();
            repoMock.Setup(r => r.GetByIdAsync(provider.Id))
                    .ReturnsAsync(provider);

            var countryProviderMock = new Mock<ICountryProvider>();
            countryProviderMock.Setup(c => c.GetAllAsync())
                .ReturnsAsync(new List<Country>
                {
                new Country("CO", "Colombia")
                });

            var useCase = new AssignCountriesToServiceUseCase(
                repoMock.Object,
                countryProviderMock.Object);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                useCase.ExecuteAsync(new AssignCountriesToServiceRequest
                {
                    ProviderId = provider.Id,
                    ServiceId = service.Id,
                    CountryCodes = new() { "XX" }
                }));
        }
    }
}
