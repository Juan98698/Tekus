using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Common;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;

namespace Tekus.Application.UseCases.Providers
{
    public class ListProvidersUseCase
    {
        private readonly IProviderRepository _providerRepository;

        public ListProvidersUseCase(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<PagedResult<ProviderListItemResponse>> ExecuteAsync(PagedRequest request)
        {
            var result = await _providerRepository.GetPagedAsync(request);

            return new PagedResult<ProviderListItemResponse>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                Items = result.Items.Select(p => new ProviderListItemResponse
                {
                    Id = p.Id,
                    Nit = p.Nit,
                    Name = p.Name,
                    Email = p.Email
                }).ToList()
            };
        }
    }
}
