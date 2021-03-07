<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />

    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 311px;
        }

        .auto-style2 {
            width: 368px;
        }
        </style>
</head>
<body>
    <div class="container-fluid">
    <form id="form1" runat="server" class="col-12">
        
            <div class="row mt-5 align-items-center">
                <div class="col align-items-center"><h3 class="text-left">Solicitudes de Entrega <small>En Camino</small></h3></div>
            </div>
            <div class="row mt-2 mb-5 align-items-left">
                <div class="col text-left">
                    <asp:HyperLink runat="server" class="btn btn-outline-success" NavigateUrl="~/Logueo.aspx">Iniciar sesión</asp:HyperLink>
                </div>
            </div>
            <div class="row">
                 <div class="auto-style1 h4 mb-3">Filtrar por fecha</div>
                 <div class="auto-style2">
                     <div class="row mb-2"><span>Año: </span> 
                    <asp:TextBox ID="txtFilAnio" runat="server" Width="72px"></asp:TextBox><asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Ingrese año válido - 4 números" ControlToValidate="txtFilAnio" Display="Dynamic" ForeColor="Red" MaximumValue="9999" MinimumValue="0000" SetFocusOnError="True" Type="Integer" ValidationGroup="fecha"></asp:RangeValidator> </div>
                     <div class="row mb-2"><span>Mes:</span>
                    <asp:TextBox ID="txtFilMes" runat="server" Width="52px"></asp:TextBox><asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Ingrese mes válido - 01 a 12" ControlToValidate="txtFilMes" Display="Dynamic" ForeColor="Red" MaximumValue="12" MinimumValue="01" SetFocusOnError="True" Type="Integer" ValidationGroup="fecha"></asp:RangeValidator></div>                           
                     <div class="row mb-2"><span>Día:</span> 
                    <asp:TextBox ID="txtFilDia" runat="server" Width="57px"></asp:TextBox><asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Ingrese dia válido - 01 a 31" ControlToValidate="txtFilDia" Display="Dynamic" ForeColor="Red" MaximumValue="31" MinimumValue="01" SetFocusOnError="True" Type="Integer" ValidationGroup="fecha"></asp:RangeValidator></div>
                    </div>
                </div>
                <div class="row my-5">
                    <div>
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" class="btn btn-outline-success mx-2" ValidationGroup="fecha"/>
                    </div>
                    <div>
                        <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar Filtro" OnClick="btnLimpiarFiltro_Click" class="btn btn-outline-success"/>
                    </div>
                </div>
                <div class="row my-5">
                    <div class="col-6">     
                        <asp:GridView ID="grvSolicitudes" runat="server" OnSelectedIndexChanged="grvSolicitudes_SelectedIndexChanged" AutoGenerateColumns="False">
                            <Columns>
                                <asp:CommandField HeaderText="Listar Paquetes" SelectText="Ver" ShowSelectButton="True" />
                                <asp:BoundField DataField="NumeroInterno" HeaderText="Número" />
                                <asp:BoundField DataField="FechaEntrega" HeaderText="Fecha de Entrega" />
                                <asp:BoundField DataField="NombreDestinatario" HeaderText="Destinatario" />
                                <asp:BoundField DataField="DireccionDestinatario" HeaderText="Dirección" />
                            </Columns>
                         </asp:GridView> 
                    </div>

                    <div class="col-md-6">
                        <asp:Xml ID="XmlPaquetes" runat="server" TransformSource="~/Paquetes.xslt"></asp:Xml>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </div>
                </div>
            </form>
        </div>
</body>
</html>
