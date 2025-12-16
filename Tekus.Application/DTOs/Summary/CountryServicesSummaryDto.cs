using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs.Summary
{
    public class CountryServicesSummaryDto
    {
        public string CountryCode { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public int ServicesCount { get; set; }
    }

}
