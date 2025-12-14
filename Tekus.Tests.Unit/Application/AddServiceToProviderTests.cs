using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.UseCases.Providers;
using Tekus.Domain.Entities;
using Tekus.Domain.Exceptions;
using Xunit;
using System.Linq;


namespace Tekus.Tests.Unit.Application
{
    public class AddServiceToProviderTests
    {
        [Fact]
        public async Task You_must_add_service_to_an_existing_provider()
        {
            var provider = new Provider("123", "Proveedor Test", "test@test.com");

            var repoMock = new Mock<IProviderRepository>();
            repoMock.Setup(r => r.GetByIdAsync(provider.Id))
        .ReturnsAsync(provider);



            var useCase = new AddServiceToProviderUseCase(repoMock.Object);

            var request = new AddServiceRequest
            {
                ProviderId = provider.Id,
                Name = "Nuevo servicio",
                HourValueUsd = 50
            };

            await useCase.ExecuteAsync(request);

            Assert.Single(provider.Services);
        }

            


    }
}

