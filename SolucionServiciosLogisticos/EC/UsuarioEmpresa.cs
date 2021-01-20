using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace EC
{
    [DataContract]
    public class UsuarioEmpresa : Usuario
    {
        private string _telefono;
        private string _direccion;
        private string _email;

        [DataMember]
        public string Telefono
        {
            get { return _telefono; }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Trim().Length > 9)
                    throw new Exception("Debe indicar un teléfono válido");
                _telefono = value;
            }
        }

        [DataMember]
        public string Direccion
        {
            get { return _direccion; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Debe especificar una dirección");

                if (value.Trim().Length > 50)
                    throw new Exception("Debe especificar una dirección de hasta 50 caracteres");
                _direccion = value;
            }
        }

        [DataMember]
        public string Email
        {
            get { return _email; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Debe especificar una dirección");

                if (value.Trim().Length > 50)
                    throw new Exception("Debe especificar una dirección de hasta 50 caracteres");
              
                if (!value.Contains(".") || !!value.Contains("@"))
                    throw new Exception("Debe indicar una direccion de email válida");
                _email = value;
            }
        }


        public UsuarioEmpresa(string telefono, string direccion, string email, string nombreUsuario, string contrasenia, string nombre)
            : base ( nombreUsuario, contrasenia, nombre )
        {
            this.Telefono = telefono;
            this.Direccion = direccion;
            this.Email = email;
        }

        public UsuarioEmpresa () { }

    }
}
