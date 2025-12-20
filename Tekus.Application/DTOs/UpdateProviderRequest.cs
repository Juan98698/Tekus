using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs
{
    public class UpdateProviderRequest
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Nit { get; set; } = default!;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = default!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        public Dictionary<string, string> CustomFields { get; set; }
    }
}
