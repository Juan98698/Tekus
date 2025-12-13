using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Common;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.UseCases.Proveedores;
using Tekus.Domain.Entities;

namespace Tekus.Tests.Unit.Application
{
    public class ListarProveedoresTests
    {
        [Fact]
        public async Task Debe_listar_proveedores_paginados()
        {
            var proveedores = new List<Proveedor>
        {
            new Proveedor("1", "A", "a@test.com"),
            new Proveedor("2", "B", "b@test.com")
        };

            var paged = new PagedResult<Proveedor>
            {
                Page = 1,
                PageSize = 10,
                TotalItems = 2,
                Items = proveedores
            };

            var repoMock = new Mock<IProveedorRepository>();
            repoMock.Setup(r => r.GetPagedAsync(It.IsAny<PagedRequest>()))
                    .ReturnsAsync(paged);

            var useCase = new ListarProveedoresUseCase(repoMock.Object);

            var result = await useCase.ExecuteAsync(new PagedRequest());

            Assert.Equal(2, result.TotalItems);
            Assert.Equal(2, result.Items.Count);
        }
    }

}
