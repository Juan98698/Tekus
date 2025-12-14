using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Common;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.UseCases.Providers;
using Tekus.Domain.Entities;

namespace Tekus.Tests.Unit.Application
{
    public class ListProvidersTests
    {
        [Fact]
        public async Task must_list_paginated_providers()
        {
            var providers = new List<Provider>
        {
            new Provider("1", "A", "a@test.com"),
            new Provider("2", "B", "b@test.com")
        };

            var paged = new PagedResult<Provider>
            {
                Page = 1,
                PageSize = 10,
                TotalItems = 2,
                Items = providers
            };

            var repoMock = new Mock<IProviderRepository>();
            repoMock.Setup(r => r.GetPagedAsync(It.IsAny<PagedRequest>()))
                    .ReturnsAsync(paged);

            var useCase = new ListProvidersUseCase(repoMock.Object);

            var result = await useCase.ExecuteAsync(new PagedRequest());

            Assert.Equal(2, result.TotalItems);
            Assert.Equal(2, result.Items.Count);
        }
    }

}
