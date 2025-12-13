using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.Exceptions
{
    public class InvalidServiceValueException : DomainException
    {
        public InvalidServiceValueException(decimal value)
            : base($"El valor por hora '{value}' debe ser mayor a cero")
        {
        }
    }
}
