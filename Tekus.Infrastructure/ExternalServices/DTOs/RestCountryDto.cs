using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Infrastructure.ExternalServices.DTOs
{
    public class RestCountryDto
    {
        public string Cca2 { get; set; } = default!;
        public RestCountryNameDto Name { get; set; } = default!;
    }

    public class RestCountryNameDto
    {
        public string Common { get; set; } = default!;
    }
}
