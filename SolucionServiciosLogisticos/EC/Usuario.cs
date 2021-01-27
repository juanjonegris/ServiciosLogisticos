using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace EC
{
    [KnownType(typeof(UsuarioEmpresa))]
    [KnownType(typeof(UsuarioEmpleado))]
    [DataContract]
    public abstract class Usuario
    {
        private string _nombreUsuario;
        private string _contrasena;
        private string _nombre;

        [DataMember]
        public string NombreUsuario
        {
            get { return _nombreUsuario; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Debe especificar un nombre");

                if (value.Trim().Length > 50)
                    throw new Exception("Debe especificar un nombre de hasta 50 caracteres");
                _nombreUsuario = value;
            }
        }

        [DataMember]
        public string Contrasenia
        {
            get { return _contrasena; }

            set
            {
                if (value.Trim().Length != 6 )
                    throw new Exception(" La contraseña debe contener caracteres");
                if ((!Regex.IsMatch(value.ToLower(), "[a-zA-Z]{3}[0-9]{2}[$&+,:;=?@#|''<>.^*()%!-]{1}")))
                        throw new Exception(" Verifique el formato del su contraseña debe ser: 3 letras,2 números y 1 Simbolo.");
                _contrasena = value;
            }
        }

        [DataMember]
        public string Nombre
        {
            get { return _nombre; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Debe especificar un nombre");

                if (value.Trim().Length > 50)
                    throw new Exception("Debe especificar un nombre de hasta 50 caracteres");

                _nombre = value;
            }
        }

        public Usuario(string nombreUsuario, string contrasenia, string nombre)
        {
            NombreUsuario = nombreUsuario;
            Contrasenia = Contrasenia;
            Nombre = nombre;
        }

        public Usuario() { }

    }
}

