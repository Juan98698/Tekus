using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.ValueObjects;

namespace Tekus.Application.Interfaces.Services
{
    public interface ICountryProvider
    {
        Task<IEnumerable<Country>> GetAllAsync();
    }
}
