using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class AltaSolicituddeEntrega : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !IsPostBack)
        {
            Session["PaqueteSeleccion"] = new List<Paquete>();
            try
            {
                UsuarioEmpleado usuemp = (UsuarioEmpleado)Session["Usuario"];
                List<Paquete> listapaq = new ServicioClient().ListarPaquetesSinSolicitud(usuemp).ToList();
                Session["Paquetes"] = listapaq;
                grvPaquetes.DataSource = listapaq;
                grvPaquetes.DataBind();
                btnAgregar.Enabled = false;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }

        }
    }

    protected void grvPaquetes_SelectedIndexChanged(object sender, EventArgs e)
    {
        Paquete paq = null;
        try
        {
            List<Paquete> lista_paq_asignados = (List<Paquete>)Session["PaqueteSeleccion"];
            List<Paquete> lista_paq_sin_asignar = (List<Paquete>)Session["Paquetes"];
            UsuarioEmpleado useremp = (UsuarioEmpleado)Session["Usuario"];

            paq = lista_paq_sin_asignar[grvPaquetes.SelectedIndex];

            lista_paq_asignados.Add(paq);

            grvAsignados.DataSource = lista_paq_asignados;
            grvAsignados.DataBind();
            Session["PaqueteSeleccion"] = lista_paq_asignados; 


            lista_paq_sin_asignar.RemoveAt(grvPaquetes.SelectedIndex);
            Session["Paquetes"] = lista_paq_sin_asignar;
            grvPaquetes.DataSource = lista_paq_sin_asignar;
            grvPaquetes.DataBind();
            btnAgregar.Enabled = true;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        

    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        SolicitudEntrega sol_ent = null;

        try
        {
            UsuarioEmpleado usuemp = (UsuarioEmpleado)Session["Usuario"];
            List<Paquete> paquetes = (List<Paquete>)Session["PaqueteSeleccion"];
            DateTime fecha_entrega = Convert.ToDateTime(txtFecha.Text);
            string nombre_dest = txtNombre.Text;
            string direccion = txtDireccion.Text;

            sol_ent = new SolicitudEntrega
            {
                NumeroInterno = 0,
                FechaEntrega = fecha_entrega,
                NombreDestinatario = nombre_dest,
                DireccionDestinatario = direccion,
                Estado = "En Depósito",
                Empleado = usuemp,
                ListaPaquete = paquetes.ToArray()
            };
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

        try
        {
            new ServicioClient().AgregarSolicitudEntrega(sol_ent, sol_ent.Empleado);
            lblMensaje.Text = "Solicitud de entrega agregada con éxito";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtFecha.Text = "";
            List<Paquete> lista_paq_sin_asignar = (List<Paquete>)Session["Paquetes"];
            grvPaquetes.DataSource = lista_paq_sin_asignar;
            grvPaquetes.DataBind();
            btnAgregar.Enabled = false;
            Session["PaqueteSeleccion"] = new List<Paquete>();
            List<Paquete> lista_paq_asignados = (List<Paquete>)Session["PaqueteSeleccion"];
            grvAsignados.DataSource = lista_paq_asignados;
            grvAsignados.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    
}