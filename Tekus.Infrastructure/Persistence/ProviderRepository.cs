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

namespace Tekus.Infrastructure.Persistence
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly TekusDbContext _context;

        public ProviderRepository(TekusDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Provider provider)
        {
            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Provider provider)
        {
            _context.Providers.Update(provider);

            await _context.SaveChangesAsync();
        }

        public async Task<Provider?> GetByIdAsync(Guid id)
        {
            return await _context.Providers
                .Include(p => p.Services)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Provider?> GetByNitAsync(string nit)
        {
            return await _context.Providers
                .FirstOrDefaultAsync(p => p.Nit == nit);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Providers.AnyAsync(p => p.Id == id);
        }

        public async Task<PagedResult<Provider>> GetPagedAsync(PagedRequest request)
        {
            var query = _context.Providers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(p =>
                    p.Name.Contains(request.Search) ||
                    p.Nit.Contains(request.Search));
            }

            var total = await query.CountAsync();

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<Provider>
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = total,
                Items = items
            };
        }
    }
}
