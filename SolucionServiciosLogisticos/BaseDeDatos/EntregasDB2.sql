USE master
go


------ Creacion de una base de datos -------------------------------------------------------------------------

IF exists(SELECT * FROM SysDataBases WHERE name='Entregas')
BEGIN
	DROP DATABASE Entregas
END
go

CREATE DATABASE Entregas
go

--creacion de usuarios

USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS 
GO

USE Entregas
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

GRANT Execute to [IIS APPPOOL\DefaultAppPool]
go

--creacion de tablas

USE Entregas
go

CREATE TABLE Usuarios (
	NombreUsuario varchar(50) not null Primary Key, 
	Contrasenia varchar(6) not null check(len(Contrasenia ) = 6 AND Contrasenia like '[a-zA-Z][a-zA-Z][a-zA-Z][0-9][0-9][$&+,:;=?@#|''<>.^*()%!-]'), 
	Nombre varchar(70) not null,
	Activo bit not null Default (1)
)
go


CREATE TABLE UsuariosEmpresa (
	NombreUsuario varchar(50) not null,
	Telefono varchar(9) not null,
	Direccion varchar(50) not null,
	Email varchar(50) not null,
	PRIMARY KEY (NombreUsuario),
	FOREIGN KEY (NombreUsuario) REFERENCES Usuarios(NombreUsuario)
)
go 


CREATE TABLE UsuariosEmpleado (
	NombreUsuario varchar(50) not null,
	HoraInicio time not null,
	HoraFin time not null, 
	PRIMARY KEY (NombreUsuario),
	FOREIGN KEY (NombreUsuario) REFERENCES Usuarios(NombreUsuario),
	check (HoraFin > HoraInicio)
)
go


CREATE TABLE SolicitudesDeEntrega (
	NumeroInterno int not null Identity (1,1) Primary Key,
	FechaDeEntrega date not null check (FechaDeEntrega >= GETDATE()),
	NombreDestinatario varchar(50) not null,
	DireccionDestinatario varchar(50) not null,
	EstadoSolicitud varchar(20) not null check( EstadoSolicitud = 'En Depósito' OR EstadoSolicitud = 'En Camino' OR EstadoSolicitud = 'Entregado' ) Default ('En Depósito'),
	NombreUsuarioEmpleado varchar(50) not null,
	FOREIGN KEY (NombreUsuarioEmpleado) REFERENCES UsuariosEmpleado(NombreUsuario)
)
go


CREATE TABLE Paquetes (
	CodigodeBarras int not null Primary Key,
	Tipo varchar(10) not null check( Tipo = 'fragil' OR Tipo = 'común' OR tipo = 'bulto'),
	Descripcion varchar(MAX) not null,
	Peso decimal not null,
	NombreUsuarioEmpresa varchar(50) not null,
	FOREIGN KEY (NombreUsuarioEmpresa) REFERENCES UsuariosEmpresa(NombreUsuario)
)
go 

CREATE TABLE SolicitudPaquete (
	CodigodeBarras int not null,
	NumeroInterno int not null,
	PRIMARY KEY (CodigodeBarras, NumeroInterno),
	FOREIGN KEY (CodigodeBarras) REFERENCES Paquetes(CodigodeBarras),
	FOREIGN KEY (NumeroInterno) REFERENCES SolicitudesDeEntrega(NumeroInterno),
)
go 

------------

/*
Insert into Usuarios (NombreUsuario, Contrasenia, Nombre ) values ('jaum12', 'abc45!', 'Pepe parada'); 
Insert into UsuariosEmpresa values ('jaum12', 091645998, 'Miguelete 1972', 'juanjose.negris@gmail.com');
Insert into UsuariosEmpleado values ('jaum12', '08:00', '10:00' );  
Insert into SolicitudesDeEntrega (FechaDeEntrega, NombreDestinatario, DireccionDestinatario, NombreUsuarioEmpleado ) values ( dateadd(dd,2,GETDATE()), 'Anibal Balsco', 'Goes 2256', 'jaum12');  
Insert into Paquetes  values ( 12345, 'fragil', 'Piscina de 80 litros', 20.5, 'jaum12');  
Insert into SolicitudPaquete values (12345, 2 ); 
*/

-- Select * from SolicitudPaquete

---------- Usuarios de SQL -------------------------

Create Procedure NuevoUsuarioSql @NombreUsuario varchar(50), @Contrasenia varchar (6) AS
Begin

	Declare @VarSentencia varchar(200)
	
		--Primero creo el usuario de logueo
			
		Set @VarSentencia = 'CREATE LOGIN ['+ @NombreUsuario + '] WITH PASSWORD = ' + QUOTENAME(@Contrasenia,'''')
		Exec (@VarSentencia)

		If (@@ERROR = 0)
		return 1
		else 
		return -1
			
End
go

Create Procedure NuevoUsuarioBD @NombreUsuario varchar(50)  AS
Begin
		--primero creo el usuario
		Declare @VarSentencia varchar (200)
		Set @VarSentencia = 'CREATE USER ['+ @NombreUsuario + '] FROM LOGIN [' + @NombreUsuario + ']'
		Exec (@VarSentencia)

		If (@@ERROR = 0)
		return 1
		else 
		return -1	

End
go


Create Procedure AgregarPermisoUsuarioBD @NombreUsuario varchar(50) AS
Begin
	
	Declare @VarSentencia varchar (200)

		Set @VarSentencia = 'GRANT Execute TO '+ @NombreUsuario

		Exec (@VarSentencia)

		If (@@ERROR = 0)
		return 1
		else 
		return -1
End
go

---------- UsuariosEmpresa -------------------------


Create Procedure UsuariosEmpresaAlta @NombreUsuario varchar(50), @Contrasenia varchar(6), @Nombre varchar(70), @Telefono varchar(9), @Direccion varchar(50), @Email varchar(50)  As
Begin
	if (Exists (Select * from Usuarios Where NombreUsuario = @NombreUsuario AND Activo = 1)) 
		return -1

	if (Exists (Select * from Usuarios Where NombreUsuario = @NombreUsuario AND Activo = 0))
		Begin Try
			Begin Tran
				Update Usuarios Set Nombre = @Nombre, Activo = 1  Where NombreUsuario = @NombreUsuario
				Update UsuariosEmpresa Set Telefono = @Telefono, Direccion = @Direccion, Email = @Email  Where NombreUsuario = @NombreUsuario
			Commit Tran
			Return 1
			End Try
 			Begin Catch
				Rollback Tran
				Return -2
		End Catch	
	Else
		Begin Try
			Begin Tran
				Insert into Usuarios (NombreUsuario, Contrasenia, Nombre) values (@NombreUsuario,  @Contrasenia, @Nombre)
				Declare @IdUsuario as varchar(50)
				Set @IdUsuario = (SELECT @@IDENTITY)
				Insert into UsuariosEmpresa (NombreUsuario, Telefono, Direccion, Email) values (@IdUsuario, @Telefono, @Direccion, @Email)
			Commit Tran 
			Return 1
		End Try
 		Begin Catch
			Rollback Tran
			Return -3
		End Catch
End
go


Create Procedure UsuariosEmpresaBaja @NombreUsuario varchar(50) As
Begin
	if (Not Exists(Select * From UsuariosEmpresa Where NombreUsuario = @NombreUsuario))
		return -1
	
	if (Exists(Select * From Paquetes Where NombreUsuarioEmpresa = @NombreUsuario))
		Begin
			Update Usuarios Set Activo = 0 Where NombreUsuario = @NombreUsuario 
			return 1
		End
	Else
		Begin try
			Begin Tran
				Delete Usuarios Where NombreUsuario = @NombreUsuario
				Delete UsuariosEmpresa Where NombreUsuario = @NombreUsuario
			Commit Tran 
			Return 1
		End Try
 		Begin Catch
			Rollback Tran
			Return -2
		End Catch
End
go 

Create Procedure UsuariosEmpresaModificar @NombreUsuario varchar(50), @Contrasenia varchar(6), @Nombre varchar(70), @Telefono varchar(9), @Direccion varchar(50), @Email varchar(50)  As
Begin
	if (Not Exists (Select * from Usuarios Where NombreUsuario = @NombreUsuario AND Activo = 1)) 
		return -1
	else
		Begin try
			Begin Tran
				Update Usuarios Set Nombre = @Nombre  Where NombreUsuario = @NombreUsuario
				Update UsuariosEmpresa Set Telefono = @Telefono, Direccion = @Direccion, Email = @Email  Where NombreUsuario = @NombreUsuario
			Commit Tran
			return 1 
			End try
		Begin Catch
			Rollback Tran
			Return -2
		End Catch
End
Go


Create Procedure UsuariosEmpresaBuscar @NombreUsuario varchar(50) AS 
Begin 
 Select * from Usuarios u inner join UsuariosEmpresa ue on u.NombreUsuario = ue.NombreUsuario where u.NombreUsuario = @NombreUsuario AND u.Activo = 1
End
Go

Create Procedure UsuariosEmpresaBuscarTodos @NombreUsuario varchar(50) AS 
Begin 
 Select * from Usuarios u inner join UsuariosEmpresa ue on u.NombreUsuario = ue.NombreUsuario where u.NombreUsuario = @NombreUsuario
End
Go


---------- UsuariosEmpleado -------------------------

Create Procedure UsuariosEmpleadoAlta @NombreUsuario varchar(50), @Contrasenia varchar(6), @Nombre varchar(70), @HoraInicio time, @HoraFin time As
Begin
	if (Exists (Select * from Usuarios Where NombreUsuario = @NombreUsuario AND Activo = 1)) 
		return -1

	if (Exists (Select * from Usuarios Where NombreUsuario = @NombreUsuario AND Activo = 0))
		Begin Try
			Begin Tran
				Update Usuarios Set Nombre = @Nombre, Activo = 1  Where NombreUsuario = @NombreUsuario
				Update UsuariosEmpleado Set HoraInicio = @HoraInicio, HoraFin = @HoraFin Where NombreUsuario = @NombreUsuario
			Commit Tran
			Return 1
			End Try
 			Begin Catch
				Rollback Tran
				Return -2
		End Catch	
	Else
		Begin Try
			Begin Tran
				Insert into Usuarios (NombreUsuario, Contrasenia, Nombre) values (@NombreUsuario,  @Contrasenia, @Nombre)
				Declare @IdUsuario as varchar(50)
				Set @IdUsuario = (SELECT @@IDENTITY)
				Insert into UsuariosEmpleado (NombreUsuario, HoraInicio, HoraFin) values (@IdUsuario, @HoraInicio, @HoraFin)
			Commit Tran 
			Return 1
		End Try
 		Begin Catch
			Rollback Tran
			Return -3
		End Catch
End
go


Create Procedure UsuariosEmpleadoBaja @NombreUsuario varchar(50) As
Begin
	if (Not Exists(Select * From UsuariosEmpleado Where NombreUsuario = @NombreUsuario))
		return -1
	
	if (Exists(Select * From SolicitudesDeEntrega Where NombreUsuarioEmpleado = @NombreUsuario))
		Begin
			Update Usuarios Set Activo = 0 Where NombreUsuario = @NombreUsuario 
			return 1
		End
	Else
		Begin try
			Begin Tran
				Delete Usuarios Where NombreUsuario = @NombreUsuario
				Delete UsuariosEmpleado Where NombreUsuario = @NombreUsuario
			Commit Tran 
			Return 1
		End Try
 		Begin Catch
			Rollback Tran
			Return -2
		End Catch
End
go 

Create Procedure UsuariosEmpleadoModificar  @NombreUsuario varchar(50), @Contrasenia varchar(6), @Nombre varchar(70), @HoraInicio time, @HoraFin time As
Begin
	if (Not Exists (Select * from Usuarios Where NombreUsuario = @NombreUsuario AND Activo = 1)) 
		return -1
	else
		Begin try
			Begin Tran
				Update Usuarios Set Nombre = @Nombre  Where NombreUsuario = @NombreUsuario
				Update UsuariosEmpleado Set HoraInicio = @HoraInicio, HoraFin = @HoraFin  Where NombreUsuario = @NombreUsuario
			Commit Tran
			return 1 
			End try
		Begin Catch
			Rollback Tran
			Return -2
		End Catch
End
Go

Create Procedure UsuariosEmpleadoBuscar @NombreUsuario varchar(50) AS 
Begin 
 Select * from Usuarios u inner join UsuariosEmpleado ue on u.NombreUsuario = ue.NombreUsuario where u.NombreUsuario = @NombreUsuario AND u.Activo = 1
End
Go

Create Procedure UsuariosEmpleadoBuscarTodos @NombreUsuario varchar(50) AS 
Begin 
 Select * from Usuarios u inner join UsuariosEmpleado ue on u.NombreUsuario = ue.NombreUsuario where u.NombreUsuario = @NombreUsuario 
End
Go

----- Usuarios ----

Create Procedure UsuariosLogueo @NombreUsuario varchar(50), @Contrasenia varchar(6) AS 
Begin 
 Select * from Usuarios where NombreUsuario = @NombreUsuario AND  Contrasenia = @Contrasenia
End
Go


--------- SolicitudesDeEntrega ------------

Create Procedure SolicitudesDeEntregaAlta @FechaDeEntrega date, @NombreDestinatario varchar(50), @DireccionDestinatario varchar(50), @NombreUsuarioEmpleado varchar(50)  As
Begin
if (Not Exists(Select * From UsuariosEmpleado Where NombreUsuario = @NombreUsuarioEmpleado )) 
	Begin
		return -1
	End
Insert into SolicitudesDeEntrega (FechaDeEntrega, NombreDestinatario, DireccionDestinatario, NombreUsuarioEmpleado) values (@FechaDeEntrega, @NombreDestinatario, @DireccionDestinatario, @NombreUsuarioEmpleado )
	return 1
End
go

Create Procedure SolicitudesDeEntregaModificarEstado @EstadoSolicitud varchar(20), @NumeroInterno int AS
Begin
	If (Not Exists (Select * From SolicitudesDeEntrega Where NumeroInterno = @NumeroInterno))
	return -1 

	Declare @es as varchar(20)
	set @es = (Select EstadoSolicitud FROM SolicitudesDeEntrega Where NumeroInterno = @NumeroInterno)
	if @es = 'En Depósito'
	Begin
		Update SolicitudesDeEntrega Set EstadoSolicitud = 'En Camino' Where NumeroInterno = @NumeroInterno
	End 

	if @es = 'En Camino'
	Begin
		Update SolicitudesDeEntrega Set EstadoSolicitud = 'Entregado' Where NumeroInterno = @NumeroInterno
	End 

	if @@ERROR <> 0 
		return -2
	else
		return 1
End
Go

Create Procedure SolicitudesDeEntregaListarEnCamino AS
	Select * From SolicitudesDeEntrega Where EstadoSolicitud = 'En Camino'
Go



Create Procedure SolicitudesDeEntregaListarPorEmpresa @NombreUsuario int AS
Begin
	 Select * From SolicitudesDeEntrega as se INNER JOIN SolicitudPaquete as sp on se.NumeroInterno = sp.NumeroInterno 
	 INNER JOIN Paquetes as p on sp.CodigodeBarras = p.CodigodeBarras 
	 Where p.NombreUsuarioEmpresa = @NombreUsuario
End
go


--------- Paquetes ------------

Create Procedure PaquetesAlta @CodigodeBarras int, @Tipo varchar(10), @Descripcion varchar(Max), @Peso decimal, @NombreUsuarioEmpresa varchar(50) As
Begin
	if (Not Exists (Select * From UsuariosEmpresa Where NombreUsuario = @NombreUsuarioEmpresa))
	Begin
		return -1
	End 

	Insert into Paquetes  values (@CodigodeBarras, @Tipo, @Descripcion, @Peso, @NombreUsuarioEmpresa )
	return 1
End
Go

Create Procedure PaquetesListadoSinSolicitud As
Begin
	Select * from Paquetes as p Where p.CodigodeBarras NOT IN ( Select CodigodeBarras From SolicitudPaquete ) 
End
Go


Create Procedure PaquetesListadoPorSolicitud @NumeroInterno int As
Begin
	Select * From Paquetes as p INNER JOIN SolicitudPaquete as sp ON p.CodigodeBarras = sp.CodigodeBarras Where sp.NumeroInterno = @NumeroInterno
End
Go


--------- SolicitudPaquete ------------

Create Procedure SolicitudPaqueteAlta @CodigodeBarras int, @NumeroInterno int As
Begin
	if (Not Exists (Select * From SolicitudesDeEntrega Where NumeroInterno = @NumeroInterno))
		return -1

	if (Not Exists (Select * From Paquetes Where CodigodeBarras = @CodigodeBarras))
		return -2

	if ( Exists (Select * From SolicitudPaquete Where CodigodeBarras = @CodigodeBarras))
		return -3

	Insert into SolicitudPaquete values (@CodigodeBarras, @NumeroInterno )
	return 1
End
Go



