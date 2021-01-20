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

        public void AgregarUsuarioEmpresa(UsuarioEmpresa UEM, Usuario UEMLogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(UEMLogueado));

            // Alta Usuario Sql
            SqlCommand oComando = new SqlCommand("NuevoUsuario", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", UEM.NombreUsuario);
            SqlParameter _contra = new SqlParameter("@Contrasenia", UEM.Contrasenia);
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
                    throw new Exception("Usuario existente");
                else if (returned_value == -2)
                    throw new Exception("No se puede Crear usuario Login");
                else if (returned_value == -3)
                    throw new Exception("No se puede crear usuario BD");
                else if (returned_value == -4)
                    throw new Exception("Problema para asignar permisos al usuario");
            }
            catch (Exception ex)
            {
                throw ex;
            } 


            // Alta Usuario Sql
            SqlCommand oComando2 = new SqlCommand("UsuariosEmpresaAlta", oConexion);
            oComando2.CommandType = CommandType.StoredProcedure;

            SqlParameter _nombre = new SqlParameter("@Nombre", UEM.Nombre);
            SqlParameter _telefono = new SqlParameter("@Telefono", UEM.Telefono);
            SqlParameter _direccion = new SqlParameter("@Direccion", UEM.Direccion);
            SqlParameter _email = new SqlParameter("@Email", UEM.Email);

            SqlParameter _Retorno2 = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno2.Direction = ParameterDirection.ReturnValue;

            oComando2.Parameters.Add(_nomUsu);
            oComando2.Parameters.Add(_contra);
            oComando2.Parameters.Add(_nombre);
            oComando2.Parameters.Add(_telefono);
            oComando2.Parameters.Add(_direccion);
            oComando2.Parameters.Add(_email);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando2.ExecuteNonQuery();

                int returned_value = Convert.ToInt32(_Retorno2.Value);

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

        public void EliminarUsuarioEmpresa(UsuarioEmpresa UEM, Usuario UEMLogueado)
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

        public void ModificarUsuarioEmpresa(UsuarioEmpresa UEM, Usuario UEMLogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(UEMLogueado));

            // Alta Usuario Sql
            SqlCommand oComando = new SqlCommand("UsuariosEmpresaModificar", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", UEM.NombreUsuario);
            SqlParameter _contra = new SqlParameter("@Contrasenia", UEM.Contrasenia);
            SqlParameter _nombre = new SqlParameter("@Nombre", UEM.Nombre);
            SqlParameter _telefono = new SqlParameter("@Telefono", UEM.Telefono);
            SqlParameter _direcc = new SqlParameter("@Direccion", UEM.Direccion);
            SqlParameter _email = new SqlParameter("@Email", UEM.Email);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_contra);
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

        public UsuarioEmpresa BuscarUsuarioEmpresa(string pNombreUsuario, Usuario ULogueado)
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

        public UsuarioEmpresa LogueoUsuario(string pNombreUsuario, string pContrasenia)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn());

            SqlCommand oComando = new SqlCommand("UsuariosLogueo", oConexion);
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


    }
    }
