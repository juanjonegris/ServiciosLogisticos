using Persistencia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia.Clases
{
    class PersistenciaUsuarioEmpresa : IPersistenciaUsuarioEmpresa
    {
        #region singleton
        private static PersistenciaUsuarioEmpresa _instancia = null;

        private PersistenciaUsuarioEmpresa() { }

        public static PersistenciaUsuarioEmpresa GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaUsuarioEmpresa();

            return _instancia;
        }
        #endregion

        public void AgregarUsuarioEmpresa(UsuarioEmpresa UEM, UsuarioEmpleado UEMLogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(UEMLogueado));



            // Alta Usuario Sql
            SqlCommand oComando = new SqlCommand("UsuariosEmpresaAlta", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", UEM.NombreUsuario);
            SqlParameter _contra = new SqlParameter("@Contrasenia", UEM.NombreUsuario);
            SqlParameter _nombre = new SqlParameter("@Nombre", UEM.Nombre);
            SqlParameter _telefono = new SqlParameter("@Telefono", UEM.Telefono);
            SqlParameter _direccion = new SqlParameter("@Direccion", UEM.Direccion);
            SqlParameter _email = new SqlParameter("@Email", UEM.Email);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_contra);
            oComando.Parameters.Add(_nombre);
            oComando.Parameters.Add(_telefono);
            oComando.Parameters.Add(_direccion);
            oComando.Parameters.Add(_email);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int returned_value = Convert.ToInt32(_Retorno.Value);

                if (returned_value == -1)
                    throw new Exception("El nombre de usuario ya se encuentra registrado");
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

        public void EliminarUsuarioEmpresa(UsuarioEmpresa UEM, UsuarioEmpleado UEMLogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(UEMLogueado));

            SqlCommand oComando = new SqlCommand("UsuariosEmpresaBaja", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", UEM.NombreUsuario);
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

        public void ModificarUsuarioEmpresa(UsuarioEmpresa UEM, UsuarioEmpleado UEMLogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(UEMLogueado));

            // Alta Usuario Sql
            SqlCommand oComando = new SqlCommand("UsuariosEmpresaModificar", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", UEM.NombreUsuario);
            SqlParameter _nombre = new SqlParameter("@Nombre", UEM.Nombre);
            SqlParameter _telefono = new SqlParameter("@Telefono", UEM.Telefono);
            SqlParameter _direcc = new SqlParameter("@Direccion", UEM.Direccion);
            SqlParameter _email = new SqlParameter("@Email", UEM.Email);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_nombre);
            oComando.Parameters.Add(_telefono);
            oComando.Parameters.Add(_direcc);
            oComando.Parameters.Add(_email);
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

        public UsuarioEmpresa BuscarUsuarioEmpresa(string pNombreUsuario, UsuarioEmpleado ULogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("UsuariosEmpresaBuscar", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", pNombreUsuario);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_Retorno);

            UsuarioEmpresa usuarioEmpresa = null;

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
                    string telef = dr["Telefono"].ToString();
                    string direcc = dr["Direccion"].ToString();
                    string email = dr["Email"].ToString();
                    usuarioEmpresa = new UsuarioEmpresa(telef, direcc, email, nomUsu, contra, nombre);
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

            return usuarioEmpresa;
        }


        internal UsuarioEmpresa BuscarUsuarioEmpresaTodos(string pNombreUsuario, Usuario ULogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("UsuariosEmpresaBuscarTodos", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", pNombreUsuario);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_Retorno);

            UsuarioEmpresa usuarioEmpresa = null;

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
                    string telef = dr["Telefono"].ToString();
                    string direcc = dr["Direccion"].ToString();
                    string email = dr["Email"].ToString();
                    usuarioEmpresa = new UsuarioEmpresa(telef, direcc, email, nomUsu, contra, nombre);
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

            return usuarioEmpresa;
        }

        public UsuarioEmpresa LogueoUsuarioEmpresa(string pNombreUsuario, string pContrasenia)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn());

            SqlCommand oComando = new SqlCommand("LogueoUsuarioEmpresa", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", pNombreUsuario);
            SqlParameter _contra = new SqlParameter("@Contrasenia", pContrasenia);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_contra);
            oComando.Parameters.Add(_Retorno);

            UsuarioEmpresa usuarioEmpresa = null;

            try
            {
                oConexion.Open();
                SqlDataReader dr = oComando.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    string nomUsu = pNombreUsuario;
                    string contra = pContrasenia;
                    string nombre = dr["Nombre"].ToString();
                    string telef = dr["Telefono"].ToString();
                    string direcc = dr["Direccion"].ToString();
                    string email = dr["Email"].ToString();
                    usuarioEmpresa = new UsuarioEmpresa(telef, direcc, email, nomUsu, contra, nombre);
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

            return usuarioEmpresa;
        }

        public void CambioContrasena(string pNuevaContrasenia, Usuario ULogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn());

            SqlCommand oComando = new SqlCommand("CambioContrasena", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", ULogueado.NombreUsuario);
            SqlParameter _contra = new SqlParameter("@Contrasenia", pNuevaContrasenia);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_contra);
            oComando.Parameters.Add(_Retorno);
            

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();

                int returned_value = Convert.ToInt32(_Retorno.Value);

                if (returned_value == -1)
                    throw new Exception("No existe el usuario en la Base de Datos");
                if (returned_value == -2)
                    throw new Exception("No existe el usuario cuya contrseña intenta modificar");
                if (returned_value == -3)
                    throw new Exception("Ha ocurrido un error al intentar modificar la contraseña");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConexion.Close();
            }

        }
    }
}
