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
    public class ServicioTests
    {
        [Fact]
        public void No_debe_permitir_agregar_paises_duplicados()
        {
            // Arrange
            var servicio = new Servicio("Descarga espacial", 100);
            servicio.AgregarPais(new Pais("CO", "Colombia"));

            // Act & Assert
            Assert.Throws<DuplicateEntityException>(() =>
                servicio.AgregarPais(new Pais("CO", "Colombia"))
            );
        }
    }
}
