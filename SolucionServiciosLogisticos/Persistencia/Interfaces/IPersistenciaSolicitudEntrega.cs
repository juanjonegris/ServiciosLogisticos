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
        void AgregarSolicitudEntrega(SolicitudEntrega S, Usuario ULogueado);
        SolicitudEntrega BuscarSolicitudEntrega(int pNumeroInterno, Usuario ULogueado);
        void ModificarEstadoSolicitudEntrega(SolicitudEntrega S, Usuario ULogueado);
        List<SolicitudEntrega> ListarSolicitudEntrega(Usuario ULogueado);
        List<SolicitudEntrega> ListarSolicitudEntregaEmpresaLogueada(Usuario ULogueado);
    }
}
