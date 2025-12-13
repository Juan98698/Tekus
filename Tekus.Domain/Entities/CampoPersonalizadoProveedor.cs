using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.Entities
{
    public class CampoPersonalizadoProveedor
    {
        public int Id { get; private set; }

        public string NombreCampo { get; private set; }
        public string Valor { get; private set; }

        public CampoPersonalizadoProveedor(string nombreCampo, string valor)
        {
            SetNombreCampo(nombreCampo);
            SetValor(valor);
        }

        private void SetNombreCampo(string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(nombreCampo))
                throw new ArgumentException("El nombre del campo personalizado es obligatorio");

            NombreCampo = nombreCampo.Trim();
        }

        private void SetValor(string valor)
        {
            Valor = valor?.Trim() ?? string.Empty;
        }

        public void ActualizarValor(string nuevoValor)
        {
            SetValor(nuevoValor);
        }


    }



}
