using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.UseCases.Proveedores;
using Tekus.Domain.Entities;
using Xunit;

namespace Tekus.Tests.Unit.Application
{
    public class AgregarServicioAProveedorTests
    {
        [Fact]
        public async Task Debe_agregar_servicio_a_proveedor_existente()
        {
            var proveedor = new Proveedor("123", "Proveedor Test", "test@test.com");

            var repoMock = new Mock<IProveedorRepository>();
            repoMock.Setup(r => r.GetByIdAsync(1))
                    .ReturnsAsync(proveedor);

            var useCase = new AgregarServicioAProveedorUseCase(repoMock.Object);

            var request = new AgregarServicioRequest
            {
                ProveedorId = 1,
                Nombre = "Nuevo Servicio",
                ValorHoraUsd = 50
            };

            await useCase.ExecuteAsync(request);

            Assert.Single(proveedor.Servicios);
        }
    }
}
