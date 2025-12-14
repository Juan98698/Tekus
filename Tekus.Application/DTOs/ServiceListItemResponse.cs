using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs
{
    public class ServiceListItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal HourValueUsd { get; set; }
    }
}
