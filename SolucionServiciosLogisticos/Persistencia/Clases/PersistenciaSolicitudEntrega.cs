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


        public void AgregarSolicitudEntrega(SolicitudEntrega S, Usuario ULogueado)
        {

            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(ULogueado));

            SqlCommand oComando = new SqlCommand("SolicitudesDeEntregaAlta", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _fechaEntrega = new SqlParameter( "@FechaDeEntrega", S.FechaEntrega );
            SqlParameter _nomDest = new SqlParameter( "@NombreDestinatario", S.NombreDestinatario );
            SqlParameter _dirDest = new SqlParameter( "@DireccionDestinatario", S.DireccionDestinatario );
            SqlParameter _nomUsuEmp = new SqlParameter( "@NombreUsuarioEmpleado", S.Empleado.NombreUsuario );
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
    }
}
