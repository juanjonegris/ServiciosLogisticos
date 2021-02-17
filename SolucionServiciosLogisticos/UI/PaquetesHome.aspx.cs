using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class PaquetesHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int index_sol = (int)Session["sol_index"];
            XmlDocument _documento = new XmlDocument();
            string doc = Session["SolicitudesEntregas"].ToString();
            _documento.LoadXml(doc);
            XmlNode nodo = _documento.DocumentElement.ChildNodes[index_sol];
            XmlPaq.DocumentContent = nodo.OuterXml;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void lnbVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}