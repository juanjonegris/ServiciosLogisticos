--Create Procedure UsuariosEmpresaAlta @NombreUsuario varchar(50), @Contrasenia varchar(6), @Nombre varchar(70), @Telefono varchar(9), @Direccion varchar(50), @Email varchar(50)  As
use Entregas 

-- alta empleado
DECLARE @r int
EXEC @r = UsuariosEmpresaAlta 'JuanjoPrueba', 'abg23!', 'Juan Jose Negris', '098978987', 'Famailla 3293', 'juanjoprueba@gmail.com'
SELECT @r 'Retorno', 
	CASE @r
	WHEN 1 THEN 'Se ha creado con exito'
	WHEN -1 THEN 'Ya existe ese usuario'
	WHEN -2 THEN 'Ha ocurrido un problema'	
END 'Mensaje'
GO

Create Procedure UsuariosEmpresaBaja @NombreUsuario varchar(50) Asselect * from Usua

select * from UsuariosEmpresa where NombreUsuario = 'Serrana Prueba'

DECLARE @r int
EXEC @r = UsuariosEmpresaBaja 'Serrana Prueba'
SELECT @r 'Retorno', 
	CASE @r
	WHEN 1 THEN 'Se ha eliminado con exito'
	WHEN -1 THEN 'No existe ese usuario'
	WHEN -2 THEN 'Ha ocurrido un problema'	
END 'Mensaje'
GO

DECLARE @r int
EXEC @r = CambioContrasena 'XimenaPrueba', 'add95..','hgd95.'
--EXEC CambioContrasena 'XimenaPrueba', 'abc95.'

SELECT @r 'Retorno', 
	CASE @r
	WHEN -1 THEN 'No existe ese usuario sql'
	WHEN -2 THEN 'No existe ese usuario'
	WHEN 0 THEN 'Cambiado'	
END 'Mensaje'
GO

select * from Usuarios


Create Procedure UsuariosEmpresaModificar @NombreUsuario varchar(50), @Contrasenia varchar(6), @Nombre varchar(70), @Telefono varchar(9), @Direccion varchar(50), @Email varchar(50)  As

DECLARE @r int
EXEC @r = UsuariosEmpresaModificar 'Serrana Prueba', 'aod99!', 'Serrrana Merlo', '098871058', 'Tucuman 1785', 'serrimerlo@gmail.com'
SELECT @r 'Retorno', 
	CASE @r
	WHEN 1 THEN 'Se ha actualizado con exito'
	WHEN -1 THEN 'No existe ese usuario'
	WHEN -2 THEN 'Ha ocurrido un problema'	
END 'Mensaje'
GO

Create Procedure UsuariosEmpresaBuscar @NombreUsuario varchar(50) AS 

EXEC UsuariosEmpresaBuscarTodos 'Serrana Prueba'

------------


Create Procedure UsuariosEmpleadoAlta @NombreUsuario varchar(50), @Contrasenia varchar(6), @Nombre varchar(70), @HoraInicio time, @HoraFin time As 
 select * from UsuariosEmpleado


DECLARE @r int
EXEC @r = UsuariosEmpleadoAlta 'Monica Prueba' , 'aft99@', 'Monica Perco', '08:00', '18:00'
SELECT @r 'Retorno', 
	CASE @r
	WHEN 1 THEN 'Se ha creado con exito'
	WHEN -1 THEN 'Ya existe ese usuario'
	WHEN -2 THEN 'Ha ocurrido un problema'	
END 'Mensaje'
GO

Create Procedure UsuariosEmpleadoBaja @NombreUsuario varchar(50) As 


DECLARE @r int
EXEC @r = UsuariosEmpleadoBaja 'Monica Prueba'
SELECT @r 'Retorno', 
	CASE @r
	WHEN 1 THEN 'Se ha eliminado con exito'
	WHEN -1 THEN 'No existe ese usuario'
	WHEN -2 THEN 'Ha ocurrido un problema'	
END 'Mensaje'
GO


Create Procedure UsuariosEmpleadoModificar  @NombreUsuario varchar(50), @Contrasenia varchar(6), @Nombre varchar(70), @HoraInicio time, @HoraFin time As 


DECLARE @r int
EXEC @r = UsuariosEmpleadoModificar 'Monica Prueba' , 'aft99@', 'Monica Perco', '10:00', '18:00'
SELECT @r 'Retorno', 
	CASE @r
	WHEN 1 THEN 'Se ha actualizado con exito'
	WHEN -1 THEN 'No existe ese usuario'
	WHEN -2 THEN 'Ha ocurrido un problema'	
END 'Mensaje'
GO

Create Procedure UsuariosEmpleadoBuscar @NombreUsuario varchar(50) AS

EXEC UsuariosEmpleadoBuscarTodos 'Monica Prueba' 

Create Procedure UsuariosLogueo @NombreUsuario varchar(50), @Contrasenia varchar(6) AS  

EXEC UsuariosLogueo 'Monica Prueba', 'aft99@'


Create Procedure SolicitudesDeEntregaAlta @FechaDeEntrega date, @NombreDestinatario varchar(50), @DireccionDestinatario varchar(50), @NombreUsuarioEmpleado varchar(50)  As 
DECLARE @r int
EXEC @r = SolicitudesDeEntregaAlta '2021-05-07' , 'Pepito', 'Ciganda 546', 'Monica Prueba'
SELECT @r 'Retorno' 




Create Procedure SolicitudesDeEntregaModificarEstado  @NumeroInterno int AS

DECLARE @r int
EXEC @r = SolicitudesDeEntregaModificarEstado 16
SELECT @r 'Retorno', 
	CASE @r
	WHEN 1 THEN 'Se ha actualizado con exito'
	WHEN -1 THEN 'No existe la solicitud'
	WHEN -2 THEN 'Ha ocurrido un problema'	
END 'Mensaje'
GO


select * from SolicitudesDeEntrega 

Create Procedure SolicitudesDeEntregaBuscar @NumeroInterno int AS 

EXEC  SolicitudesDeEntregaBuscar 16

Create Procedure SolicitudesDeEntregaListar AS

EXEC  SolicitudesDeEntregaListar 




Create Procedure SolicitudesDeEntregaListarPorEmpresa @NombreUsuario int AS

EXEC  SolicitudesDeEntregaListarPorEmpresa 'James Prueba'

Select * from UsuariosEmpresa



Create Procedure PaquetesAlta @CodigodeBarras int, @Tipo varchar(10), @Descripcion varchar(Max), @Peso decimal, @NombreUsuarioEmpresa varchar(50) As

DECLARE @r int
EXEC @r = SolicitudesDeEntregaModificarEstado 16
SELECT @r 'Retorno', 
	CASE @r
	WHEN 1 THEN 'Se ha creado con exito'
	WHEN -1 THEN 'Ya existe el paquete'
	WHEN -2 THEN 'No existe elusuario asignado al paquete'	
	WHEN -2 THEN 'Ha habido un error al ingresar el paquete'	
END 'Mensaje'
GO

Create Procedure PaqueteBuscar @CodigodeBarras int AS 

EXEC PaqueteBuscar 18744 

EXEC PaquetesListadoSinSolicitud


Create Procedure PaquetesListadoPorSolicitud @NumeroInterno int As

select * from SolicitudesDeEntrega


EXEC PaquetesListadoPorSolicitud 8

 Create Procedure SolicitudPaqueteAlta @CodigodeBarras int, @NumeroInterno int As

EXEC PaquetesListadoPorSolicitud 8 


Create Procedure SolicitudPaqueteAlta @CodigodeBarras int, @NumeroInterno int As

DECLARE @r int
EXEC @r = SolicitudPaqueteAlta 85471, 16
SELECT @r 'Retorno', 
	CASE @r
	WHEN 1 THEN 'Se ha creado con exito'
	WHEN -1 THEN 'No existe la solicitud'
	WHEN -2 THEN 'No existe el paquete'	
	WHEN -2 THEN 'YA se ha asignado el paquete'	
END 'Mensaje'
GO



---------------------

UsuariosEmpresaAlta