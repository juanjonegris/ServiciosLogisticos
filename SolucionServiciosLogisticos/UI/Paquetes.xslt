<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    
>
  <!--xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"-->
  <xsl:template match="/">

    <table >
        <xsl:for-each select="SolicitudEntrega/Paquete">
          <tr style="border: 1px solid gray; padding-bottom: 15px; margin-bottom:30px;">
            <td style="background-color:red;padding:4px;font-size:12pt; color:white">
              Codigo de barras: <xsl:value-of select="CodigoBarras"/>
            </td>
          </tr>
          <tr>
            <td style="margin-left:20px;margin-bottom:1em;font-size:12pt;">
              <b> Tipo: </b> <xsl:value-of select="Tipo"/>
            </td>
          </tr>
          <tr>
            <td style="margin-left:20px;margin-bottom:1em;font-size:12pt;">
              <b>Descripción: </b>
              <xsl:value-of select="Descripcion"/>
            </td>
          </tr>
          <tr>
            <td style="margin-left:20px;margin-bottom:1em;font-size:12pt;">
              <b>Peso: </b> <xsl:value-of select="Peso"/>
            </td>
            </tr>
          <tr>
            <td style="margin-left:20px;margin-bottom:1em;font-size:12pt;">
              <b>Nombre del Empleado: </b> <xsl:value-of select="UsuarioEmpresa/Nombre"/>
            </td>
          </tr>
        </xsl:for-each>
    </table> 
  </xsl:template>
</xsl:stylesheet>
