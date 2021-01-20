using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaUsuarioEmpleado
    {
        void AgregarUsuarioEmpleado(UsuarioEmpleado UEM, Usuario UELogueado);
        void EliminarUsuarioEmpleado(UsuarioEmpleado UE, Usuario UELogueado);
        void ModificarUsuarioEmpleado(UsuarioEmpleado UE, Usuario UELogueado);
        UsuarioEmpleado BuscarUsuarioEmpleado(string pNombreUsuario, Usuario UELogueado);
        UsuarioEmpleado LogueoUsuario(string pNombreUsuario, string pContrasenia);
    }
}
