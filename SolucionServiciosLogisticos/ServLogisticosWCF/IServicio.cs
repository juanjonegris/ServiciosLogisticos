using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EC;
using System.Xml;

namespace ServLogisticosWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicio" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicio
    {
        #region Paquetes

        [OperationContract]
        void AgregarPaquete(Paquete P, UsuarioEmpleado ULogueado);

        [OperationContract]
        Paquete BuscarPaquete(int pCodigoBarras, UsuarioEmpleado ULogueado);

        [OperationContract]
        List<Paquete> ListarPaquetesSinSolicitud(UsuarioEmpleado ULogueado);

        #endregion


        #region Solicitudes de Entregas  

        [OperationContract]
        void AgregarSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado);

        [OperationContract]
        SolicitudEntrega BuscarSolicitudEntrega(int pNumeroInterno, UsuarioEmpleado ULogueado);

        [OperationContract]
        void ModificarEstadoSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado);

        [OperationContract]
        XmlDocument ListarSolicitudEntrega();

        [OperationContract]
        List<SolicitudEntrega> ListarSolicitudEntregaEmpresaLogueada(UsuarioEmpresa ULogueado);

        #endregion

        #region Usuarios 

        [OperationContract]
        void AgregarUsuario(Usuario U, UsuarioEmpleado ULogueado);

        [OperationContract]
        void EliminarUsuario(Usuario U, UsuarioEmpleado ULogueado);

        [OperationContract]
        void ModificarUsuario(Usuario U, UsuarioEmpleado ULogueado);

        [OperationContract]
        Usuario BuscarUsuario(string pNombreUsuario, UsuarioEmpleado ULogueado);

        [OperationContract]
        Usuario LogueoUsuario(string pNombreUsuario, string pContrasenia);

        #endregion
    }
}
