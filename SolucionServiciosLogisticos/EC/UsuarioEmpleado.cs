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
    public class UsuarioEmpleado : Usuario
    {
        private string _horaInicio;
        private string _horaFin;

        [DataMember]
        public string HoraInicio
        {
            get { return _horaInicio; }

            set {
                if (value.Trim().Length != 5)
                    throw new Exception("Ingrese horas y minutos - Ejemplo 08:00");
                int number;

                bool success = Int32.TryParse(value, out number);

                if (!success)
                    throw new Exception("Ingrese valores numericos");

                string hora = value.Substring(0, 1);
                string min = value.Substring(3, 4);

                if (Convert.ToInt32(hora) < 0 || Convert.ToInt32(hora) > 23)
                    throw new Exception("Ingrese una hora valida - 0 a 23");

                if (Convert.ToInt32(min) < 0 || Convert.ToInt32(min) > 59)
                    throw new Exception("Ingrese minutos validos - 0 a 60");

                _horaInicio = value; }
        }

        [DataMember]
        public string HoraFin
        {
            get { return _horaFin; }

            set {
                if (value.Trim().Length != 5)
                    throw new Exception("Ingrese horas y minutos - Ejemplo 08:00");
                int number;

                bool success = Int32.TryParse(value, out number);

                if (!success)
                    throw new Exception("Ingrese valores numericos");

                string hora = value.Substring(0, 1);
                string min = value.Substring(3, 4);

                if (Convert.ToInt32(hora) < 0 || Convert.ToInt32(hora) > 23)
                    throw new Exception("Ingrese una hora valida - 0 a 23");

                if (Convert.ToInt32(min) < 0 || Convert.ToInt32(min) > 59)
                    throw new Exception("Ingrese minutos validos - 0 a 60");

                _horaFin = value; }
        }

        public UsuarioEmpleado(string horaInicio, string horaFin, string nombreUsuario, string contrasenia, string nombre)
            : base ( nombreUsuario, contrasenia, nombre )
        {
            this.HoraInicio = horaInicio;
            this.HoraFin = horaFin;
        }

        public UsuarioEmpleado() { }
    }
}
