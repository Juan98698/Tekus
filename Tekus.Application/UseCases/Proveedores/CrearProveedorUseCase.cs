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
    public class CrearProveedorUseCase
    {
        private readonly IProveedorRepository _proveedorRepository;

        public CrearProveedorUseCase(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<ProveedorResponse> ExecuteAsync(CrearProveedorRequest request)
        {
            var proveedorExistente = await _proveedorRepository.GetByNitAsync(request.Nit);
            if (proveedorExistente != null)
                throw new DuplicateEntityException("Proveedor");

            var proveedor = new Proveedor(
                request.Nit,
                request.Nombre,
                request.Email
            );

            await _proveedorRepository.AddAsync(proveedor);

            return new ProveedorResponse
            {
                Id = proveedor.Id,
                Nit = proveedor.Nit,
                Nombre = proveedor.Nombre,
                Email = proveedor.Email
            };
        }
    }
}
