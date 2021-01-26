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

-------------------------- Insert Data --------------------------------


INSERT Usuarios (NombreUsuario, Contrasenia, Nombre) VALUES ('Juanjo Prueba', 'abg23!', 'Juan Jose Negris'),
	('Mario Prueba', 'acb23@', 'Mario Puzo'),('James Prueba', 'adf23:', 'James Batdrock'),
	('Ximena Prueba', 'afs24,', 'Ximena Smith'),('Boris Prueba', 'atr23!', 'Boris Hendrson')
Go

INSERT UsuariosEmpresa (NombreUsuario, Telefono, Direccion, Email) VALUES ('Juanjo Prueba', '098978987', 'Famailla 3293', 'juanjoprueba@gmail.com'),
	('Mario Prueba', '098996654', '18 de Julio 1987', 'marioprueba@gmail.com'), ('James Prueba', '091971258', 'Elm Road 1875', 'jamesprueba@gmail.com')
Go	

INSERT UsuariosEmpleado (NombreUsuario, HoraInicio, HoraFin) VALUES ('Ximena Prueba', '09:00', '17:00'),
	('Boris Prueba', '10:00', '18:00')
Go

INSERT SolicitudesDeEntrega ( FechaDeEntrega, NombreDestinatario, DireccionDestinatario, NombreUsuarioEmpleado ) 
VALUES ( DATEADD(DAY, 105, GETDATE()), 'Ana Monterroso', 'Garibaldi 7941', 'Ximena Prueba'),
( DATEADD(DAY, 105, GETDATE()), 'JaimeRoss', 'Ansina 1987', 'Boris Prueba'),
( DATEADD(DAY, 98, GETDATE()), 'Peter Capusoto', '25 de mayo 789', 'Ximena Prueba'),
( DATEADD(DAY, 59, GETDATE()), 'Ingrid Lopez', 'Ciganda 871', 'Boris Prueba'),
( DATEADD(DAY, 67, GETDATE()), 'Sergio Dalmata', 'Miguelete 1972', 'Ximena Prueba'),
( DATEADD(DAY, 72, GETDATE()), 'Jaime Anibal Gizman', 'Enric el Xavi 687', 'Boris Prueba'),
( DATEADD(DAY, 120, GETDATE()), 'Vago President', 'Gianattasio km 25', 'Ximena Prueba'),
( DATEADD(DAY, 158, GETDATE()), 'Eulalia Sierra', '17 metros 789', 'Boris Prueba'),
( DATEADD(DAY, 78, GETDATE()), 'Kurtney Love', 'Gomensoro 1897', 'Ximena Prueba'),
( DATEADD(DAY, 85, GETDATE()), 'Salome Salomon', 'Trapani 7897', 'Boris Prueba'),
( DATEADD(DAY, 115, GETDATE()), 'Dario Bravo', 'Roy Mondle 5454', 'Ximena Prueba'),
( DATEADD(DAY, 100, GETDATE()), 'Eduardo Scott', '18 de Julio 8974', 'Boris Prueba'),
( DATEADD(DAY, 49, GETDATE()), 'Servando Pagliari', 'Montenegro 7941', 'Ximena Prueba'),
( DATEADD(DAY, 82, GETDATE()), 'Reina Rodriguez', 'Servia 1987', 'Boris Prueba'),
( DATEADD(DAY, 72, GETDATE()), 'Tomas Bartolomiu', 'Austria 1941', 'Ximena Prueba')
Go

--select * from Paquetes
go
INSERT Paquetes( CodigodeBarras, Tipo, Descripcion, Peso, NombreUsuarioEmpresa )  
VALUES ( 45454, 'fragil', 'Platos', 3200 , 'Juanjo Prueba'),
( 487878, 'común', 'Caja de zapatos', 800 , 'Juanjo Prueba'),
( 48788, 'fragil', 'IPad', 500 , 'Juanjo Prueba'),
( 1254, 'bulto', 'Caja comun', 3000 , 'James Prueba'),
( 8754, 'común', 'Mochila', 500 , 'Mario Prueba'),
( 98712, 'fragil', 'TV', 5000 , 'James Prueba'),
( 54458, 'bulto', 'Caja especial', 8500 , 'Juanjo Prueba'),
( 54545, 'fragil', 'Parabrisas', 16000 , 'Juanjo Prueba'),
( 885412, 'común', 'Caja comun', 3000 , 'Mario Prueba'),
( 85471, 'fragil', 'Parlantes', 2200 , 'Juanjo Prueba'),
( 66336, 'fragil', 'Auriculares', 300 , 'Juanjo Prueba'),
( 85214, 'común', 'Jeans', 800 , 'Juanjo Prueba'),
( 36255, 'fragil', 'Electrodomestico', 3200 , 'James Prueba'),
( 87456, 'bulto', 'Caja comun', 3000 , 'Juanjo Prueba'),
( 18744, 'común', 'Calzado', 1200 , 'Mario Prueba'),
( 84484, 'fragil', 'Lentes Caja', 2000 , 'James Prueba'),
( 78452, 'bulto', 'Caja grande', 6000 , 'Juanjo Prueba'),
( 23215, 'común', 'Sillas Plegables', 18000 , 'Juanjo Prueba'),
( 12121, 'fragil', 'Mesa vidrio', 20000 , 'Juanjo Prueba'),
( 22123, 'fragil', 'Heladera', 20500 , 'Juanjo Prueba'),
( 36952, 'común', 'Juguetes caja', 3800 , 'James Prueba'),
( 25321, 'fragil', 'Aticulos iluminacion', 6000 , 'Juanjo Prueba'),
( 84645, 'bulto', 'Caja pequeña', 1500 , 'Mario Prueba'),
( 231545, 'común', 'Mochila', 500 , 'Juanjo Prueba'),
( 96325, 'fragil', 'Espejo', 6500 , 'Juanjo Prueba'),
( 74145, 'bulto', 'Caja grande', 6000 , 'Juanjo Prueba'),
( 23123, 'fragil', 'Cristaleria', 3500 , 'Juanjo Prueba'),
( 84127, 'común', 'Remeras', 2000 , 'James Prueba'),
( 96365, 'fragil', 'Apple TV', 600 , 'Juanjo Prueba'),
( 35241, 'común', 'Championes bebe', 200 , 'Juanjo Prueba'),
( 85695, 'bulto', 'Caja pequeña', 1500 , 'Mario Prueba')
 


INSERT SolicitudPaquete (CodigodeBarras, NumeroInterno) VALUES 
(45454,1 ), (487878, 1), ( 48788, 1), ( 1254, 2), ( 8754, 2),( 98712, 2),( 54458, 3),( 54545, 3), 
(885412, 4), (66336, 4), (85214, 4),
(36255, 5), (87456, 5), (18744, 5),
(84484, 6), (78452, 6),
(23215, 7), (12121, 7), (22123, 7),
(36952, 8), (25321,8), (84645,8),
(231545, 9), (96325, 9), (74145, 9),
(23123, 10), (84127, 10), 
(96365, 11), (35241, 11), 
(85695, 12)

Go
---------- Usuarios de SQL -------------------------

------------ NEW DEV -----------------
CREATE PROCEDURE NuevoUsuario @NombreUsuario varchar(50), @Contrasenia varchar (6) AS
Begin

	If  Exists ( SELECT name FROM master.sys.server_principals WHERE name = @NombreUsuario )
	return -1

	Declare @VarSentencia varchar (200)

	-- Multiples acciones - TRN

		--primero creo el usuario de logueo
		SET @VarSentencia = 'CREATE LOGIN [' + @NombreUsuario + '] WITH PASSWORD = ' + QUOTENAME (@Contrasenia, '''')
		EXEC(@VarSentencia)

		if (@@ERROR <> 0 )
		begin 
			return -2
		end

		--segundo creo usuari db
		Set @VarSentencia = 'CREATE USER ['+@NombreUsuario + '] FROM LOGIN [' +@NombreUsuario + ']'
		exec (@VarSentencia)

		if (@@ERROR <> 0 )
		begin 
			return -3
		end

				--segundo creo usuari db con permisos
		Set @VarSentencia = 'GRANT EXECUTE TO [' +@NombreUsuario + ']'
		exec (@VarSentencia)

		if (@@ERROR <> 0 )
		BEGIN 
			return -4
		END

		Set @VarSentencia = 'GRANT ALTER ANY LOGIN TO [' +@NombreUsuario + ']'
		exec (@VarSentencia)

		if (@@ERROR <> 0 )
		BEGIN 
			return -5
		END

		return 1
 
END
GO


CREATE PROCEDURE CambioContraseña @NombreUsuario varchar(50), @Contrasenia varchar (6) AS
Begin
	
		If Not Exists ( SELECT name FROM master.sys.server_principals WHERE name = @NombreUsuario )
		return -1

		Declare @VarSentencia varchar (200)


			Set @VarSentencia = 'ALTER LOGIN ['+ @NombreUsuario + '] WITH PASSWORD = ' + QUOTENAME(@Contrasenia,'''')
			Exec (@VarSentencia)

			If (@@ERROR = 0)
			return -2
			
			
			if (Not Exists (Select * from Usuarios Where NombreUsuario = @NombreUsuario AND Activo = 1)) 
			return -3
			else
			
				Update Usuarios Set Contrasenia = @Contrasenia  Where NombreUsuario = @NombreUsuario
			
			If (@@ERROR = 0)
			return -3 


			return 1
			
End
Go

-------------------------------------

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
				Insert into UsuariosEmpresa (NombreUsuario, Telefono, Direccion, Email) values (@NombreUsuario, @Telefono, @Direccion, @Email)
			Commit Tran 
			Return 1
		End Try
 		Begin Catch
			Rollback Tran
			Return -2
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
				Delete UsuariosEmpresa Where NombreUsuario = @NombreUsuario
				Delete Usuarios Where NombreUsuario =  @NombreUsuario
			Commit Tran 
			Return 1
		End Try
 		Begin Catch
			Rollback Tran
			Return -2
		End Catch
End
Go 

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
 Select u.NombreUsuario, u.Contrasenia, u.Nombre, ue.Telefono, ue.Direccion, ue.Email  from Usuarios u inner join UsuariosEmpresa ue on u.NombreUsuario = ue.NombreUsuario where u.NombreUsuario = @NombreUsuario AND u.Activo = 1
End
Go

Create Procedure UsuariosEmpresaBuscarTodos @NombreUsuario varchar(50) AS 
Begin 
 Select u.NombreUsuario, u.Contrasenia, u.Nombre, ue.Telefono, ue.Direccion, ue.Email  from Usuarios u inner join UsuariosEmpresa ue on u.NombreUsuario = ue.NombreUsuario where u.NombreUsuario = @NombreUsuario
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
				Insert into UsuariosEmpleado (NombreUsuario, HoraInicio, HoraFin) values (@NombreUsuario, @HoraInicio, @HoraFin)
			Commit Tran 
			Return 1
		End Try
 		Begin Catch
			Rollback Tran
			Return -2
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
				Delete UsuariosEmpleado Where NombreUsuario = @NombreUsuario
				Delete Usuarios Where NombreUsuario = @NombreUsuario
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
 Select  u.NombreUsuario, u.Contrasenia, u.Nombre, ue.HoraInicio, ue.HoraFin from Usuarios u inner join UsuariosEmpleado ue on u.NombreUsuario = ue.NombreUsuario where u.NombreUsuario = @NombreUsuario AND u.Activo = 1
End
Go

Create Procedure UsuariosEmpleadoBuscarTodos @NombreUsuario varchar(50) AS 
Begin 
 Select u.NombreUsuario, u.Contrasenia, u.Nombre, ue.HoraInicio, ue.HoraFin from Usuarios u inner join UsuariosEmpleado ue on u.NombreUsuario = ue.NombreUsuario where u.NombreUsuario = @NombreUsuario 
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
	RETURN ident_current('SolicitudesDeEntrega');
End
go

Create Procedure SolicitudesDeEntregaModificarEstado  @NumeroInterno int AS
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


Create Procedure SolicitudesDeEntregaBuscar @NumeroInterno int AS 
Begin 
 Select * from SolicitudesDeEntrega where NumeroInterno = @NumeroInterno
End
Go

Create Procedure SolicitudesDeEntregaListar AS
	Select * From SolicitudesDeEntrega
Go



Alter Procedure SolicitudesDeEntregaListarPorEmpresa @NombreUsuario varchar(50) AS
Begin
	 Select se.NumeroInterno as 'Numero Interno', se.FechaDeEntrega as 'Fecha de Entrega', se.NombreDestinatario as 'Nombre Destinatario', se.DireccionDestinatario as 'Direcciòn Destinatario', se.EstadoSolicitud as 'Estado' From SolicitudesDeEntrega as se INNER JOIN SolicitudPaquete as sp on se.NumeroInterno = sp.NumeroInterno 
	 INNER JOIN Paquetes as p on sp.CodigodeBarras = p.CodigodeBarras 
	 Where p.NombreUsuarioEmpresa = @NombreUsuario
End
go

--------- Paquetes ------------

Create Procedure PaquetesAlta @CodigodeBarras int, @Tipo varchar(10), @Descripcion varchar(Max), @Peso decimal, @NombreUsuarioEmpresa varchar(50) As
Begin
		if( Exists (Select * From Paquetes Where CodigodeBarras = @CodigodeBarras))
	Begin
		return -1
	End 

	if (Not Exists (Select * From UsuariosEmpresa Where NombreUsuario = @NombreUsuarioEmpresa))
	Begin
		return -2
	End 

	Insert into Paquetes  values (@CodigodeBarras, @Tipo, @Descripcion, @Peso, @NombreUsuarioEmpresa )
	return 1 

	if @@ERROR <> 0 
		return -3
End
Go

Create Procedure PaqueteBuscar @CodigodeBarras int AS 
Begin 
 Select * from Paquetes where CodigodeBarras = @CodigodeBarras
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



