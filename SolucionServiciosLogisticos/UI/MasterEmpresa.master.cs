using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class MasterEmpresa : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Session["Usuario"] is UsuarioEmpresa))
            Response.Redirect("Default.aspx");

        UsuarioEmpresa empresa = (UsuarioEmpresa)Session["Usuario"];
        lblBienvenido.Text = empresa.Nombre;
    }

    protected void btnDestroySession_Click(object sender, EventArgs e)
    {
        Session["Usuario"] = null;
        Response.Redirect("Default.aspx");
    }
}
