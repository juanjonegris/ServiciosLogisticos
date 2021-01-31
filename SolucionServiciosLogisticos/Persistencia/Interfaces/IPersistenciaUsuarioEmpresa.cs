using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaUsuarioEmpresa
    {
        void AgregarUsuarioEmpresa(UsuarioEmpresa UEM, UsuarioEmpleado UEMLogueado);
        void EliminarUsuarioEmpresa(UsuarioEmpresa UEM, UsuarioEmpleado UEMLogueado);
        void ModificarUsuarioEmpresa(UsuarioEmpresa UEM, UsuarioEmpleado UEMLogueado);
        UsuarioEmpresa BuscarUsuarioEmpresa(string pNombreUsuario, UsuarioEmpleado ULogueado);
        UsuarioEmpresa LogueoUsuarioEmpresa(string pNombreUsuario, string pContrasenia);

    }
}
