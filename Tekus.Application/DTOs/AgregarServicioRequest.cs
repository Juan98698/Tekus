using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs
{
    public class AgregarServicioRequest
    {
        public int ProveedorId { get; set; }
        public string Nombre { get; set; } = default!;
        public decimal ValorHoraUsd { get; set; }
    }
}
