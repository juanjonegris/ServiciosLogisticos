using EC;
using Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Logica.Clases
{
    internal class LogicaSolicitudEntrega : ILogicaSolicitudEntrega
    {
        #region singleton
        private static LogicaSolicitudEntrega _instancia = null;

        private LogicaSolicitudEntrega() { }

        public static LogicaSolicitudEntrega GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaSolicitudEntrega();

            return _instancia;
        }
        #endregion

        public void AgregarSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado)
        {
            if (DateTime.Now > S.FechaEntrega)
                throw new Exception("La fecha de entrega no puede ser anterior a la actual");

            Persistencia.Fabrica.GetPS().AgregarSolicitudEntrega(S, ULogueado);

        }

        public SolicitudEntrega BuscarSolicitudEntrega(int pNumeroInterno, UsuarioEmpleado ULogueado)
        {
            return (Persistencia.Fabrica.GetPS().BuscarSolicitudEntrega(pNumeroInterno, ULogueado));
        }

        public void ModificarEstadoSolicitudEntrega(SolicitudEntrega S, UsuarioEmpleado ULogueado)
        {
            Persistencia.Fabrica.GetPS().ModificarEstadoSolicitudEntrega(S, ULogueado);
        }

        public XmlDocument ListarSolicitudEntrega()
        {
            XmlDocument _DocumentXml = new XmlDocument();
            _DocumentXml.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <Raiz> </Raiz>");
            XmlNode _raiz = _DocumentXml.DocumentElement;
            List<SolicitudEntrega> _lista_solicitudes = Persistencia.Fabrica.GetPS().ListarSolicitudEntrega();

            foreach (SolicitudEntrega _solicitudes in _lista_solicitudes)
            {
                //Nodo SolicitudEntrega
                XmlElement _NodoSol = _DocumentXml.CreateElement("SolicitudEntrega");

                XmlElement NumeroInterno = _DocumentXml.CreateElement("NumeroInterno");
                NumeroInterno.InnerText = _solicitudes.NumeroInterno.ToString();
                _NodoSol.AppendChild(NumeroInterno);

                XmlElement FechaEntrega = _DocumentXml.CreateElement("FechaEntrega");
                FechaEntrega.InnerText = _solicitudes.FechaEntrega.ToString();
                _NodoSol.AppendChild(FechaEntrega);


                XmlElement NombreDestinatario = _DocumentXml.CreateElement("NombreDestinatario");
                NombreDestinatario.InnerText = _solicitudes.NombreDestinatario.ToString();
                _NodoSol.AppendChild(NombreDestinatario);

                XmlElement DireccionDestinatario = _DocumentXml.CreateElement("DireccionDestinatario");
                DireccionDestinatario.InnerText = _solicitudes.DireccionDestinatario.ToString();
                _NodoSol.AppendChild(DireccionDestinatario);

                XmlElement Estado = _DocumentXml.CreateElement("Estado");
                Estado.InnerText = _solicitudes.Estado.ToString();
                _NodoSol.AppendChild(Estado);

                // Nodo UsuarioEmpleado 

                XmlElement _NodoUE = _DocumentXml.CreateElement("UsuarioEmpleado");

                XmlElement NombreUsuario = _DocumentXml.CreateElement("NombreUsuario");
                NombreUsuario.InnerText = _solicitudes.Empleado.NombreUsuario.ToString();
                _NodoUE.AppendChild(NombreUsuario);

                XmlElement Contrasenia = _DocumentXml.CreateElement("Contrasenia");
                Contrasenia.InnerText = _solicitudes.Empleado.Contrasenia.ToString();
                _NodoUE.AppendChild(Contrasenia);

                XmlElement Nombre = _DocumentXml.CreateElement("Nombre");
                Nombre.InnerText = _solicitudes.Empleado.Nombre.ToString();
                _NodoUE.AppendChild(Nombre);

                XmlElement HoraInicio = _DocumentXml.CreateElement("HoraInicio");
                HoraInicio.InnerText = _solicitudes.Empleado.HoraInicio.ToString();
                _NodoUE.AppendChild(HoraInicio);

                XmlElement HoraFin = _DocumentXml.CreateElement("HoraFin");
                HoraFin.InnerText = _solicitudes.Empleado.HoraFin.ToString();
                _NodoUE.AppendChild(HoraFin);

                _NodoSol.AppendChild(_NodoUE);


                foreach (Paquete _paquete in _solicitudes.ListaPaquete)
                {
                    // Paquetes
                    XmlElement _NodoP = _DocumentXml.CreateElement("Paquete");


                    XmlElement CodigoBarras = _DocumentXml.CreateElement("CodigoBarras");
                    CodigoBarras.InnerText = _paquete.CodigoBarras.ToString();
                    _NodoP.AppendChild(CodigoBarras);


                    XmlElement Tipo = _DocumentXml.CreateElement("Tipo");
                    Tipo.InnerText = _paquete.Tipo.ToString();
                    _NodoP.AppendChild(Tipo);

                    XmlElement Descripcion = _DocumentXml.CreateElement("Descripcion");
                    Descripcion.InnerText = _paquete.Descripcion.ToString();
                    _NodoP.AppendChild(Descripcion);

                    XmlElement Peso = _DocumentXml.CreateElement("Peso");
                    Peso.InnerText = _paquete.Peso.ToString();
                    _NodoP.AppendChild(Peso);


                    // Nodo UsuarioEmpresa 

                    XmlElement _NodoUEmpresa = _DocumentXml.CreateElement("UsuarioEmpresa");

                    XmlElement NombreUsuarioEmp = _DocumentXml.CreateElement("NombreUsuario");
                    NombreUsuarioEmp.InnerText = _paquete.Empresa.NombreUsuario.ToString();
                    _NodoUEmpresa.AppendChild(NombreUsuarioEmp);


                    XmlElement ContraseniaEmp = _DocumentXml.CreateElement("Contrasenia");
                    ContraseniaEmp.InnerText = _paquete.Empresa.Contrasenia.ToString();
                    _NodoUEmpresa.AppendChild(ContraseniaEmp);

                    XmlElement NombreEmp = _DocumentXml.CreateElement("Nombre");
                    NombreEmp.InnerText = _paquete.Empresa.Nombre.ToString();
                    _NodoUEmpresa.AppendChild(NombreEmp);

                    XmlElement Telefono = _DocumentXml.CreateElement("Telefono");
                    Telefono.InnerText = _paquete.Empresa.Telefono.ToString();
                    _NodoUEmpresa.AppendChild(Telefono);

                    XmlElement Direccion = _DocumentXml.CreateElement("Direccion");
                    Direccion.InnerText = _paquete.Empresa.Direccion.ToString();
                    _NodoUEmpresa.AppendChild(Direccion);

                    XmlElement Email = _DocumentXml.CreateElement("Email");
                    Email.InnerText = _paquete.Empresa.Email.ToString();
                    _NodoUEmpresa.AppendChild(Email);

                    _NodoP.AppendChild(_NodoUEmpresa);

                    _NodoSol.AppendChild(_NodoP);
                }

                
                _raiz.AppendChild(_NodoSol);

            }
            return _DocumentXml;
        }

        public List<SolicitudEntrega> ListarSolicitudEntregaEmpresaLogueada(UsuarioEmpresa ULogueado)
        {
            return (Persistencia.Fabrica.GetPS().ListarSolicitudEntregaEmpresaLogueada(ULogueado));
        }

        public List<SolicitudEntrega> ListarSolicitudEntregaEstado(UsuarioEmpleado ULogueado)
        {
            return (Persistencia.Fabrica.GetPS().ListarSolicitudEntrega( ULogueado ));
        }
    }
}
