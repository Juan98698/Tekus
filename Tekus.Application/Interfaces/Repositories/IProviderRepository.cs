using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;
using Tekus.Application.Common;

namespace Tekus.Application.Interfaces.Repositories
{
    public interface IProviderRepository
    {
        Task AddAsync(Provider provider);
        Task<Provider> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<Provider> GetByNitAsync(string nit);
        Task UpdateAsync(Provider provider);    
        Task<PagedResult<Provider>> GetPagedAsync(PagedRequest request);
        Task DeleteAsync(Provider provider);


    }
}
