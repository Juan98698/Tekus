using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Tekus.Application.Interfaces.Repositories
{
    public interface IProveedorRepository
    {
        Task AddAsync(Proveedor proveedor);
        Task<Proveedor?> GetByIdAsync(int id);
        Task<Proveedor?> GetByNitAsync(string nit);
        Task UpdateAsync(Proveedor proveedor);
        Task<bool> ExistsAsync(int id);
    }
}
