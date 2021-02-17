<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaquetesHome.aspx.cs" Inherits="PaquetesHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Xml ID="XmlPaq" runat="server" TransformSource="~/Paquetes.xslt"></asp:Xml>
        <br />
        <br />
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <asp:LinkButton ID="lnbVolver" runat="server" OnClick="lnbVolver_Click">Volver</asp:LinkButton>
        <br />
    
    </div>
    </form>
</body>
</html>
