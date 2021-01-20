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
        
    }
}
