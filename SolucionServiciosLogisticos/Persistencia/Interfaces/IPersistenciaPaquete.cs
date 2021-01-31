using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC;


namespace Persistencia.Interfaces
{
    public interface IPersistenciaPaquete
    {
        void AgregarPaquete(Paquete P, UsuarioEmpleado ULogueado);
        Paquete BuscarPaquete(int pCodigoBarras, UsuarioEmpleado ULogueado);
        List<Paquete> ListarPaquetesSinSolicitud(UsuarioEmpleado ULogueado);

    }
}
