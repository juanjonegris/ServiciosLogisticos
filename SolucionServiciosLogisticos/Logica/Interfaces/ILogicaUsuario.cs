using EC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaUsuario
    {
        void AgregarUsuario(Usuario U, UsuarioEmpleado ULogueado);
        void EliminarUsuario(Usuario U, UsuarioEmpleado ULogueado);
        void ModificarUsuario(Usuario U, UsuarioEmpleado ULogueado);
        Usuario BuscarUsuario(string pNombreUsuario, UsuarioEmpleado ULogueado);
        Usuario LogueoUsuario(string pNombreUsuario, string pContrasenia);

    }
}
