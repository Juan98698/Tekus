using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Domain.Entities;
using Tekus.Domain.Exceptions;

namespace Tekus.Application.UseCases.Proveedores
{
    public class AgregarServicioAProveedorUseCase
    {
        private readonly IProveedorRepository _proveedorRepository;

        public AgregarServicioAProveedorUseCase(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task ExecuteAsync(AgregarServicioRequest request)
        {
            var proveedor = await _proveedorRepository.GetByIdAsync(request.ProveedorId);

            if (proveedor == null)
                throw new NotFoundException("Proveedor");
            var servicio = new Servicio(
                request.Nombre,
                request.ValorHoraUsd
            );

            proveedor.AgregarServicio(servicio);

            await _proveedorRepository.UpdateAsync(proveedor);
        }
    }
}
