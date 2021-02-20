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
    internal class PersistenciaSolicitudEntrega : IPersistenciaSolicitudEntrega
    {
        #region singleton
        private static PersistenciaSolicitudEntrega _instancia = null;

        private PersistenciaSolicitudEntrega() { }

        public static PersistenciaSolicitudEntrega GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaSolicitudEntrega();

            return _instancia;
        }
        #endregion


        public void AgregarSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado)
        {

            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("SolicitudesDeEntregaAlta", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _fechaEntrega = new SqlParameter("@FechaDeEntrega", S.FechaEntrega);
            SqlParameter _nomDest = new SqlParameter("@NombreDestinatario", S.NombreDestinatario);
            SqlParameter _dirDest = new SqlParameter("@DireccionDestinatario", S.DireccionDestinatario);
            SqlParameter _nomUsuEmp = new SqlParameter("@NombreUsuarioEmpleado", S.Empleado.NombreUsuario);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_fechaEntrega);
            oComando.Parameters.Add(_nomDest);
            oComando.Parameters.Add(_dirDest);
            oComando.Parameters.Add(_nomUsuEmp);
            oComando.Parameters.Add(_Retorno);

            SqlTransaction _miTransaccion = null;

            try
            {
                oConexion.Open();
                _miTransaccion = oConexion.BeginTransaction();

                oComando.Transaction = _miTransaccion;
                oComando.ExecuteNonQuery();


                int numeroInterno = Convert.ToInt32(_Retorno.Value);

                if (numeroInterno == -1)
                    throw new Exception(" El usuario que intenta asociar a la factura no existe ");

                foreach (Paquete unPaquete in S.ListaPaquete)
                {
                    PersistenciaSolicitudPaquete.GetInstancia().AltaSolicitudPaquete(numeroInterno, unPaquete.CodigoBarras, _miTransaccion);
                }


                _miTransaccion.Commit();
            }
            catch (Exception ex)
            {
                _miTransaccion.Rollback();
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }

        public void ModificarEstadoSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("SolicitudesDeEntregaModificarEstado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _numeroInterno = new SqlParameter("@NumeroInterno", S.NumeroInterno);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_numeroInterno);
            oComando.Parameters.Add(_Retorno);

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                int return_value = Convert.ToInt32(_Retorno.Value);


                if (return_value == -1)
                    throw new Exception(" La solicitud que intenta modificar no existe ");
                if (return_value == -2)
                    throw new Exception(" Ha habido un error al intentar modificar la solicitud ");

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

        public SolicitudEntrega BuscarSolicitudEntrega(int pNumeroInterno, UsuarioEmpleado ULogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("SolicitudesDeEntregaBuscar", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _numInt = new SqlParameter("@NumeroInterno", pNumeroInterno);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_numInt);
            oComando.Parameters.Add(_Retorno);

            SolicitudEntrega solicitud = null;

            try
            {
                oConexion.Open();
                SqlDataReader dr = oComando.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();

                    DateTime fechaEntr = (DateTime)dr["FechaDeEntrega"];
                    string nombreDest = dr["NombreDestinatario"].ToString();
                    string dirDesti = dr["DireccionDestinatario"].ToString();
                    string estado = dr["EstadoSolicitud"].ToString();
                    string usuEmp = dr["NombreUsuarioEmpleado"].ToString();
                    UsuarioEmpleado empleado = PersistenciaUsuarioEmpleado.GetInstancia().BuscarUsuarioEmpleadoTodos(usuEmp, ULogueado);
                    List<Paquete> listaPaquete = PersistenciaSolicitudPaquete.GetInstancia().ListarPaquetesEnSolicitud(pNumeroInterno, ULogueado);
                    solicitud = new SolicitudEntrega(pNumeroInterno, fechaEntr, nombreDest, dirDesti, estado, empleado, listaPaquete);

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
            return solicitud;
        }


        public List<SolicitudEntrega> ListarSolicitudEntrega()
        {

            SqlConnection oConexion = oConexion = new SqlConnection(Conexion.Cnn());

            SqlCommand oComando = new SqlCommand("SolicitudesDeEntregaListar", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            List<SolicitudEntrega> listSolicitud = new List<SolicitudEntrega>();

            try
            {
                oConexion.Open();
                SqlDataReader dr = oComando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int numInt = (int)dr["NumeroInterno"];
                        DateTime fechaEntr = (DateTime)dr["FechaDeEntrega"];
                        string nombreDest = dr["NombreDestinatario"].ToString();
                        string dirDesti = dr["DireccionDestinatario"].ToString();
                        string estado = dr["EstadoSolicitud"].ToString();
                        string usuEmp = dr["NombreUsuarioEmpleado"].ToString();
                        
                        UsuarioEmpleado empleado = PersistenciaUsuarioEmpleado.GetInstancia().BuscarUsuarioEmpleadoTodos(usuEmp);
                        List<Paquete> listaPaquete = PersistenciaSolicitudPaquete.GetInstancia().ListarPaquetesEnSolicitud(numInt);
                        SolicitudEntrega solicitud = new SolicitudEntrega(numInt, fechaEntr, nombreDest, dirDesti, estado, empleado, listaPaquete);
                        
                        listSolicitud.Add(solicitud);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                oConexion.Close();
            }

            return listSolicitud;

        }



        public List<SolicitudEntrega> ListarSolicitudEntrega(UsuarioEmpleado ULogueado = null)
        {

            SqlConnection oConexion = oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("SolicitudesDeEntregaListar", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            
            List<SolicitudEntrega> listSolicitud = new List<SolicitudEntrega>();

            try
            {
                oConexion.Open();
                SqlDataReader dr = oComando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int numInt = (int)dr["NumeroInterno"];
                        DateTime fechaEntr = (DateTime)dr["FechaDeEntrega"];
                        string nombreDest = dr["NombreDestinatario"].ToString();
                        string dirDesti = dr["DireccionDestinatario"].ToString();
                        string estado = dr["EstadoSolicitud"].ToString();
                        string usuEmp = dr["NombreUsuarioEmpleado"].ToString();
                        
                        UsuarioEmpleado empleado = PersistenciaUsuarioEmpleado.GetInstancia().BuscarUsuarioEmpleadoTodos(usuEmp, ULogueado);
                        List<Paquete> listaPaquete = PersistenciaSolicitudPaquete.GetInstancia().ListarPaquetesEnSolicitud(numInt, ULogueado);
                        SolicitudEntrega solicitud = new SolicitudEntrega(numInt, fechaEntr, nombreDest, dirDesti, estado, empleado, listaPaquete);
                        
                        listSolicitud.Add(solicitud);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                oConexion.Close();
            }

            return listSolicitud;

        }



        public List<SolicitudEntrega> ListarSolicitudEntregaEmpresaLogueada(UsuarioEmpresa ULogueado)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("SolicitudesDeEntregaListarPorEmpresa", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;
            SqlParameter _nomUsu = new SqlParameter("@NombreUsuario", ULogueado.NombreUsuario);

            oComando.Parameters.Add(_nomUsu);



            List<SolicitudEntrega> listSolicitud = new List<SolicitudEntrega>();

            try
            {
                oConexion.Open();
                SqlDataReader dr = oComando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int numInt = (int)dr["NumeroInterno"];
                        DateTime fechaEntr = (DateTime)dr["FechaDeEntrega"];
                        string nombreDest = dr["NombreDestinatario"].ToString();
                        string dirDesti = dr["DireccionDestinatario"].ToString();
                        string estado = dr["EstadoSolicitud"].ToString();
                        string usuEmp = dr["NombreUsuarioEmpleado"].ToString();
                        UsuarioEmpleado empleado = PersistenciaUsuarioEmpleado.GetInstancia().BuscarUsuarioEmpleadoTodos(usuEmp, ULogueado);
                        List<Paquete> listaPaquete = PersistenciaSolicitudPaquete.GetInstancia().ListarPaquetesEnSolicitud(numInt, ULogueado);
                        SolicitudEntrega solicitud = new SolicitudEntrega(numInt, fechaEntr, nombreDest, dirDesti, estado, empleado, listaPaquete);

                        listSolicitud.Add(solicitud);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                oConexion.Close();
            }

            return listSolicitud;
        }
    }
}
