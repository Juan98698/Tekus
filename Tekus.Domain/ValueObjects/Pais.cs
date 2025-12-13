using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.ValueObjects
{
    public class Pais
    {
        public string Codigo { get; }
        public string Nombre { get; }

        public Pais(string codigo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código del país es obligatorio");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del país es obligatorio");

            Codigo = codigo.Trim().ToUpper();
            Nombre = nombre.Trim();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Pais other)
                return false;

            return Codigo == other.Codigo;
        }

        public override int GetHashCode()
        {
            return Codigo.GetHashCode();
        }

        



    }

}
