using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs
{
    public class AddServiceRequest
    {
        public Guid ProviderId { get; set; }
        public string Name { get; set; } = default!;
        public decimal HourValueUsd { get; set; }
    }
}
