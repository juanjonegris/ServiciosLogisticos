using EC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Clases
{
    internal class PersistenciaSolicitudPaquete
    {
        #region singleton
        private static PersistenciaSolicitudPaquete _instancia = null;

        private PersistenciaSolicitudPaquete() { }

        public static PersistenciaSolicitudPaquete GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaSolicitudPaquete();

            return _instancia;
        }
        #endregion 

        internal void AltaSolicitudPaquete(int numeroInterno, int codigoBarras, SqlTransaction _miTransaccion)
        {
            SqlCommand oComando = new SqlCommand("SolicitudPaqueteAlta", _miTransaccion.Connection);
            SqlParameter _numInt = new SqlParameter("@CodigodeBarras", codigoBarras);
            SqlParameter _nomDest = new SqlParameter("@NumeroInterno", numeroInterno);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_numInt);
            oComando.Parameters.Add(_nomDest);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oComando.Transaction = _miTransaccion;

                oComando.ExecuteNonQuery();

                //verifico si hay errores
                int retorno = Convert.ToInt32(_Retorno.Value);
                if (retorno == -1)
                    throw new Exception("No existe el numero de solicitud - Solicitud Paquete");
                if (retorno == -2)
                    throw new Exception("No existe el paquete - Solicitud Paquete");
                if (retorno == -3)
                    throw new Exception("El paquete ya ha sido asignado a una solicitud");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        internal List<Paquete> ListarPaquetesEnSolicitud(int pNnumeroInterno, Usuario ULogueado = null)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("PaquetesListadoPorSolicitud", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            SqlParameter _numInt = new SqlParameter("@NumeroInterno", pNnumeroInterno);

            oComando.Parameters.Add(_numInt);

            List<Paquete> listPaquetePorSol = new List<Paquete>();
            int codBar;
            string tipo;
            string descripcion;
            double peso;
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
                        peso = Convert.ToDouble(dr["Peso"]);
                        nombreUsuarioEmp = dr["NombreUsuarioEmpresa"].ToString();
                        empresa = PersistenciaUsuarioEmpresa.GetInstancia().BuscarUsuarioEmpresaTodos(nombreUsuarioEmp, ULogueado);
                        Paquete paquete = new Paquete(codBar, tipo, descripcion, peso, empresa);
                        listPaquetePorSol.Add(paquete);
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
            return listPaquetePorSol;
        }

    }
}
