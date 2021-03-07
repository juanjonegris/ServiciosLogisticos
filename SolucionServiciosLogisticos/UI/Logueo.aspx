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
    <div class="container">
            <div class="row mt-5 align-items-center">
            <div class="col align-items-center">
                <form id="form1" runat="server">
                
                    <h3 class="my-5" >Login</h3>
                    <div class="form-row my-2">
                        <label class="mr-1" >Nombre de usuario: </label> 
                        <asp:TextBox ID="txtNombreUsr" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-row">
                      <label class="mr-1" >Contraseña: </label> <asp:TextBox ID="txtContra" runat="server" Type="password"></asp:TextBox>
                    </div>
                    <div class="my-5"  >
                        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" class="btn btn-outline-success mt-5" />
                    </div>

                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <br />
                    <br />
                        <asp:HyperLink ID="hplLogueo" runat="server" NavigateUrl="~/Default.aspx" class="btn btn-primary stretched-link">Volver</asp:HyperLink>        
                </form>
        </div>
    </div>
</div>
</body>
</html>
