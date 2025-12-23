using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Common;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;

namespace Tekus.Application.UseCases.Services
{
    public class ListServicesPagedUseCase
    {
        private readonly IServiceRepository _repository;

        public ListServicesPagedUseCase(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<ServiceGlobalListItemResponse>> ExecuteAsync(
            PagedRequest request)
        {
            var result = await _repository.GetPagedAsync(request);

            return new PagedResult<ServiceGlobalListItemResponse>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                Items = result.Items.Select(s => new ServiceGlobalListItemResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    HourValueUsd = s.HourValueUsd,
                    ProviderId = s.Provider.Id,
                    ProviderName = s.Provider.Name,
                    Countries = s.Countries.Select(c => new CountryResponse
                    {
                        Code = c.Code,
                        Name = c.Name
                    }).ToList()
                }).ToList()
            };
        }
    }
}
