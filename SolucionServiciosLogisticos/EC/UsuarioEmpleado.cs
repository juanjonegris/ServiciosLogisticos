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
    public class UsuarioEmpleado : Usuario
    {
        private DateTime _horaInicio;
        private DateTime _horaFin;

        [DataMember]
        public DateTime HoraInicio
        {
            get { return _horaInicio; }

            set { _horaInicio = value; }
        }

        [DataMember]
        public DateTime HoraFin
        {
            get { return _horaFin; }

            set { _horaFin = value; }
        }

        public UsuarioEmpleado(DateTime horaInicio, DateTime horaFin, string nombreUsuario, string contrasenia, string nombre)
            : base ( nombreUsuario, contrasenia, nombre )
        {
            this.HoraInicio = horaInicio;
            this.HoraFin = horaFin;
        }

        public UsuarioEmpleado() { }
    }
}
