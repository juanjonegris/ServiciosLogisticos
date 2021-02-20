<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpresa.master" AutoEventWireup="true" CodeFile="SolicitudesEmpresa.aspx.cs" Inherits="SolicitudesEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 295px;
        }
        .auto-style2 {
            width: 189px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style1">Solicitudes de mi Empresa</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">Filtro Ultimo mes</td>
            <td class="auto-style1">
                <asp:Button ID="btnMes" runat="server" OnClick="btnMes_Click" Text="Último mes" />
            </td>
            <td>
                <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar Filtro" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Filtro mes/año</td>
            <td class="auto-style1">
                <asp:Button ID="btnMesAño" runat="server" OnClick="btnMesAño_Click" Text="Solicitudes /mes-año" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">Filtro por fecha</td>
            <td class="auto-style1">
                <asp:Calendar ID="cldFecha" runat="server" OnSelectionChanged="cldFecha_SelectionChanged"></asp:Calendar>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style1">
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style1">
                <asp:GridView ID="grvSolEmpresa" runat="server">
                </asp:GridView>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style1">
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style1">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

