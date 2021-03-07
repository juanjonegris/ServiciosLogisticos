using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class ABMEmpresa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LimpiarFormulario();
        
    }

    private void LimpiarFormulario( string msg = null )
    {
        txtNomUsu.Text = "";
        txtContra.Text = "";
        txtNombre.Text = "";
        txtTelefono.Text = "";
        txtDireccion.Text = "";
        txtEmail.Text = "";

        if ( msg == null ) lblMensaje.Text = "";

        txtContra.Enabled = false;
        txtNombre.Enabled = false;
        txtTelefono.Enabled = false;
        txtDireccion.Enabled = false;
        txtEmail.Enabled = false;

        btnBuscar.Enabled = true;

    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            string nomusu = txtNomUsu.Text;
            UsuarioEmpleado usuLogueado = (UsuarioEmpleado)Session["Usuario"];
            UsuarioEmpresa usuempresa = (UsuarioEmpresa)new ServicioClient().BuscarUsuario(nomusu, usuLogueado);

            if (usuempresa != null)
            {
                // Modificar eliminar
                Session["Empresa"] = usuempresa;
                btnModificar.Enabled = true;
                btnBaja.Enabled = true;
                btnBuscar.Enabled = false;
                btnAlta.Enabled = false;
                txtContra.Enabled = true;
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                txtDireccion.Enabled = true;
                txtEmail.Enabled = true;
                txtContra.Enabled = false;

                lblMensaje.Text = "Usuario Empresa " + usuempresa.Nombre + " existe. Quiere modificar o eliminar";
                txtNombre.Text = usuempresa.Nombre;
                txtTelefono.Text = usuempresa.Telefono;
                txtDireccion.Text = usuempresa.Direccion;
                txtEmail.Text = usuempresa.Email;

            }
            else
            {
                //Alta
                Session["Empresa"] = null;
                txtContra.Enabled = true;
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                txtDireccion.Enabled = true;
                txtEmail.Enabled = true;

                btnAlta.Enabled = true;
                btnBaja.Enabled = false;
                btnModificar.Enabled = false;
                btnBuscar.Enabled = false;
                lblMensaje.Text = "La Empresa no existe. ¿Quiere darla de alta?";
            }
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    protected void btnAlta_Click(object sender, EventArgs e)
    {
        UsuarioEmpresa usuempresa = null;

        try
        {

            usuempresa = new UsuarioEmpresa
            {
                NombreUsuario = txtNomUsu.Text,
                Contrasenia = txtContra.Text,
                Nombre = txtNombre.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,
                Email = txtEmail.Text
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
            new ServicioClient().AgregarUsuario(usuempresa, usuLogueado);
            LimpiarFormulario();
            btnAlta.Enabled = false;
            btnBaja.Enabled = false;
            btnModificar.Enabled = false;
            lblMensaje.Text = "Se ha dado de alta a la empresa " + usuempresa.Nombre;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            UsuarioEmpleado usuLogueado = (UsuarioEmpleado)Session["Usuario"];
            UsuarioEmpresa usuempresa = (UsuarioEmpresa)Session["Empresa"];
            usuempresa.NombreUsuario = txtNomUsu.Text;
            usuempresa.Nombre = txtNombre.Text;
            usuempresa.Telefono = txtTelefono.Text;
            usuempresa.Direccion = txtDireccion.Text;
            usuempresa.Email = txtEmail.Text;

            new ServicioClient().ModificarUsuario(usuempresa, usuLogueado);
            lblMensaje.Text = "Se ha modificado la empresa con éxito";
            LimpiarFormulario("limpiar");

            btnAlta.Enabled = false;
            btnBaja.Enabled = false;
            btnModificar.Enabled = false;
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
            UsuarioEmpresa usuempresa = (UsuarioEmpresa)Session["Empresa"];
            new ServicioClient().EliminarUsuario(usuempresa, usuLogueado);

            lblMensaje.Text = "Se ha dado de baja la empresa de forma exitosa";
            LimpiarFormulario("mensaje");

            btnAlta.Enabled = false;
            btnModificar.Enabled = false;
            btnAlta.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}