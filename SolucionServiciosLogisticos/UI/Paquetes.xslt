<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    
>
  <!--xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"-->
  <xsl:template match="/">

    <table>
        <xsl:for-each select="SolicitudEntrega/Paquete">
          <tr>
            <td style="background-color:#008800;padding:4px;font-size:15pt;
                                  font-weight:bold;color:white">
              Codigo de barras: <xsl:value-of select="CodigoBarras"/>
            </td>
            <td style="margin-left:20px;margin-bottom:1em;font-size:15pt">
              Tipo: <xsl:value-of select="Tipo"/>
            </td>
            <td style="margin-left:20px;margin-bottom:1em;font-size:15pt">
              Descripción: <xsl:value-of select="Descripcion"/>
            </td>
            <td style="margin-left:20px;margin-bottom:1em;font-size:15pt">
              Peso: <xsl:value-of select="Peso"/>
            </td>
            <td style="margin-left:20px;margin-bottom:1em;font-size:15pt">
              Nombre del Empleado: <xsl:value-of select="UsuarioEmpresa/Nombre"/>
            </td>
          </tr>
        </xsl:for-each>
    </table> 
  </xsl:template>
</xsl:stylesheet>
