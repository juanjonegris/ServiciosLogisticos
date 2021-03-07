using EC;
using Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Clases
{
    internal class LogicaUsuario : ILogicaUsuario
    {

        #region singleton
        private static LogicaUsuario _instancia = null;

        private LogicaUsuario() { }

        public static LogicaUsuario GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaUsuario();

            return _instancia;
        }
        #endregion 


        public void AgregarUsuario(Usuario U, UsuarioEmpleado ULogueado)
        {
            if (U is UsuarioEmpresa)
            {
                Persistencia.Fabrica.GetPUEM().AgregarUsuarioEmpresa((UsuarioEmpresa)U, ULogueado);
            } else
            {
                UsuarioEmpleado UE = (UsuarioEmpleado)U;

                //if ( Convert.ToInt32(UE.HoraInicio) >= Convert.ToInt32(UE.HoraFin) )
                //{
                //    throw new Exception("Hora de finalizacion tiene que ser posterior a la de inicio");
                //} else
                //{
                    Persistencia.Fabrica.GetPUE().AgregarUsuarioEmpleado(UE, ULogueado);
                //}
            }
        }

        public void EliminarUsuario(Usuario U, UsuarioEmpleado ULogueado)
        {
            if (U is UsuarioEmpresa)
            {
                Persistencia.Fabrica.GetPUEM().EliminarUsuarioEmpresa((UsuarioEmpresa)U, ULogueado);
            } else
            {
                Persistencia.Fabrica.GetPUE().EliminarUsuarioEmpleado((UsuarioEmpleado)U, ULogueado);
            }
        }

        public void ModificarUsuario(Usuario U, UsuarioEmpleado ULogueado)
        {
            if (U is UsuarioEmpresa)
            {
                Persistencia.Fabrica.GetPUEM().ModificarUsuarioEmpresa((UsuarioEmpresa)U, ULogueado);
            } else
            {
                Persistencia.Fabrica.GetPUE().ModificarUsuarioEmpleado((UsuarioEmpleado)U, ULogueado);
            }
        }

        public Usuario BuscarUsuario(string pNombreUsuario, UsuarioEmpleado ULogueado)
        {
            Usuario _usuario = null;
            _usuario = Persistencia.Fabrica.GetPUEM().BuscarUsuarioEmpresa(pNombreUsuario, ULogueado);
            
            if (_usuario == null)
            {
                _usuario = Persistencia.Fabrica.GetPUE().BuscarUsuarioEmpleado(pNombreUsuario, ULogueado);
            }

            return _usuario;
        }

        public Usuario LogueoUsuario(string pNombreUsuario, string pContrasenia)
        {
            Usuario _usuario = null;

            _usuario = Persistencia.Fabrica.GetPUEM().LogueoUsuarioEmpresa(pNombreUsuario, pContrasenia);

            if (_usuario == null)
            {
                _usuario = Persistencia.Fabrica.GetPUE().LogueoUsuarioEmpleado(pNombreUsuario, pContrasenia);
            }

            return _usuario;
        }

        public void CambioContrasena(string pNuevaContrasenia, Usuario ULogueado)
        {
           Persistencia.Fabrica.GetPUEM().CambioContrasena(pNuevaContrasenia, ULogueado);
        }
    }
}
