﻿using RefWCF;
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
            catch (Exception ex)
            {

                lblMensaje.Text = ex.Message;
            }
        } 
        else
        {
            try
            {
                grvSolicitudes.DataSource = Session["SolicitudLista"];
                grvSolicitudes.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

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
        string fecha = txtFilDia.Text + "/" + txtFilMes.Text + "/" + txtFilAnio.Text+ "0:00:00";
        XElement _documento = (XElement)Session["SolicitudesEntregas"];
        
        List<object> solFil = (from unaS in (_documento.Elements("SolicitudEntrega"))
                               where unaS.Element("FechaEntrega").Value == fecha
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
}