using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC;


namespace Persistencia.Interfaces
{
    public interface IPersistenciaPaquete
    {
        void AgregarPaquete(Paquete P, Usuario ULogueado);
        Paquete BuscarPaquete(int pCodigoBarras, Usuario ULogueado);
        List<Paquete> ListarPaquetesSinSolicitud(Usuario ULogueado);
        List<Paquete> ListarPaquetesEnSolicitud(int numeroInterno, Usuario ULogueado);
    }
}
