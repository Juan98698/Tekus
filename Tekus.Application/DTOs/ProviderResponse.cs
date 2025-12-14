using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Tekus.Application.DTOs
{
    public class ProviderResponse
    {
        public Guid Id { get; set; }
        public string Nit { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public IReadOnlyDictionary<string, string> CustomFields { get; set; }

        public static ProviderResponse From(Provider provider)
        {
            return new ProviderResponse
            {
                Id = provider.Id,
                Nit = provider.Nit,
                Name = provider.Name,
                Email = provider.Email,
                CustomFields = provider.CustomFields
            };
        }
    }

}
