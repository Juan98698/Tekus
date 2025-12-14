using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Tekus.Domain.Entities;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.UseCases.Providers;
using Tekus.Domain.Exceptions;
using Xunit;

namespace Tekus.Tests.Unit.Application
{
    public class CreateProviderTests
    {
        [Fact]
        public async Task Do_not_allow_creating_a_provider_with_a_duplicate_NIT()
        {
            var repoMock = new Mock<IProviderRepository>();
            repoMock.Setup(r => r.GetByNitAsync("123"))
                    .ReturnsAsync(new Tekus.Domain.Entities.Provider("123", "Test", "a@a.com"));

            var useCase = new CreateProviderUseCase(repoMock.Object);

            var request = new CreateProviderRequest
            {
                Nit = "123",
                Name = "Nuevo",
                Email = "nuevo@test.com"
            };

            await Assert.ThrowsAsync<DuplicateEntityException>(() =>    
                useCase.ExecuteAsync(request)
            );
                    repoMock.Verify(r => r.AddAsync(It.IsAny<Provider>()), Times.Never);

        }
    }
}
