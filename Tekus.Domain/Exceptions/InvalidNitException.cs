using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.Exceptions
{
    public class InvalidNitException : DomainException
    {
        public InvalidNitException(string nit)
            : base($"El NIT '{nit}' no tiene un formato válido")
        {
        }
    }
}
