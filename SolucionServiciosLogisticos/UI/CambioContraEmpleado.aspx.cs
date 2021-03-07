using RefWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CambioContraEmpleado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCambia_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario user = (Usuario)Session["Usuario"];
            string nuevaContra = txtNuevaContra.Text;

            new ServicioClient().CambioContrasena(nuevaContra, user);
            lblMensaje.Text = "Contraseña cambiada con éxito";

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void rtnLimpiar_Click(object sender, EventArgs e)
    {
        txtNuevaContra.Text = "";
        lblMensaje.Text = "";
    }
}