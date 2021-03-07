using EC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Logica;
using System.Xml;

namespace ServLogisticosWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Servicio" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Servicio.svc o Servicio.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Servicio : IServicio
    {
        #region Paquetes

        
       void IServicio.AgregarPaquete(Paquete P, UsuarioEmpleado ULogueado)
        {
            Fabrica.GetLP().AgregarPaquete(P, ULogueado);
        }

        Paquete IServicio.BuscarPaquete(int pCodigoBarras, UsuarioEmpleado ULogueado)
        {
           return ( Fabrica.GetLP().BuscarPaquete(pCodigoBarras, ULogueado));
        }


        List<Paquete> IServicio.ListarPaquetesSinSolicitud(UsuarioEmpleado ULogueado)
        {
            return (Fabrica.GetLP().ListarPaquetesSinSolicitud( ULogueado));
        }


        #endregion


        #region Solicitudes de Entregas  

        void IServicio.AgregarSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado)
        {
            Fabrica.GetLS().AgregarSolicitudEntrega(S, ULogueado);
        }

        SolicitudEntrega IServicio.BuscarSolicitudEntrega(int pNumeroInterno, UsuarioEmpleado ULogueado)
        {
           return ( Fabrica.GetLS().BuscarSolicitudEntrega(pNumeroInterno, ULogueado));
        }

        void IServicio.ModificarEstadoSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado)
        {
            Fabrica.GetLS().ModificarEstadoSolicitudEntrega(S, ULogueado);
        }

        string IServicio.ListarSolicitudEntrega()
        {
            return (Fabrica.GetLS().ListarSolicitudEntrega().OuterXml);
        }

        List<SolicitudEntrega> IServicio.ListarSolicitudEntregaEmpresaLogueada( UsuarioEmpresa ULogueado)
        {
            return (Fabrica.GetLS().ListarSolicitudEntregaEmpresaLogueada ( ULogueado ));
        }

        public List<SolicitudEntrega> ListarSolicitudEntregaEstado(UsuarioEmpleado ULogueado)
        {
            return (Fabrica.GetLS().ListarSolicitudEntregaEstado(ULogueado));
        }
        #endregion

        #region Usuarios 

        void IServicio.AgregarUsuario(Usuario U, UsuarioEmpleado ULogueado)
        {
            Fabrica.GetLU().AgregarUsuario( U,  ULogueado);
        }

        void IServicio.EliminarUsuario(Usuario U, UsuarioEmpleado ULogueado)
        {
            Fabrica.GetLU().EliminarUsuario(U, ULogueado);
        }

        void IServicio.ModificarUsuario(Usuario U, UsuarioEmpleado ULogueado)
        {
            Fabrica.GetLU().ModificarUsuario(U, ULogueado);
        }


        Usuario IServicio.BuscarUsuario(string pNombreUsuario, UsuarioEmpleado ULogueado)
        {
          return ( Fabrica.GetLU().BuscarUsuario( pNombreUsuario, ULogueado));
        }

        Usuario IServicio.LogueoUsuario( string pNombreUsuario, string pContrasenia )
        {
            return (Fabrica.GetLU().LogueoUsuario(pNombreUsuario, pContrasenia));
        }

        void IServicio.CambioContrasena(string pNuevaContrasenia, Usuario ULogueado)
        {
            Fabrica.GetLU().CambioContrasena(pNuevaContrasenia, ULogueado);
        }

        #endregion
    }
}
