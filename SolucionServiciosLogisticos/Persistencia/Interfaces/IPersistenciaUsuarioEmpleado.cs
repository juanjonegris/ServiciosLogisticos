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
        void AgregarUsuarioEmpleado(UsuarioEmpleado UEM, UsuarioEmpleado UELogueado);
        void EliminarUsuarioEmpleado(UsuarioEmpleado UE, UsuarioEmpleado UELogueado);
        void ModificarUsuarioEmpleado(UsuarioEmpleado UE, UsuarioEmpleado UELogueado);
        UsuarioEmpleado BuscarUsuarioEmpleado(string pNombreUsuario, UsuarioEmpleado UELogueado);
        UsuarioEmpleado LogueoUsuarioEmpleado(string pNombreUsuario, string pContrasenia);
    }
}
