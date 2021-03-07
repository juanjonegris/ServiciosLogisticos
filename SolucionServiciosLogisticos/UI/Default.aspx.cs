using RefWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !IsPostBack )
        {
            try
            {
                XElement _documento = XElement.Parse(new ServicioClient().ListarSolicitudEntrega());
                Session["SolicitudesEntregas"] = _documento;

                desplegarEntregas(_documento);
            }
            catch (Exception ex)
            {

                lblMensaje.Text = ex.Message;
            }
        } 
    }

    protected void desplegarEntregas(XElement _documento)
    {
        List<object> solEnt = (from unaS in (_documento.Elements("SolicitudEntrega"))
                               where unaS.Element("Estado").Value == "En Camino"
                               select new
                               {
                                   NumeroInterno = unaS.Element("NumeroInterno").Value,
                                   FechaEntrega = unaS.Element("FechaEntrega").Value,
                                   NombreDestinatario = unaS.Element("NombreDestinatario").Value,
                                   DireccionDestinatario = unaS.Element("DireccionDestinatario").Value,
                                   NombreEmpleado = unaS.Element("UsuarioEmpleado").Element("NombreUsuario")
                               }
                                    ).ToList<object>();

        Session["SolicitudLista"] = solEnt;
        grvSolicitudes.DataSource = solEnt;
        grvSolicitudes.DataBind();
    }

    protected void grvSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            XmlDocument _documento = new XmlDocument();
            _documento.LoadXml(Session["SolicitudesEntregas"].ToString());
            XmlNode nodo = _documento.DocumentElement.ChildNodes[grvSolicitudes.SelectedIndex];
            XmlPaquetes.DocumentContent = nodo.OuterXml;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime fecha = Convert.ToDateTime(txtFilDia.Text + "/" + txtFilMes.Text + "/" + txtFilAnio.Text);
            XElement _documento = (XElement)Session["SolicitudesEntregas"];

            List<object> solFil = (from unaS in (_documento.Elements("SolicitudEntrega"))
                                   where Convert.ToDateTime(unaS.Element("FechaEntrega").Value).Date == fecha.Date
                                   select new
                                   {
                                       NumeroInterno = unaS.Element("NumeroInterno").Value,
                                       FechaEntrega = unaS.Element("FechaEntrega").Value,
                                       NombreDestinatario = unaS.Element("NombreDestinatario").Value,
                                       DireccionDestinatario = unaS.Element("DireccionDestinatario").Value,
                                       NombreEmpleado = unaS.Element("UsuarioEmpleado").Element("NombreUsuario")
                                   }
                                        ).ToList<object>();

            grvSolicitudes.DataSource = solFil;
            grvSolicitudes.DataBind();

        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
    {
        try
        {
            txtFilDia.Text = "";
            txtFilMes.Text = "";
            txtFilAnio.Text = "";
            //XElement _documento = (XElement)Session["SolicitudesEntregas"];
            //desplegarEntregas(_documento);
            List<object> solEnt = (List<object>)Session["SolicitudLista"];
            grvSolicitudes.DataSource = solEnt;
            grvSolicitudes.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        
    }
}