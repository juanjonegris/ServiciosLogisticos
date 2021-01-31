using EC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaPaquete
    {
        void AgregarPaquete(Paquete P, UsuarioEmpleado ULogueado);
        Paquete BuscarPaquete(int pCodigoBarras, UsuarioEmpleado ULogueado);
        List<Paquete> ListarPaquetesSinSolicitud(UsuarioEmpleado ULogueado);
    }
}
