using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class Logueo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Usuario"] = null;
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        string nomUsu = txtNombreUsr.Text;
        string contra = txtContra.Text;

        try
        {
            Usuario usu = new ServicioClient().LogueoUsuario(nomUsu, contra);

            if (usu != null)
            {
                if ( usu is UsuarioEmpresa )
                {
                    Session["Usuario"] = (UsuarioEmpresa) usu;
                    Response.Redirect("HomeEmpresa.aspx");
                } else
                {
                    Session["Usuario"] = (UsuarioEmpleado)usu;
                    Response.Redirect("HomeEmpleado.aspx");
                }
            }
            else
            {
                throw new Exception("No se ha encontrado el usuario");
            }
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

    }
}