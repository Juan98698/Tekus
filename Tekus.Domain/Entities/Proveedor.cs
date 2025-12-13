using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Tekus.Domain.Entities
{
    public class Proveedor
    {
        public int Id { get;private set; }
        public string Nit { get; private set; }
        public string Nombre { get; private set; }
        public string Email { get; private set; }

        private readonly List<Servicio> _servicios = new();
        public IReadOnlyCollection<Servicio> Servicios => _servicios.AsReadOnly();

        private readonly List<CampoPersonalizadoProveedor> _camposPersonalizados = new();
        public IReadOnlyCollection<CampoPersonalizadoProveedor> CamposPersonalizados
            => _camposPersonalizados.AsReadOnly();

        public Proveedor(string nit, string nombre, string email)
        {
            SetNit(nit);
            SetNombre(nombre);
            SetEmail(email);
        }

        private void SetNit(string nit)
        {
            if (string.IsNullOrWhiteSpace(nit))
                throw new ArgumentException("El NIT es obligatorio");

            Nit = nit.Trim();
        }

        private void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio");

            Nombre = nombre.Trim();
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es obligatorio");

            if (!email.Contains("@"))
                throw new ArgumentException("El email no es válido");

            Email = email.Trim().ToLower();
        }

        public void ActualizarDatos(string nit, string nombre, string email)
        {
            SetNit(nit);
            SetNombre(nombre);
            SetEmail(email);
        }

        public void AgregarServicio(Servicio servicio)
        {
            if (_servicios.Any(s => s.Nombre == servicio.Nombre))
                throw new ArgumentException("El proveedor ya tiene un servicio con ese nombre");

            _servicios.Add(servicio);
        }

        public void AgregarCampoPersonalizado(string nombreCampo, string valor)
        {
            if (_camposPersonalizados.Any(c => c.NombreCampo == nombreCampo))
                throw new ArgumentException("El campo personalizado ya existe");

            var campo = new CampoPersonalizadoProveedor(nombreCampo, valor);
            _camposPersonalizados.Add(campo);
        }





    }
}
