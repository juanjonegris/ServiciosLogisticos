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
    public class Paquete
    {
        private int _codigoBarras;
        private string _tipo;
        private string _descripcion;
        private double _peso;
        private UsuarioEmpresa _empresa;


        [DataMember] 
        public int CodigoBarras
        {
            get { return _codigoBarras; }

            set
            {
                if (value <= 0 )
                    throw new Exception("Debe indicar un codigo de barras");
                _codigoBarras = value;
            }
        }

        [DataMember]
        public string Tipo
        {
            get { return _tipo; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Debe especificar un tipo");
                if ( value.ToLower() != "fragil" && value.ToLower() != "común" && value.ToLower() != "bulto")
                    throw new Exception("El tipo especificado no es válido");
                _tipo = value;
            }
        }

        [DataMember]
        public string Descripcion
        {
            get { return _descripcion; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Debe especificar una descripción");

                _descripcion = value;
            }
        }

        [DataMember]
        public double Peso
        {
            get { return _peso; }

            set
            {
                if (value <= 0)
                    throw new Exception("Debe indicar un peso del paquete válido");
                _peso = value;
            }
        }

        [DataMember]
        public UsuarioEmpresa Empresa
        {
            get { return _empresa; }

            set
            {
                if ( value == null )
                    throw new Exception("Debe existir la empresa del paquete");
                _empresa = value;
            }
        }

        public Paquete(int codigoBarras, string tipo, string descripcion, double peso, UsuarioEmpresa empresa)
        {
            CodigoBarras = codigoBarras;
            Tipo = tipo;
            Descripcion = descripcion;
            Peso = peso;
            Empresa = empresa;
            
        }

        public Paquete() { }
    }
}
