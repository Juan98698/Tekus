using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.Exceptions
{
    public class InvalidNitException : DomainException
    {
        public InvalidNitException(string message)
            : base(message)
        {
        }
    }
}