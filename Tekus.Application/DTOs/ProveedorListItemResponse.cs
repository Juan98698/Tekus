using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs
{
    public class ProveedorListItemResponse
    {
        public int Id { get; set; }
        public string Nit { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
