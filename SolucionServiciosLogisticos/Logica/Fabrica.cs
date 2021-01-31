using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;
using EC;
using Logica.Interfaces;
using Logica.Clases;

namespace Logica
{
    public class Fabrica
    {
        public static ILogicaUsuario GetLU()
        {
            return LogicaUsuario.GetInstancia();
        }

        public static ILogicaSolicitudEntrega GetLS()
        {
            return LogicaSolicitudEntrega.GetInstancia();
        }

        public static ILogicaPaquete GetLP()
        {
            return LogicaPaquete.GetInstancia();
        }
    }
}
