using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tekus.Application.Common;
using Tekus.Application.Interfaces.Repositories;
using Tekus.Domain.Entities;
using Tekus.Infrastructure.Persistence.Context;

namespace Tekus.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly TekusDbContext _context;

        public ServiceRepository(TekusDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Service>> GetPagedAsync(PagedRequest request)
        {
            var query = _context.Services
                .Include(s => s.Provider)
                .Include(s => s.Countries)
                .AsQueryable();

            //  Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(s =>
                    s.Name.Contains(request.Search) ||
                    s.Provider.Name.Contains(request.Search));
            }

            //  Order
            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                switch (request.OrderBy.ToLower())
                {
                    case "name":
                        query = request.OrderAsc
                            ? query.OrderBy(s => s.Name)
                            : query.OrderByDescending(s => s.Name);
                        break;

                    case "hourvalueusd":
                        query = request.OrderAsc
                            ? query.OrderBy(s => s.HourValueUsd)
                            : query.OrderByDescending(s => s.HourValueUsd);
                        break;

                    case "provider":
                        query = request.OrderAsc
                            ? query.OrderBy(s => s.Provider.Name)
                            : query.OrderByDescending(s => s.Provider.Name);
                        break;

                    default:
                        query = query.OrderBy(s => s.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(s => s.Name);
            }

            var total = await query.CountAsync();

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<Service>
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = total,
                Items = items
            };
        }
    }
}
