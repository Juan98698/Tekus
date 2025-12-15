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
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public decimal HourValueUsd { get; private set; }

        private readonly List<Country> _countries = new();
        public IReadOnlyCollection<Country> Countries => _countries.AsReadOnly();

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
            if (hourValueUsd <= 0)
                throw new InvalidServiceValueException(hourValueUsd);

            HourValueUsd = hourValueUsd;
        }


        public void Update(string name, decimal hourValueUsd)
        {
            SetName(name);
            SetHourValueUsd(hourValueUsd);
        }


        public void AddCountry(Country country)
        {
            if (_countries.Any(c => c.Code == country.Code))
                throw new DuplicateEntityException("Country");

            _countries.Add(country);
        }








    }
}
