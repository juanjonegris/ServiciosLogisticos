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
    [DataContract]
    public class SolicitudEntrega
    {
        private int _numeroInterno;
        private DateTime _fechaEntrega;
        private string _nombreDestinatario;
        private string _direccionDestinatario;
        private string _estado;
        private UsuarioEmpleado _empleado;
        private List<Paquete> _listaPaquete;


        [DataMember]
        public int NumeroInterno
        {
            get { return _numeroInterno; }

            set { _numeroInterno = value; }
        }


        [DataMember]
        public DateTime FechaEntrega
        {
            get { return _fechaEntrega; }

            set { _fechaEntrega = value; }
        }

        [DataMember]
        public string NombreDestinatario
        {
            get { return _nombreDestinatario; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Debe especificar un nombre");

                if (value.Trim().Length > 50)
                    throw new Exception("Debe especificar un nombre de hasta 50 caracteres");

                _nombreDestinatario = value;
            }
        }

        [DataMember]
        public string DireccionDestinatario
        {
            get { return _direccionDestinatario; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Debe especificar una dirección");

                if (value.Trim().Length > 50)
                    throw new Exception("Debe especificar una dirección de hasta 50 caracteres");

                _direccionDestinatario = value;
            }
        }

        public string Estado
        {
            get { return _estado; }

            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Debe especificar un estado");
                if (value != "En Depósito" && value != "En Camino" && value != "Entregado")
                    throw new Exception("El estado especificado no es válido");
                _estado = value;
            }
        }

        [DataMember]
        public UsuarioEmpleado Empleado
        {
            get { return _empleado; }

            set
            {
                if (value == null)
                    throw new Exception("Debe inidcar el empledo");
                _empleado = value;
            }
        }

        [DataMember]
        public List<Paquete> ListaPaquete
        {
            get { return _listaPaquete; }

            set
            {
                if (value == null)
                    throw new Exception("Una Solicitud sin paquetesa no se admite");
                if (value.Count == 0)
                    throw new Exception("Debe seleccionar al menos un paquete obligatoriamente");
                _listaPaquete = value;
            }
        }

        public SolicitudEntrega(int numeroInterno, DateTime fechaEntrega, string nombreDestinatario, string direccionDestinatario, string estado, UsuarioEmpleado empleado, List<Paquete> listaPaquete)
        {
            NumeroInterno = numeroInterno;
            FechaEntrega = fechaEntrega;
            NombreDestinatario = nombreDestinatario;
            DireccionDestinatario = direccionDestinatario;
            Estado = estado;
            Empleado = empleado;
            ListaPaquete = listaPaquete;
        }

        public SolicitudEntrega() { }

    }
}
