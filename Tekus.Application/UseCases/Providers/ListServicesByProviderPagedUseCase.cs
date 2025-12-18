using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Application.Common;
using Tekus.Application.DTOs;

namespace Tekus.Application.UseCases.Providers
{
    public class ListServicesByProviderPagedUseCase
    {
        private readonly IProviderRepository _providerRepository;

        public ListServicesByProviderPagedUseCase(
            IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<PagedResult<ServiceListItemResponse>> ExecuteAsync(
            Guid providerId,
            PagedRequest request)
        {
            var result = await _providerRepository
                .GetServicesByProviderAsync(providerId, request);

            var items = result.Items
                .Select(s => new ServiceListItemResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    HourValueUsd = s.HourValueUsd,
                    Countries = s.Countries
                        .Select(c => new CountryResponse
                        {
                            Code = c.Code,
                            Name = c.Name
                        })
                        .ToList()
                })
                .ToList();

            return new PagedResult<ServiceListItemResponse>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                Items = items
            };
        }
    }

}     
