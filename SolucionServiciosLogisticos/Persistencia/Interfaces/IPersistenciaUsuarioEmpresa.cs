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
        void AgregarUsuarioEmpresa(UsuarioEmpresa UEM, Usuario UEMLogueado);
        void EliminarUsuarioEmpresa(UsuarioEmpresa UEM, Usuario UEMLogueado);
        void ModificarUsuarioEmpresa(UsuarioEmpresa UEM, Usuario UEMLogueado);
        UsuarioEmpresa BuscarUsuarioEmpresa(string pNombreUsuario, Usuario ULogueado);
        UsuarioEmpresa LogueoUsuario(string pNombreUsuario, string pContrasenia);

    }
}
