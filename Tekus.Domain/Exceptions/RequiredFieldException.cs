using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.Exceptions
{
    public class RequiredFieldException : DomainException
    {
        public RequiredFieldException(string fieldName)
            : base($"El campo '{fieldName}' es obligatorio")
        {
        }
    }
}
