using Persistencia.Clases;
using Persistencia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class Fabrica
    {
        public static IPersistenciaUsuarioEmpleado GetPUE()
        {
            return PersistenciaUsuarioEmpleado.GetInstancia();
        }

        public static IPersistenciaUsuarioEmpresa GetPUEM()
        {
            return PersistenciaUsuarioEmpresa.GetInstancia();
        }

        public static IPersistenciaSolicitudEntrega GetPS()
        {
            return PersistenciaSolicitudEntrega.GetInstancia();
        }

        public static IPersistenciaPaquete GetPP()
        {
            return PersistenciaPaquete.GetInstancia();
        }
    }
}
