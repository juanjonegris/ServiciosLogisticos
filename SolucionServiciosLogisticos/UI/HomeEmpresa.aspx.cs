using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class HomeEmpresa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UsuarioEmpresa usr = (UsuarioEmpresa)Session["Usuario"];
        lblMensaje.Text = "Bienvenido al panel de Usuarios Empleados - " + usr.Nombre;
    }
}