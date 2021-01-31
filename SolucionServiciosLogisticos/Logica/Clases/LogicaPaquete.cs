using EC;
using Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;

namespace Logica.Clases
{
    internal class LogicaPaquete : ILogicaPaquete
    {
        #region singleton
        private static LogicaPaquete _instancia = null;

        private LogicaPaquete() { }

        public static LogicaPaquete GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaPaquete();

            return _instancia;
        }
        #endregion


        public void AgregarPaquete(Paquete P, UsuarioEmpleado ULogueado)
        {
            Persistencia.Fabrica.GetPP().AgregarPaquete(P, ULogueado);
        }

        public Paquete BuscarPaquete(int pCodigoBarras, UsuarioEmpleado ULogueado)
        {
          return ( Persistencia.Fabrica.GetPP().BuscarPaquete(pCodigoBarras, ULogueado) );
        }

        public List<Paquete> ListarPaquetesSinSolicitud(UsuarioEmpleado ULogueado)
        {
            return (Persistencia.Fabrica.GetPP().ListarPaquetesSinSolicitud(ULogueado));
        }
       
    }
}
