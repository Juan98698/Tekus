using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.UseCases.Proveedores;
using Tekus.Domain.Exceptions;
using Xunit;

namespace Tekus.Tests.Unit.Application
{
    public class CrearProveedorTests
    {
        [Fact]
        public async Task No_debe_permitir_crear_proveedor_con_NIT_duplicado()
        {
            var repoMock = new Mock<IProveedorRepository>();
            repoMock.Setup(r => r.GetByNitAsync("123"))
                    .ReturnsAsync(new Tekus.Domain.Entities.Proveedor("123", "Test", "a@a.com"));

            var useCase = new CrearProveedorUseCase(repoMock.Object);

            var request = new CrearProveedorRequest
            {
                Nit = "123",
                Nombre = "Nuevo",
                Email = "nuevo@test.com"
            };

            await Assert.ThrowsAsync<DuplicateEntityException>(() =>
                useCase.ExecuteAsync(request)
            );
        }
    }
}
