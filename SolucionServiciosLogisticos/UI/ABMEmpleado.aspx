<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpleado.master" AutoEventWireup="true" CodeFile="ABMEmpleado.aspx.cs" Inherits="ABMEmpleado" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 199px;
        }
        .auto-style2 {
            width: 237px;
        }
        .auto-style3 {
            width: 199px;
            height: 30px;
        }
        .auto-style4 {
            width: 237px;
            height: 30px;
        }
        .auto-style5 {
            height: 30px;
        }
    </style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style1" style="width: 160px">&nbsp;</td>
            <td class="auto-style2 h3" style="width: 339px">Gestión de Empleados</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="width: 160px">Nombre de Usuario</td>
            <td class="auto-style2" style="width: 339px">
                <asp:TextBox ID="txtNomUsu" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
            </td>
        </tr>
        <tr>
            <td class="auto-style1" style="width: 160px">Contraseña</td>
            <td class="auto-style2" style="width: 339px">
                <asp:TextBox ID="txtContra" runat="server" Enabled="False" Type="password"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="width: 160px">Nombre</td>
            <td class="auto-style2" style="width: 339px">
                <asp:TextBox ID="txtNombre" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar" Width="64px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style3" style="width: 160px">Hora Inicio</td>
            <td class="auto-style4" style="width: 339px">
                <asp:TextBox ID="txtHoraInicio" runat="server" Enabled="False" Width="59px" ValidationGroup="hora"></asp:TextBox>
&nbsp;:
                <asp:TextBox ID="txtMinutoIncio" runat="server" Enabled="False" Width="59px" ValidationGroup="hora"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtHoraInicio" Display="Dynamic" ErrorMessage="Ingrese un número de horas de 00 a 23" MaximumValue="23" MinimumValue="00" ValidationGroup="horarios"></asp:RangeValidator>
&nbsp; -&nbsp;
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtMinutoIncio" Display="Dynamic" ErrorMessage="Ingrses minutos de 00 a 59 " MaximumValue="59" MinimumValue="00"></asp:RangeValidator>
            </td>
            <td class="auto-style5"></td>
        </tr>
        <tr>
            <td class="auto-style3" style="width: 160px">Hora Fin</td>
            <td class="auto-style4" style="width: 339px">
                <asp:TextBox ID="txtHoraFin" runat="server" Enabled="False" Width="59px" ValidationGroup="hora"></asp:TextBox>
&nbsp;:
                <asp:TextBox ID="txtMinutoFin" runat="server" Enabled="False" Width="59px" ValidationGroup="hora"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtHoraFin" Display="Dynamic" ErrorMessage="Ingrese un número de horas de 00 a 23" MaximumValue="23" MinimumValue="00"></asp:RangeValidator>
&nbsp;-
                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtMinutoFin" Display="Dynamic" ErrorMessage="Ingrses minutos de 00 a 59 " MaximumValue="59" MinimumValue="00"></asp:RangeValidator>
            </td>
            <td class="auto-style5">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="width: 160px">
                &nbsp;</td>
            <td class="auto-style2" style="width: 339px">
                <asp:Button ID="btnAlta" runat="server" Enabled="False" Text="Ingresar" Width="78px" OnClick="btnAlta_Click" ValidationGroup="hora" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBaja" runat="server" Enabled="False" Text="Eliminar" OnClick="btnBaja_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnModificacion" runat="server" Enabled="False" Text="Modificar" OnClick="btnModificacion_Click" ValidationGroup="hora" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="width: 160px">&nbsp;</td>
            <td class="auto-style2" style="width: 339px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="width: 160px">&nbsp;</td>
            <td class="auto-style2" style="width: 339px">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

