using EC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Logica.Interfaces
{
    public interface ILogicaSolicitudEntrega
    {
        void AgregarSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado);
        SolicitudEntrega BuscarSolicitudEntrega(int pNumeroInterno, UsuarioEmpleado ULogueado);
        void ModificarEstadoSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado);
        XmlDocument ListarSolicitudEntrega();
        List<SolicitudEntrega> ListarSolicitudEntregaEmpresaLogueada(UsuarioEmpresa ULogueado);
        List<SolicitudEntrega> ListarSolicitudEntregaEstado(UsuarioEmpleado ULogueado);
    }
}
