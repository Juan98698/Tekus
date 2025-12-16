using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs.Summary
{
    public class SummaryResponse
    {
        public List<CountryServicesSummaryDto> ServicesByCountry { get; set; } = new();
        public List<CountryProvidersSummaryDto> ProvidersByCountry { get; set; } = new();
    }

}
