using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;


public partial class AltaPaquetes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LimpiarFormulario();
    }

    private void LimpiarFormulario( string msg = null )
    {
        txtCodigo.Text ="";
        txtNombreUsuario.Text = "";
        txtPeso.Text = "";
        txtDescripcion.Text = "";
        btnAlta.Enabled = false;

        if (msg != null) lblMensaje.Text = "";
        
    }

    protected void btnBuscarUsuario_Click(object sender, EventArgs e)
    {
        try
        {
            string nomusu = txtNombreUsuario.Text;
            UsuarioEmpleado usuLogueado = (UsuarioEmpleado)Session["Usuario"];
            UsuarioEmpresa usuempresa = (UsuarioEmpresa)new ServicioClient().BuscarUsuario(nomusu, usuLogueado);
            if (usuempresa != null)
            {
                Session["Empresa"] = usuempresa;
                lblNombreEmpresa.Text = "Se ha encontrado el usuario de la empresa, nombre " + usuempresa.Nombre;
                btnAlta.Enabled = true;
            } else
            {
                lblMensaje.Text = "No se encontró la empresa. Necesita asignar la Empresa propietaria para dar de alta elpaquete";
            }
            
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnAlta_Click(object sender, EventArgs e)
    {
        Paquete paquete = null;
        UsuarioEmpresa uempresa = (UsuarioEmpresa)Session["Empresa"];
        try
        {
            paquete = new Paquete
            {
                CodigoBarras = Convert.ToInt32(txtCodigo.Text),
                Tipo = ddlTipo.SelectedValue,
                Descripcion = txtDescripcion.Text,
                Peso = Convert.ToDecimal(txtPeso.Text),
                Empresa = uempresa
            };
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        try
        {
            UsuarioEmpleado usuLogueado = (UsuarioEmpleado)Session["Usuario"];
            new ServicioClient().AgregarPaquete(paquete, usuLogueado);
            lblMensaje.Text = "Se ha ingresado el paquete con exito";
            LimpiarFormulario();
            lblNombreEmpresa.Text = "";


        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFormulario("mensaje");
    }
}