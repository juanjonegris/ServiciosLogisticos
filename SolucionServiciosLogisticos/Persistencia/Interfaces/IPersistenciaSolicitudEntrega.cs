using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaSolicitudEntrega
    {
        void AgregarSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado);
        SolicitudEntrega BuscarSolicitudEntrega(int pNumeroInterno, UsuarioEmpleado ULogueado);
        void ModificarEstadoSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado);
        List<SolicitudEntrega> ListarSolicitudEntrega();
        List<SolicitudEntrega> ListarSolicitudEntrega(UsuarioEmpleado ULogueado);
        List<SolicitudEntrega> ListarSolicitudEntregaEmpresaLogueada(UsuarioEmpresa ULogueado);
    }
}
