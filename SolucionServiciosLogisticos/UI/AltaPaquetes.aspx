<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpleado.master" AutoEventWireup="true" CodeFile="AltaPaquetes.aspx.cs" Inherits="AltaPaquetes" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 179px;
        }
        .auto-style2 {
            width: 170px;
        }
        .auto-style3 {
            width: 179px;
            height: 40px;
        }
        .auto-style4 {
            width: 170px;
            height: 40px;
        }
        .auto-style5 {
            height: 40px;
        }
    </style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style2 h3">Alta de Paquetes</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style2 h6">Empresa</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">Nombre de Usuario</td>
            <td class="auto-style2">
                <asp:TextBox ID="txtNombreUsuario" runat="server"></asp:TextBox>
            </td>
        <tr>
            <td>
                <asp:Button ID="btnBuscarUsuario" runat="server" OnClick="btnBuscarUsuario_Click" Text="Buscar Empresa" Width="150px" class="btn btn-success" />
            </td>
        </tr>
        </tr>
        <tr>
            <td class="auto-style1"></td>
            <td class="auto-style2">
                <asp:Label ID="lblNombreEmpresa" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="auto-style1">Codigo de Barras</td>
            <td class="auto-style2">
                <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">Tipo</td>
            <td class="auto-style2">
                <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="fragil">Fragil</asp:ListItem>
                    <asp:ListItem Value="común">Común</asp:ListItem>
                    <asp:ListItem Value="bulto">Bulto</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">Descripción</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtDescripcion" runat="server" Rows="5"></asp:TextBox>
            </td>
            <td class="auto-style5">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">Peso</td>
            <td class="auto-style2">
                <asp:TextBox ID="txtPeso" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style2">
                <asp:Button ID="btnAlta" runat="server" Enabled="False" OnClick="btnAlta_Click" Text="Ingresar" class="btn btn-success" />
            </td>
            <td>
                <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar" class="btn btn-success"/>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style2">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

