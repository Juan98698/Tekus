using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.ValueObjects
{
    public class Country
    {
        public string Code { get; }
        public string Name { get; }

        public Country(string code, string name)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("El código del país es obligatorio");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del país es obligatorio");

            Code = code.Trim().ToUpper();
            Name = name.Trim();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Country other)
                return false;

            return Code == other.Code;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        



    }

}
