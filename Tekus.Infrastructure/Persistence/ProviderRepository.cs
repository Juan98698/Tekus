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
           

            await _context.SaveChangesAsync();
        }

        public async Task<Provider?> GetByIdAsync(Guid id)
        {
            return await _context.Providers
                .Include(p => p.Services)
                    .ThenInclude(s => s.Countries)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<PagedResult<Service>> GetServicesByProviderAsync(
    Guid providerId,
    PagedRequest request)
        {
            var query = _context.Providers
                .Where(p => p.Id == providerId)
                .SelectMany(p => p.Services)
                .Include(s => s.Countries)
                .AsQueryable();

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
        public async Task DeleteAsync(Provider provider)
        {
            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();
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


            
            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                switch (request.OrderBy.ToLower())
                {
                    case "name":
                        query = request.OrderAsc
                            ? query.OrderBy(p => p.Name)
                            : query.OrderByDescending(p => p.Name);
                        break;

                    case "nit":
                        query = request.OrderAsc
                            ? query.OrderBy(p => p.Nit)
                            : query.OrderByDescending(p => p.Nit);
                        break;

                    case "email":
                        query = request.OrderAsc
                            ? query.OrderBy(p => p.Email)
                            : query.OrderByDescending(p => p.Email);
                        break;

                    default:
                        query = query.OrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                
                query = query.OrderBy(p => p.Name);
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

        public async Task<List<Provider>> GetAllAsync()
        {
            return await _context.Providers
                .Include(p => p.Services)
                    .ThenInclude(s => s.Countries)
                .ToListAsync();
        }
        public async Task<Provider?> GetByEmailAsync(string email)
        {
            return await _context.Providers
                .FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
