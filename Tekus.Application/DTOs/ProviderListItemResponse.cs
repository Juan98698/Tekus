using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs
{
    public class ProviderListItemResponse
    {
        public Guid Id { get; set; }
        public string Nit { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
