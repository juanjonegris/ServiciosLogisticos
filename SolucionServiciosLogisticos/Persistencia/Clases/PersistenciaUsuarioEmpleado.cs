using EC;
using Persistencia.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Clases
{
    internal class PersistenciaUsuarioEmpleado : IPersistenciaUsuarioEmpleado
    {
        #region singleton
        private static PersistenciaUsuarioEmpleado _instancia = null;

        private PersistenciaUsuarioEmpleado() { }

        public static PersistenciaUsuarioEmpleado GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaUsuarioEmpleado();

            return _instancia;
        }
        #endregion


        public void AgregarUsuarioEmpleado(UsuarioEmpleado UE, UsuarioEmpleado UELogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(UELogueado));

            SqlCommand oComando = new SqlCommand("UsuariosEmpleadoAlta", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", UE.NombreUsuario);
            SqlParameter _nombre = new SqlParameter("@Nombre", UE.Nombre);
            SqlParameter _contra = new SqlParameter("@Contrasenia", UE.Contrasenia);
            SqlParameter _hini = new SqlParameter("@HoraInicio", UE.HoraInicio);
            SqlParameter _hfin = new SqlParameter("@HoraFin", UE.HoraFin);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_contra);
            oComando.Parameters.Add(_nombre);
            oComando.Parameters.Add(_hini);
            oComando.Parameters.Add(_hfin);
            oComando.Parameters.Add(_Retorno);

            
            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int returned_value = Convert.ToInt32(_Retorno.Value);

                if (returned_value == -1)
                    throw new Exception("Ya existe un usuario registrado con ese Nombre de Usuario");
                if (returned_value == -2)
                    throw new Exception("Ha habido un error al intentar dar de alta el usuario");

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }

        public void EliminarUsuarioEmpleado(UsuarioEmpleado UE, UsuarioEmpleado UELogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(UELogueado));

            SqlCommand oComando = new SqlCommand("UsuariosEmpleadoBaja", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", UE.NombreUsuario);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int returned_value = Convert.ToInt32(_Retorno.Value);

                if (returned_value == -1)
                    throw new Exception("El usuario que intenta borrar no se encuentra registrado");
                if (returned_value == -2)
                    throw new Exception("Ha habido un error al intentar borrar el usuario");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }

        public void ModificarUsuarioEmpleado(UsuarioEmpleado UE, UsuarioEmpleado UELogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(UELogueado));

            // Alta Usuario Sql
            SqlCommand oComando = new SqlCommand("UsuariosEmpleadoModificar", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", UE.NombreUsuario);
            SqlParameter _nombre = new SqlParameter("@Nombre", UE.Nombre);
            SqlParameter _horaini = new SqlParameter("@HoraInicio", UE.HoraInicio);
            SqlParameter _horafin = new SqlParameter("@HoraFin", UE.HoraFin);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_nombre);
            oComando.Parameters.Add(_horaini);
            oComando.Parameters.Add(_horafin);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int returned_value = Convert.ToInt32(_Retorno.Value);

                if (returned_value == -1)
                    throw new Exception("No existe el usuario que intenta modificar");
                if (returned_value == -2)
                    throw new Exception("Ha ocurrido un error al intentar modificar el usuario");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }

        public UsuarioEmpleado BuscarUsuarioEmpleado(string pNombreUsuario, UsuarioEmpleado UELogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(UELogueado));

            SqlCommand oComando = new SqlCommand("UsuariosEmpleadoBuscar", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", pNombreUsuario);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_Retorno);

            UsuarioEmpleado usuarioEmpleado = null;

            try
            {
                oConexion.Open();
                SqlDataReader dr = oComando.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    string nomUsu = pNombreUsuario;
                    string contra = dr["Contrasenia"].ToString();
                    string nombre = dr["Nombre"].ToString();
                    string horaini =dr["HoraInicio"].ToString();
                    string horafin = dr["HoraFin"].ToString();
                    usuarioEmpleado = new UsuarioEmpleado(horaini, horafin, nomUsu, contra, nombre);
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConexion.Close();
            }

            return usuarioEmpleado;
        }

        internal UsuarioEmpleado BuscarUsuarioEmpleadoTodos(string pNombreUsuario, Usuario UELogueado = null)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(UELogueado));

            SqlCommand oComando = new SqlCommand("UsuariosEmpleadoBuscarTodos", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", pNombreUsuario);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_Retorno);

            UsuarioEmpleado usuarioEmpleado = null;

            try
            {
                oConexion.Open();
                SqlDataReader dr = oComando.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    string nomUsu = pNombreUsuario;
                    string contra = dr["Contrasenia"].ToString();
                    string nombre = dr["Nombre"].ToString();
                    string horaini = dr["HoraInicio"].ToString();
                    string horafin = dr["HoraFin"].ToString();
                    usuarioEmpleado = new UsuarioEmpleado(horaini, horafin, nomUsu, contra, nombre);
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConexion.Close();
            }

            return usuarioEmpleado;
        }

        public UsuarioEmpleado LogueoUsuarioEmpleado(string pNombreUsuario, string pContrasenia)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn());

            SqlCommand oComando = new SqlCommand("LogueoUsuarioEmpleado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", pNombreUsuario);
            SqlParameter _contra = new SqlParameter("@Contrasenia", pContrasenia);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_contra);
            oComando.Parameters.Add(_Retorno);

            UsuarioEmpleado usuarioEmpleado = null;

            try
            {
                oConexion.Open();
                SqlDataReader dr = oComando.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    string nomUsu = pNombreUsuario;
                    string contra = dr["Contrasenia"].ToString();
                    string nombre = dr["Nombre"].ToString();
                    string horaini = dr["HoraInicio"].ToString();
                    string horafin = dr["HoraFin"].ToString();
                    usuarioEmpleado = new UsuarioEmpleado(horaini, horafin, nomUsu, contra, nombre);
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConexion.Close();
            }

            return usuarioEmpleado;
        }

    }

}
