using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;


public partial class CambioEstadoSolicitud : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !IsPostBack)
        {
            try
            {
                UsuarioEmpleado usuemp = (UsuarioEmpleado)Session["Usuario"];
                
                List<SolicitudEntrega> lista = (from unaSE in (new ServicioClient().ListarSolicitudEntregaEstado(usuemp).ToList())
                                               where unaSE.Estado == "En Camino" || unaSE.Estado == "En Depósito"
                                               select unaSE).ToList<SolicitudEntrega>();

                Session["Solicitudes"] = lista;
                grvSolEnt.DataSource = lista;
                grvSolEnt.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
            
        }
    }

    protected void grvSolEnt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            UsuarioEmpleado usuemp = (UsuarioEmpleado)Session["Usuario"];
            List<SolicitudEntrega> lista = (List<SolicitudEntrega>)Session["Solicitudes"];
            new ServicioClient().ModificarEstadoSolicitudEntrega(lista[grvSolEnt.SelectedIndex], usuemp);
            List<SolicitudEntrega> listaNueva = (from unaSE in (new ServicioClient().ListarSolicitudEntregaEstado(usuemp).ToList())
                                            where unaSE.Estado == "En Camino" || unaSE.Estado == "En Depósito"
                                            select unaSE).ToList<SolicitudEntrega>();
            
            grvSolEnt.DataSource = listaNueva;
            grvSolEnt.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

    }
}