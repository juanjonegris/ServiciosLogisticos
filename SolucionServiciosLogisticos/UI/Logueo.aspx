<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logueo.aspx.cs" Inherits="Logueo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <h3>Loguin</h3>
        Nombre de usuario:
            <asp:TextBox ID="txtNombreUsr" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
        
            &nbsp;<br />
        Contraseña: &nbsp;<asp:TextBox ID="txtContra" runat="server"></asp:TextBox>
            <br />
        <br />
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
            <br />
        <br />
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <br />
        <br />
            <asp:HyperLink ID="hplLogueo" runat="server" NavigateUrl="~/Default.aspx">Volver</asp:HyperLink>
       

    </div>
         
    </form>
</body>
</html>
