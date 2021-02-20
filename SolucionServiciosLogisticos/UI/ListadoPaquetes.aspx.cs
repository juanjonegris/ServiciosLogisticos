using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class ListadoPaquetes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UsuarioEmpleado usuemp = (UsuarioEmpleado)Session["Usuario"];
            List<Paquete> listapaq = new ServicioClient().ListarPaquetesSinSolicitud(usuemp).ToList();
            grvListPaquetes.DataSource = listapaq;
            grvListPaquetes.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}