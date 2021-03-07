using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class MasterEmpleado : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!(Session["Usuario"] is UsuarioEmpleado))
                Response.Redirect("Default.aspx");

            UsuarioEmpleado empleado = (UsuarioEmpleado)Session["Usuario"];
            lblBienvenido.Text = empleado.Nombre;
        }
        catch (Exception ex)
        {

            lblBienvenido.Text = ex.Message;
        }
        


    }

    protected void btnDestroySession_Click(object sender, EventArgs e)
    {
        Session["Usuario"] = null;
        Response.Redirect("Default.aspx");
    }
}
