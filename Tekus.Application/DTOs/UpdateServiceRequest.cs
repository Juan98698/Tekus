using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs
{
    public class UpdateServiceRequest
    {
        public Guid ProviderId { get; set; }
        public Guid ServiceId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = default!;
        [Range(0.01, double.MaxValue)]
        public decimal HourValueUsd { get; set; }
    }
}
