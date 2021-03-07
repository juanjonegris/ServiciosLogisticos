<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpleado.master" AutoEventWireup="true" CodeFile="AltaSolicituddeEntrega.aspx.cs" Inherits="AltaSolicituddeEntrega" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 358px;
        }
        .auto-style2 {
            height: 23px;
        }
        .auto-style3 {
            width: 358px;
            height: 23px;
        }
    </style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style1 h3">Ingreso de Solicitudes de Entrega</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style1">Agregar paquetes: </td>
            <td>Paquetes asignados</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style1">
                <asp:GridView ID="grvPaquetes" runat="server" OnSelectedIndexChanged="grvPaquetes_SelectedIndexChanged" AutoGenerateColumns="False">
                    <Columns>
                        <asp:CommandField HeaderText="Selección" SelectText="Agregar" ShowSelectButton="True" />
                        <asp:BoundField DataField="CodigoBarras" HeaderText="Codigo de Barras" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="Peso" HeaderText="Peso" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo de Paquete" />
                    </Columns>
                </asp:GridView>
            </td>
            <td>
                <asp:GridView ID="grvAsignados" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="CodigoBarras" HeaderText="Código de Barras" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="Peso" HeaderText="Peso" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo de paquete" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style1">Nombre del destinatario&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 33px"></td>
            <td class="auto-style1" style="height: 33px">Direccion de entrega&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
            </td>
            <td style="height: 33px">
                </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style1">Fecha de entrega&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtFecha" runat="server" TextMode="Date"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2"></td>
            <td class="auto-style3"></td>
            <td class="auto-style2"></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style1">
                <asp:Button ID="btnAgregar" class="btn btn-success" runat="server" OnClick="btnAgregar_Click" Text="Agregar Solicitud" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style1">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style1">
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

