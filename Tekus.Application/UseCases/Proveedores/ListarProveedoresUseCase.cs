using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Common;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;

namespace Tekus.Application.UseCases.Proveedores
{
    public class ListarProveedoresUseCase
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ListarProveedoresUseCase(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<PagedResult<ProveedorListItemResponse>> ExecuteAsync(PagedRequest request)
        {
            var result = await _proveedorRepository.GetPagedAsync(request);

            return new PagedResult<ProveedorListItemResponse>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                Items = result.Items.Select(p => new ProveedorListItemResponse
                {
                    Id = p.Id,
                    Nit = p.Nit,
                    Nombre = p.Nombre,
                    Email = p.Email
                }).ToList()
            };
        }
    }
}
