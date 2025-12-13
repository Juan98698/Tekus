using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.Exceptions
{
    public class InvalidEmailException : DomainException
    {
        public InvalidEmailException(string email)
            : base($"El email '{email}' no es válido")
        {
        }
    }
}
