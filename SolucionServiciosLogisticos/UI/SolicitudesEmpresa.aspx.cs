using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class SolicitudesEmpresa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UsuarioEmpresa usuemp = (UsuarioEmpresa)Session["Usuario"];
            List<SolicitudEntrega> listasol = new ServicioClient().ListarSolicitudEntregaEmpresaLogueada(usuemp).ToList();
            Session["listaSolEmpresa"] = listasol;
            grvSolEmpresa.DataSource = listasol;
            grvSolEmpresa.DataBind();

        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnMes_Click(object sender, EventArgs e)
    {
        int month = DateTime.Now.Month;
        
        List<SolicitudEntrega> listasol = (List<SolicitudEntrega>)Session["listaSolEmpresa"];
        List<object> lista = (from unaS in listasol
                              where unaS.FechaEntrega.Month == month
                              group unaS by unaS.Estado into grupo
                              select new
                              {
                                Estado = grupo.First().Estado,
                                Cantidad = grupo.Count()
                              }).ToList<object>();

        grvSolEmpresa.DataSource = lista;
        grvSolEmpresa.DataBind();
    }

    protected void btnMesAño_Click(object sender, EventArgs e)
    {
        List<SolicitudEntrega> listasol = (List<SolicitudEntrega>)Session["listaSolEmpresa"];

        List<object> lista = (from unaS in listasol
                              group unaS by new { unaS.FechaEntrega.Year, unaS.FechaEntrega.Month } into grupo
                              select new
                              {
                                  Año =  grupo.Key.Year,
                                  Mes = grupo.Key.Month,
                                  Cantidad = grupo.Count()

                              }).ToList<object>();

        grvSolEmpresa.DataSource = lista;
        grvSolEmpresa.DataBind();
    }

    protected void cldFecha_SelectionChanged(object sender, EventArgs e)
    {
        DateTime selecteddate = cldFecha.SelectedDate;

        List<SolicitudEntrega> listasol = (List<SolicitudEntrega>)Session["listaSolEmpresa"];
        List<SolicitudEntrega> lista = (from unaS in listasol
                              where unaS.FechaEntrega.Date == selecteddate.Date
                              select unaS).ToList<SolicitudEntrega>();

        grvSolEmpresa.DataSource = lista;
        grvSolEmpresa.DataBind();
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        try
        {
            UsuarioEmpresa usuemp = (UsuarioEmpresa)Session["Usuario"];
            List<SolicitudEntrega> listasol = (List<SolicitudEntrega>)Session["listaSolEmpresa"];
            grvSolEmpresa.DataSource = listasol;
            grvSolEmpresa.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}