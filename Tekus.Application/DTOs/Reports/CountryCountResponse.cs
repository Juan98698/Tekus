using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs.Reports
{
    public class CountryCountResponse
    {
        public string CountryCode { get; set; } = default!;
        public int Count { get; set; }
    }
}
