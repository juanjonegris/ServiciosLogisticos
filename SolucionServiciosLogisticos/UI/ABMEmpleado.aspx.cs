using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class ABMEmpleado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LimpioFormulario();
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            string nomusu = txtNomUsu.Text;
            UsuarioEmpleado usuLogueado = (UsuarioEmpleado)Session["Usuario"];
            UsuarioEmpleado usuemp = (UsuarioEmpleado)new ServicioClient().BuscarUsuario(nomusu, usuLogueado);

            if(usuemp != null)
            {
                // Modificar eliminar
                Session["Empleado"] = usuemp;
                btnModificacion.Enabled = true;
                btnBaja.Enabled = true;
                btnBuscar.Enabled = false;
                txtContra.Enabled = false;
                txtNombre.Enabled = true;
                txtHoraFin.Enabled = true;
                txtHoraInicio.Enabled = true;
                txtMinutoFin.Enabled = true;
                txtMinutoIncio.Enabled = true;
                lblMensaje.Text = "El Empleado " + usuemp.Nombre + " existe. ";
                txtNombre.Text = usuemp.Nombre;
                txtHoraInicio.Text = usuemp.HoraInicio.Substring(0, 2);
                txtMinutoIncio.Text = usuemp.HoraInicio.Substring(3, 2);
                txtHoraFin.Text = usuemp.HoraFin.Substring(0, 2);
                txtMinutoFin.Text = usuemp.HoraFin.Substring(3, 2);
            } else
            {
                //Alta
                Session["Empleado"] = null;
                txtContra.Enabled = true;
                txtNombre.Enabled = true;
                txtHoraFin.Enabled = true;
                txtHoraInicio.Enabled = true;
                txtMinutoFin.Enabled = true;
                txtMinutoIncio.Enabled = true;
                btnAlta.Enabled = true;
                btnBaja.Enabled = false;
                btnModificacion.Enabled = false;
                lblMensaje.Text = "El Empleado no existe. ¿Quiere darlo de alta?";
            }
        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }



    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        this.LimpioFormulario();
    }

    private void LimpioFormulario( string msg = null)
    {
        txtNomUsu.Text = "";
        txtContra.Text = "";
        txtNombre.Text = "";
        txtHoraInicio.Text = "";
        txtMinutoIncio.Text = "";
        txtHoraFin.Text = "";
        txtMinutoFin.Text = "";

        if (msg == null) lblMensaje.Text = "";

        txtContra.Enabled = false;
        txtNombre.Enabled = false;
        txtHoraInicio.Enabled = false;
        txtMinutoIncio.Enabled = false;
        txtHoraFin.Enabled = false;
        txtMinutoFin.Enabled = false;
        btnBuscar.Enabled = true;
    }




    protected void btnAlta_Click(object sender, EventArgs e)
    {
        UsuarioEmpleado usuemp = null;

        try
        {

            usuemp = new UsuarioEmpleado
            {
                NombreUsuario = txtNomUsu.Text,
                Contrasenia = txtContra.Text,
                Nombre = txtNombre.Text,
                HoraInicio = txtHoraInicio.Text + ':' + txtMinutoIncio.Text,
                HoraFin = txtHoraFin.Text + ':' + txtMinutoFin.Text
            };

        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
            return;
        }

        try
        {
            UsuarioEmpleado usuLogueado = (UsuarioEmpleado)Session["Usuario"];
            new ServicioClient().AgregarUsuario(usuemp, usuLogueado);
            LimpioFormulario();
            btnAlta.Enabled = false;
            btnBaja.Enabled = false;
            btnModificacion.Enabled = false;
            lblMensaje.Text = "Se ha dado de alta al usuario "+ usuemp.Nombre;
        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnModificacion_Click(object sender, EventArgs e)
    {
        try
        {
            UsuarioEmpleado usuLogueado = (UsuarioEmpleado)Session["Usuario"];
            UsuarioEmpleado usuemp = (UsuarioEmpleado)Session["Empleado"];
            usuemp.NombreUsuario = txtNomUsu.Text;
            usuemp.Nombre = txtNombre.Text;
            usuemp.HoraInicio = txtHoraInicio.Text + ':' + txtMinutoIncio.Text;
            usuemp.HoraFin = txtHoraFin.Text + ':' + txtMinutoFin.Text;

            new ServicioClient().ModificarUsuario(usuemp, usuLogueado);
            lblMensaje.Text = "Se ha modificado el usuario con éxito";
            LimpioFormulario("limpiar");

            btnAlta.Enabled = false;
            btnBaja.Enabled = false;
            btnModificacion.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnBaja_Click(object sender, EventArgs e)
    {
        try
        {
            UsuarioEmpleado usuLogueado = (UsuarioEmpleado)Session["Usuario"];
            UsuarioEmpleado usuemp = (UsuarioEmpleado)Session["Empleado"];
            new ServicioClient().EliminarUsuario(usuemp, usuLogueado);

            lblMensaje.Text = "Se ha dado de baja al usuario de forma exitosa";
            LimpioFormulario();

            btnAlta.Enabled = false;
            btnModificacion.Enabled = false;
            btnAlta.Enabled = false;
            btnBaja.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}