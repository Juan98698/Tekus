using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.ValueObjects;

namespace Tekus.Domain.Entities
{
    public class Servicio
    {
        public int Id { get; private set; }

        public string Nombre { get; private set; }
        public decimal ValorHoraUsd { get; private set; }

        private readonly List<Pais> _paises = new();
        public IReadOnlyCollection<Pais> Paises => _paises.AsReadOnly();

        public Servicio(string nombre, decimal valorHoraUsd)
        {
            SetNombre(nombre);
            SetValorHoraUsd(valorHoraUsd);
        }

        private void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del servicio es obligatorio");

            Nombre = nombre.Trim();
        }

        private void SetValorHoraUsd(decimal valorHoraUsd)
        {
            if (valorHoraUsd <= 0)
                throw new ArgumentException("El valor por hora debe ser mayor a cero");

            ValorHoraUsd = valorHoraUsd;
        }

        public void Actualizar(string nombre, decimal valorHoraUsd)
        {
            SetNombre(nombre);
            SetValorHoraUsd(valorHoraUsd);
        }

        public void AgregarPais(Pais pais)
        {
            if (_paises.Any(p => p.Equals(pais)))
                throw new ArgumentException("El país ya está asociado al servicio");

            _paises.Add(pais);
        }

        public void RemoverPais(Pais pais)
        {
            _paises.Remove(pais);
        }

      
                





    }
}
