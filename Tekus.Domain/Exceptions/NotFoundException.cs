using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string entityName)
            : base($"{entityName} no encontrado")
        {
        }
    }
}
