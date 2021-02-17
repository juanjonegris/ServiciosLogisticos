using EC;
using Persistencia.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Clases
{
    internal class PersistenciaPaquete : IPersistenciaPaquete
    {
        #region singleton
        private static PersistenciaPaquete _instancia = null;

        private PersistenciaPaquete() { }

        public static PersistenciaPaquete GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaPaquete();

            return _instancia;
        }
        #endregion

        public void AgregarPaquete(Paquete P, UsuarioEmpleado ULogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("PaquetesAlta", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _codBar = new SqlParameter("@CodigodeBarras", P.CodigoBarras);
            SqlParameter _tipo = new SqlParameter("@Tipo", P.Tipo);
            SqlParameter _desc = new SqlParameter("@Descripcion", P.Descripcion);
            SqlParameter _peso = new SqlParameter("@Peso", P.Peso);
            SqlParameter _nomUsu = new SqlParameter("@NombreUsuarioEmpresa", P.Empresa.NombreUsuario);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_codBar);
            oComando.Parameters.Add(_tipo);
            oComando.Parameters.Add(_desc);
            oComando.Parameters.Add(_peso);
            oComando.Parameters.Add(_nomUsu);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                int returned_value = Convert.ToInt32(_Retorno.Value);

                if (returned_value == -1)
                    throw new Exception("El codigo del paquete que intenta dar de alta ya existe");
                if (returned_value == -2)
                    throw new Exception("No existe la empresa que intenta asociar al paquete");
                if (returned_value == -3)
                    throw new Exception("Ha habido un error al intentar dar de alta el paquete");
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

        public Paquete BuscarPaquete(int pCodigoBarras, UsuarioEmpleado ULogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("PaqueteBuscar", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _codBar = new SqlParameter("@CodigodeBarras", pCodigoBarras);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_codBar);
            oComando.Parameters.Add(_Retorno);

            Paquete paquete = null;
            try
            {
                oConexion.Open();
                SqlDataReader dr = oComando.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    string tipo = dr["Tipo"].ToString();
                    string descripcion = dr["Descripcion"].ToString();
                    Decimal peso = Convert.ToDecimal(dr["Peso"]);
                    string nombreUsuarioEmp = dr["NombreUsuarioEmpresa"].ToString();
                    UsuarioEmpresa empresa = PersistenciaUsuarioEmpresa.GetInstancia().BuscarUsuarioEmpresaTodos(nombreUsuarioEmp, ULogueado);
                    paquete = new Paquete(pCodigoBarras, tipo, descripcion, peso, empresa);
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
            return paquete;
        }

        public List<Paquete> ListarPaquetesSinSolicitud(UsuarioEmpleado ULogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("PaquetesListadoSinSolicitud", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            List<Paquete> listPaqueteSinSol = new List<Paquete>();
            int codBar;
            string tipo;
            string descripcion;
            Decimal peso;
            string nombreUsuarioEmp;
            UsuarioEmpresa empresa;
            try
            {
                oConexion.Open();
                SqlDataReader dr = oComando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        codBar = (int)dr["CodigodeBarras"];
                        tipo = dr["Tipo"].ToString();
                        descripcion = dr["Descripcion"].ToString();
                        peso = Convert.ToDecimal(dr["Peso"]);
                        nombreUsuarioEmp = dr["NombreUsuarioEmpresa"].ToString();
                        empresa = PersistenciaUsuarioEmpresa.GetInstancia().BuscarUsuarioEmpresaTodos(nombreUsuarioEmp, ULogueado);
                        Paquete paquete = new Paquete(codBar, tipo, descripcion, peso, empresa);
                        listPaqueteSinSol.Add(paquete);
                    }
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
            return listPaqueteSinSol;
        }

        
    }
}
