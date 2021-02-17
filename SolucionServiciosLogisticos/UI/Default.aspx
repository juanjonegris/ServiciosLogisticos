﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 311px;
        }

        .auto-style2 {
            width: 368px;
        }
        .auto-style3 {
            width: 311px;
            height: 23px;
        }
        .auto-style4 {
            width: 368px;
            height: 23px;
        }
        .auto-style5 {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h3>Solicitudes de Entrega <small>En Camino&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </small>
                        <asp:HyperLink runat="server" NavigateUrl="~/Logueo.aspx">Iniciar sesión</asp:HyperLink>
                    </h3>

            <table style="width: 100%;">
                <tr>
                    <td class="auto-style1">Filtrar por fecha</td>
                    <td class="auto-style2">&nbsp;Año:&nbsp;
                    <asp:TextBox ID="txtFilAnio" runat="server" Width="72px"></asp:TextBox>
                        &nbsp;- Mes:&nbsp;
                    <asp:TextBox ID="txtFilMes" runat="server" Width="52px"></asp:TextBox>
                        &nbsp;Día:&nbsp;
                    <asp:TextBox ID="txtFilDia" runat="server" Width="57px"></asp:TextBox>
                        &nbsp;&nbsp; </td>
                    <td>
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style2">
                        <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar Filtro" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:GridView ID="grvSolicitudes" runat="server" OnSelectedIndexChanged="grvSolicitudes_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField HeaderText="Listar Paquetes" SelectText="Ver" ShowSelectButton="True" />
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Xml ID="XmlPaquetes" runat="server" TransformSource="~/Paquetes.xslt"></asp:Xml>
                    </td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style5"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
