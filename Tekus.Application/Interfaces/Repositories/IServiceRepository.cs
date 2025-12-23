using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Application.Common;
using Tekus.Domain.Entities;

namespace Tekus.Application.Interfaces.Repositories
{
    public interface IServiceRepository
    {
        Task<PagedResult<Service>> GetPagedAsync(PagedRequest request);
    }
}
