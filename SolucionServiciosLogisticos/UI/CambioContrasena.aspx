<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpresa.master" AutoEventWireup="true" CodeFile="CambioContrasena.aspx.cs" Inherits="CambioContrasena" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">

        <br />
        <p class="h3"> Ingrese la nueva Contraseña</p><br/>
        <br />
        <asp:TextBox ID="txtNuevaContra" runat="server" type="password"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNuevaContra" Display="Dynamic" ErrorMessage="Contraseña inválida" ValidationExpression="[a-zA-Z][a-zA-Z][a-zA-Z][0-9][0-9][^a-zA-Z0-9]" ValidationGroup="contrasena"></asp:RegularExpressionValidator>
        <br />
        <br />
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnCambia" runat="server" Text="Cambiar" OnClick="btnCambia_Click" ValidationGroup="contrasena" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="rtnLimpiar" runat="server" Text="Limpiar" OnClick="rtnLimpiar_Click" />
        <br />
        <br />
        <br />
    </div>

</asp:Content>

