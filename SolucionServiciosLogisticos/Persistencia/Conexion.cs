using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC;
namespace Persistencia
{
    internal class Conexion
    {
            internal static string Cnn(Usuario pUsu = null)
            {
                if (pUsu == null)
                    return "Data Source=LAPTOP-ITOE3H9S; Initial Catalog = Entregas; Integrated Security = true";
                else
                    return "Data Source=LAPTOP-ITOE3H9S; Initial Catalog = EjemploExtraBD; User=" + pUsu.NombreUsuario + "; Password='" + pUsu.Contrasenia + "'";
            }
    }
}
