using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs.Reports
{
    public class SummaryReportResponse
    {
        public List<CountryCountResponse> ServicesByCountry { get; set; } = new();
        public List<CountryCountResponse> ProvidersByCountry { get; set; } = new();
    }
}
