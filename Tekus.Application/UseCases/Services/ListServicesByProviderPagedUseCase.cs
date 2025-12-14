using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Common;
using Tekus.Application.DTOs;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Domain.Exceptions;

namespace Tekus.Application.UseCases.Services
{
    public class ListServicesByProviderPagedUseCase
    {
        private readonly IProviderRepository _repository;

        public ListServicesByProviderPagedUseCase(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<ServiceListItemResponse>> ExecuteAsync(
            Guid providerId,
            PagedRequest request)
        {
            var provider = await _repository.GetByIdAsync(providerId)
                ?? throw new NotFoundException("Provider");

            var query = provider.Services.AsQueryable();

            //  Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(s =>
                    s.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
            }

            //  Ordering
            query = request.OrderBy?.ToLower() switch
            {
                "name" => request.OrderAsc
                    ? query.OrderBy(s => s.Name)
                    : query.OrderByDescending(s => s.Name),

                "hourvalueusd" => request.OrderAsc
                    ? query.OrderBy(s => s.HourValueUsd)
                    : query.OrderByDescending(s => s.HourValueUsd),

                _ => query.OrderBy(s => s.Name)
            };

            var totalItems = query.Count();

            var items = query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(s => new ServiceListItemResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    HourValueUsd = s.HourValueUsd
                })
                .ToList();

            return new PagedResult<ServiceListItemResponse>
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = totalItems,
                Items = items
            };
        }
    }
}
