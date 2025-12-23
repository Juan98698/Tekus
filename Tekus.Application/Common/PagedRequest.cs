using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.Common
{
    public class PagedRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string Search { get; set; } = string.Empty;
        public string OrderBy { get; set; } = string.Empty;

        public bool OrderAsc { get; set; } = true;
    }
}
