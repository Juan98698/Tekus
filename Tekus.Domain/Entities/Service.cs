using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Exceptions;
using Tekus.Domain.ValueObjects;

namespace Tekus.Domain.Entities
{
    public class Service
    {
        public int Id { get; private set; }

        public string Name { get; private set; }
        public decimal HourValueUsd { get; private set; }

        private readonly List<Country> _Countries = new();
        public IReadOnlyCollection<Country> Countries => _Countries.AsReadOnly();

        public Service(string name, decimal HourValueUsd)
        {
            SetName(name);
            SetHourValueUsd(HourValueUsd);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del service es obligatorio");

            Name = name.Trim();
        }

        private void SetHourValueUsd(decimal hourValueUsd)
        {
            if (HourValueUsd <= 0)
                throw new InvalidServiceValueException(hourValueUsd);

            HourValueUsd = hourValueUsd;
        }

        public void Actualizar(string name, decimal HourValueUsd)
        {
            SetName(name);
            SetHourValueUsd(HourValueUsd);
        }

        public void AddCountry(Country Country)
        {
            if (_Countries.Any(p => p.Equals(Country)))
                throw new DuplicateEntityException("País");

            _Countries.Add(Country);
        }

        public void RemoverCountry(Country Country)
        {
            _Countries.Remove(Country);
        }

      
                





    }
}
