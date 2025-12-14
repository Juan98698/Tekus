using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;
using Tekus.Domain.Exceptions;

namespace Tekus.Domain.Entities
{
    public class Provider
    {
        public Guid Id { get; private set; } = default!;

        public string Nit { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        private readonly Dictionary<string, string> _customFields = new();
        public IReadOnlyDictionary<string, string> CustomFields => _customFields;



        
        private Provider() { }

        
        public Provider(string nit, string name, string email)
        {
            

            SetNit(nit);
            SetName(name);
            SetEmail(email);
        }
    

        private void SetNit(string nit)
        {
            if (string.IsNullOrWhiteSpace(nit))
                throw new RequiredFieldException("NIT");

            Nit = nit.Trim();
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new RequiredFieldException("Nombre");

            Name = name.Trim();
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new RequiredFieldException("Email");

            if (!email.Contains("@"))
                throw new InvalidEmailException(email);

            Email = email.Trim().ToLower();
        }

        
        public void AddCustomField(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new RequiredFieldException("Nombre del campo personalizado");

            if (_customFields.ContainsKey(fieldName))
                throw new DuplicateEntityException("Campo personalizado");

            _customFields[fieldName] = string.Empty;
        }

        public void AssignCustomFieldValue(string fieldName, string value)
        {
            if (!_customFields.ContainsKey(fieldName))
                throw new NotFoundException("Campo personalizado");

            _customFields[fieldName] = value ?? string.Empty;
        }

        private readonly List<Service> _services = new();
        public IReadOnlyCollection<Service> Services => _services.AsReadOnly();

        public void AddService(Service service)
        {
            if (service == null)
                throw new RequiredFieldException("service");

            if (_services.Any(s => s.Name == service.Name))
                throw new DuplicateEntityException("service");

            _services.Add(service);
        }

        public void UpdateBasicInfo(string nit, string name, string email)
        {
            SetNit(nit);
            SetName(name);
            SetEmail(email);
        }

        public void ReplaceCustomFields(Dictionary<string, string> fields)
        {
            _customFields.Clear();
            foreach (var field in fields)
                _customFields[field.Key] = field.Value;
        }

    }

}
