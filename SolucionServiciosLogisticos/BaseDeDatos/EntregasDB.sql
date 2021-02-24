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

-- Creacion de tablas

USE Entregas
go

CREATE TABLE Usuarios (
	NombreUsuario varchar(50) not null Primary Key, 
	Contrasenia varchar(6) not null check(len(Contrasenia ) = 6 AND Contrasenia like '[a-zA-Z][a-zA-Z][a-zA-Z][0-9][0-9][^a-zA-Z0-9]'), 
	Nombre varchar(70) not null,
	Activo bit not null Default (1)
)
go

CREATE TABLE UsuariosEmpresa (
	NombreUsuario varchar(50) not null,
	Telefono varchar(9) not null,
	Direccion varchar(50) not null,
	Email varchar(50) not null check ( Email LIKE '%_@__%.__%'),
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
GO

CREATE TABLE SolicitudesDeEntrega (
	NumeroInterno int not null Identity (1,1) Primary Key,
	FechaDeEntrega date not null check (FechaDeEntrega >= GETDATE()),
	NombreDestinatario varchar(50) not null,
	DireccionDestinatario varchar(50) not null,
	EstadoSolicitud varchar(20) not null Default 'En Depósito' check( EstadoSolicitud IN ('En Depósito', 'En Camino', 'Entregado' )),
	NombreUsuarioEmpleado varchar(50) not null,
	FOREIGN KEY (NombreUsuarioEmpleado) REFERENCES UsuariosEmpleado(NombreUsuario)
)
GO


CREATE TABLE Paquetes (
	CodigodeBarras int not null Primary Key,
	Tipo varchar(10) not null check( Tipo IN ('fragil', 'común', 'bulto')),
	Descripcion varchar(MAX) not null,
	Peso decimal not null check( Peso >= 0 ),
	NombreUsuarioEmpresa varchar(50) not null,
	FOREIGN KEY (NombreUsuarioEmpresa) REFERENCES UsuariosEmpresa(NombreUsuario)
)
GO 

CREATE TABLE SolicitudPaquete (
	CodigodeBarras int not null,
	NumeroInterno int not null,
	PRIMARY KEY (CodigodeBarras, NumeroInterno),
	FOREIGN KEY (CodigodeBarras) REFERENCES Paquetes(CodigodeBarras),
	FOREIGN KEY (NumeroInterno) REFERENCES SolicitudesDeEntrega(NumeroInterno),
)
go 



----------------------------- Stored Procedures -------------------------------


CREATE PROCEDURE CambioContrasena @NombreUsuario varchar(50), @Contrasenia varchar (6) AS
Begin
	
		Declare @VarSentencia varchar (200)
		If Not Exists ( SELECT name FROM master.sys.server_principals WHERE name = @NombreUsuario AND type='U')
		return -1

		if (Not Exists (Select * from Usuarios Where NombreUsuario = @NombreUsuario AND Activo = 1)) 
			return -2
		

		Update Usuarios Set Contrasenia = @Contrasenia  Where NombreUsuario = @NombreUsuario			
		If (@@ERROR <> 0)
		Begin
			return -3
		End
		
		Declare @oldContra varchar (6)
		SET @oldContra = (Select Contrasenia From Usuarios Where NombreUsuario = @NombreUsuario)

		EXEC sp_password @old = @oldContra, @new = @NombreUsuario, @loginame = @Contrasenia
		
End
Go

---------- UsuariosEmpresa -------------------------




Create Procedure UsuariosEmpresaBaja @NombreUsuario varchar(50) As
Begin
	if (Not Exists(Select * From UsuariosEmpresa as ue INNER JOIN Usuarios as u on ue.NombreUsuario = u.NombreUsuario where u.NombreUsuario = @NombreUsuario AND u.Activo = 1))
		return -1
	
	Declare @VarSentencia varchar (200)

	if (Exists(Select * From Paquetes Where NombreUsuarioEmpresa = @NombreUsuario))
		Begin try
			Begin Tran
				Update Usuarios Set Activo = 0 Where NombreUsuario = @NombreUsuario 

				Set @VarSentencia = 'Drop Login [' + @NombreUsuario + ']'
				exec (@VarSentencia) 

				Set @VarSentencia = 'Drop User [' + @NombreUsuario + ']'
				exec (@VarSentencia)
			Commit Tran 
			Return 1
			End Try
 			Begin Catch
				Rollback Tran
				Return -2
			End Catch
	Else
		Begin try
			Begin Tran
				Delete UsuariosEmpresa Where NombreUsuario = @NombreUsuario
				
				Delete Usuarios Where NombreUsuario =  @NombreUsuario

				Set @VarSentencia = 'Drop Login [' + @NombreUsuario + ']'
				exec (@VarSentencia) 

				Set @VarSentencia = 'Drop User [' + @NombreUsuario + ']'
				exec (@VarSentencia)
			Commit Tran 
			Return 1
		End Try
 		Begin Catch
			Rollback Tran
			Return -2
		End Catch
End
Go 

Create Procedure UsuariosEmpresaModificar @NombreUsuario varchar(50), @Nombre varchar(70), @Telefono varchar(9), @Direccion varchar(50), @Email varchar(50)  As
Begin
	if (Not Exists(Select * From UsuariosEmpresa as ue INNER JOIN Usuarios as u on ue.NombreUsuario = u.NombreUsuario where u.NombreUsuario = @NombreUsuario AND u.Activo = 1))
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


Create Procedure UsuariosEmpleadoBaja @NombreUsuario varchar(50) As
Begin
	if (Not Exists(Select * From UsuariosEmpleado as ue INNER JOIN Usuarios as u on ue.NombreUsuario = u.NombreUsuario where u.NombreUsuario = @NombreUsuario AND u.Activo = 1))
		return -1
	
	Declare @VarSentencia varchar (200)
	
	if (Exists(Select * From SolicitudesDeEntrega Where NombreUsuarioEmpleado = @NombreUsuario))
			Begin try 
				Begin tran

				Update Usuarios Set Activo = 0 Where NombreUsuario = @NombreUsuario
				
				Set @VarSentencia = 'Drop Login [' + @NombreUsuario + ']'
				exec (@VarSentencia) 

				Set @VarSentencia = 'Drop User [' + @NombreUsuario + ']'
				exec (@VarSentencia)

				Commit tran 
				return 1
			End Try
 			Begin Catch
				Rollback Tran
				Return -2
			End Catch
	Else
			Begin try
				Begin Tran
					Delete UsuariosEmpleado Where NombreUsuario = @NombreUsuario
					
					Delete Usuarios Where NombreUsuario = @NombreUsuario
					
					Set @VarSentencia = 'Drop Login [' + @NombreUsuario + ']'
					exec (@VarSentencia) 

					Set @VarSentencia = 'Drop User [' + @NombreUsuario + ']'
					exec (@VarSentencia)
				Commit Tran 
				Return 1
			End Try
 			Begin Catch
				Rollback Tran
				Return -2
			End Catch
End
go 

Create Procedure UsuariosEmpleadoModificar  @NombreUsuario varchar(50), @Nombre varchar(70), @HoraInicio time, @HoraFin time As
Begin
	if (Not Exists (Select * From UsuariosEmpleado as ue INNER JOIN Usuarios as u on ue.NombreUsuario = u.NombreUsuario where u.NombreUsuario = @NombreUsuario AND u.Activo = 1)) 
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

Create Procedure LogueoUsuarioEmpresa @NombreUsuario varchar(50), @Contrasenia varchar(6) AS 
Begin 
	Select * From UsuariosEmpresa as ue Inner Join Usuarios as u ON ue.NombreUsuario = u.NombreUsuario where u.NombreUsuario = @NombreUsuario AND  u.Contrasenia = @Contrasenia AND Activo = 1
End
Go

Create Procedure LogueoUsuarioEmpleado @NombreUsuario varchar(50), @Contrasenia varchar(6) AS 
Begin 
	Select * From UsuariosEmpleado as ue Inner Join Usuarios as u ON ue.NombreUsuario = u.NombreUsuario where u.NombreUsuario = @NombreUsuario AND  u.Contrasenia = @Contrasenia AND Activo = 1
End
Go

--------- SolicitudesDeEntrega ------------

ALTER Procedure SolicitudesDeEntregaAlta @FechaDeEntrega date, @NombreDestinatario varchar(50), @DireccionDestinatario varchar(50), @NombreUsuarioEmpleado varchar(50)  As
Begin
if (Not Exists(Select * From UsuariosEmpleado as ue INNER JOIN Usuarios as u on ue.NombreUsuario = u.NombreUsuario where u.NombreUsuario = @NombreUsuarioEmpleado AND u.Activo = 1)) 
	Begin
		return -1
	End
Insert into SolicitudesDeEntrega (FechaDeEntrega, NombreDestinatario, DireccionDestinatario, NombreUsuarioEmpleado) values (@FechaDeEntrega, @NombreDestinatario, @DireccionDestinatario, @NombreUsuarioEmpleado )
	RETURN @@IDENTITY
	-- ident_current('SolicitudesDeEntrega');
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

Create Procedure SolicitudesDeEntregaListarCaminoyDeposito AS
	Select * From SolicitudesDeEntrega Where EstadoSolicitud = 'En Camino' AND EstadoSolicitud = 'En Depósito' 
Go

Create Procedure SolicitudesDeEntregaListarPorEmpresa @NombreUsuario varchar(50) AS
Begin
	 Select DISTINCT se.NumeroInterno , se.FechaDeEntrega, se.NombreDestinatario, se.DireccionDestinatario, se.EstadoSolicitud, se.NombreUsuarioEmpleado  
	 From SolicitudesDeEntrega as se INNER JOIN SolicitudPaquete as sp on se.NumeroInterno = sp.NumeroInterno 
	 INNER JOIN Paquetes as p on sp.CodigodeBarras = p.CodigodeBarras 
	 Where p.NombreUsuarioEmpresa = @NombreUsuario
End
go

--------- Paquetes ------------

Create Procedure PaquetesAlta @CodigodeBarras int, @Tipo varchar(10), @Descripcion varchar(Max), @Peso decimal, @NombreUsuarioEmpresa varchar(50) As
Begin
	
	if ( Exists (Select * From Paquetes Where CodigodeBarras = @CodigodeBarras))
	Begin
		return -1
	End 

	if (Not Exists (Select * From UsuariosEmpresa as ue INNER JOIN Usuarios as u on ue.NombreUsuario = u.NombreUsuario where u.NombreUsuario = @NombreUsuarioEmpresa AND u.Activo = 1)) -- verificar si es activo todas las FK revisar que este activo
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

	if ( Exists ( Select * From SolicitudPaquete Where CodigodeBarras = @CodigodeBarras AND NumeroInterno = @NumeroInterno ))
		return -3

	Insert into SolicitudPaquete values (@CodigodeBarras, @NumeroInterno )
	return 1
End
GO



------------- Roles --------------------------


CREATE ROLE Empresa 
GO 

GRANT EXECUTE ON CambioContrasena TO Empresa
GO
GRANT EXECUTE ON SolicitudesDeEntregaListarPorEmpresa TO Empresa
GO
GRANT EXECUTE ON UsuariosEmpleadoBuscarTodos TO Empresa
GO
GRANT EXECUTE ON PaquetesListadoPorSolicitud TO Empresa
GO
GRANT EXECUTE ON UsuariosEmpresaBuscarTodos TO Empresa
GO


CREATE ROLE Publico 
GO 
GRANT EXECUTE ON LogueoUsuarioEmpleado TO Publico
GO
GRANT EXECUTE ON LogueoUsuarioEmpresa TO Publico
GO
GRANT EXECUTE ON SolicitudesDeEntregaListar TO Publico
GO
GRANT EXECUTE ON UsuariosEmpleadoBuscarTodos TO Publico
GO
GRANT EXECUTE ON PaquetesListadoPorSolicitud TO Publico
GO
GRANT EXECUTE ON UsuariosEmpresaBuscarTodos TO Publico
GO

CREATE ROLE Empleado 
GO
GRANT EXECUTE TO Empleado
GO
REVOKE EXECUTE ON LogueoUsuarioEmpleado TO Empleado
GO
REVOKE EXECUTE ON LogueoUsuarioEmpresa TO Empleado
GO
REVOKE EXECUTE ON SolicitudesDeEntregaListarPorEmpresa TO Empleado
GO
GRANT EXECUTE ON SolicitudesDeEntregaListar TO Empleado
GO
GRANT ALTER ANY USER TO Empleado
GO
GRANT ALTER ANY ROLE TO Empleado
GO

---- Creacion de usuarios con asignacion de roles -----------------

--creacion de usuarios

USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS 
GO

USE Entregas
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

EXEC sp_addrolemember @rolename='Publico', @membername=[IIS APPPOOL\DefaultAppPool]
GO

USE Entregas
go


Create Procedure UsuariosEmpresaAlta @NombreUsuario varchar(50), @Contrasenia varchar(6), @Nombre varchar(70), @Telefono varchar(9), @Direccion varchar(50), @Email varchar(50)  As
Begin
	Declare @VarSentencia varchar(200)
		
	if (Exists (Select * from Usuarios  Where NombreUsuario = @NombreUsuario AND Activo = 1)) 
		return -1

	if (Exists (Select * from UsuariosEmpresa as ue Inner Join Usuarios as u on ue.NombreUsuario = u.NombreUsuario Where u.NombreUsuario = @NombreUsuario AND Activo = 0))
		BEGIN
			BEGIN TRY
				BEGIN TRAN
				--primero creo el usuario de logueo
				SET @VarSentencia = 'CREATE LOGIN [' + @NombreUsuario + '] WITH PASSWORD = ' + QUOTENAME (@Contrasenia, '''')
				EXEC(@VarSentencia)

			
				Set @VarSentencia = 'CREATE USER ['+@NombreUsuario + '] FROM LOGIN [' +@NombreUsuario + ']'
				exec (@VarSentencia)

			
				Update Usuarios Set Nombre = @Nombre, Activo = 1  Where NombreUsuario = @NombreUsuario 
			
				Update UsuariosEmpresa Set Telefono = @Telefono, Direccion = @Direccion, Email = @Email  Where NombreUsuario = @NombreUsuario	
				COMMIT TRAN
			END TRY 
			BEGIN CATCH
				ROLLBACK TRAN
				RETURN -2
			END CATCH
		END		
	Else
		BEGIN
			BEGIN TRY
				BEGIN TRAN 
				--primero creo el usuario de logueo
				SET @VarSentencia = 'CREATE LOGIN [' + @NombreUsuario + '] WITH PASSWORD = ' + QUOTENAME (@Contrasenia, '''')
				EXEC(@VarSentencia)

			
				Set @VarSentencia = 'CREATE USER ['+@NombreUsuario + '] FROM LOGIN [' +@NombreUsuario + ']'
				exec (@VarSentencia)

			
				Insert into Usuarios (NombreUsuario, Contrasenia, Nombre) values (@NombreUsuario,  @Contrasenia, @Nombre)
			
				Insert into UsuariosEmpresa (NombreUsuario, Telefono, Direccion, Email) values (@NombreUsuario, @Telefono, @Direccion, @Email)
			
			COMMIT TRAN
			END TRY 
			BEGIN CATCH
				ROLLBACK TRAN
				RETURN -2
			END CATCH
		END
		
		EXEC sp_addrolemember @rolename='Empresa', @membername=@NombreUsuario
		RETURN 1		
End
go

Create Procedure UsuariosEmpleadoAlta @NombreUsuario varchar(50), @Contrasenia varchar(6), @Nombre varchar(70), @HoraInicio time, @HoraFin time As
Begin

	Declare @VarSentencia varchar (200) 

	if (Exists (Select * from Usuarios  Where NombreUsuario = @NombreUsuario AND Activo = 1)) 
		return -1

	if (Exists (Select * From UsuariosEmpleado as ue INNER JOIN Usuarios as u on ue.NombreUsuario = u.NombreUsuario where u.NombreUsuario = @NombreUsuario AND u.Activo = 0))
		BEGIN
			BEGIN TRY 
				BEGIN TRAN
					--primero creo el usuario de logueo
					SET @VarSentencia = 'CREATE LOGIN [' + @NombreUsuario + '] WITH PASSWORD = ' + QUOTENAME (@Contrasenia, '''')
					EXEC(@VarSentencia)

					--segundo creo usuario db
					Set @VarSentencia = 'CREATE USER ['+@NombreUsuario + '] FROM LOGIN [' +@NombreUsuario + ']'
					exec (@VarSentencia)
			
					Update Usuarios Set Nombre = @Nombre, Activo = 1  Where NombreUsuario = @NombreUsuario

					Update UsuariosEmpleado Set HoraInicio = @HoraInicio, HoraFin = @HoraFin Where NombreUsuario = @NombreUsuario
			
				COMMIT TRAN
			END TRY	
			BEGIN CATCH
				ROLLBACK TRAN
				RETURN -2
			END CATCH
		END	
	Else
		BEGIN
			BEGIN TRY
				BEGIN TRAN 
				--primero creo el usuario de logueo
				SET @VarSentencia = 'CREATE LOGIN [' + @NombreUsuario + '] WITH PASSWORD = ' + QUOTENAME (@Contrasenia, '''')
				EXEC(@VarSentencia)

				--segundo creo usuari db
				Set @VarSentencia = 'CREATE USER ['+@NombreUsuario + '] FROM LOGIN [' +@NombreUsuario + ']'
				exec (@VarSentencia)

		
				Insert into Usuarios (NombreUsuario, Contrasenia, Nombre) values (@NombreUsuario,  @Contrasenia, @Nombre)
		
				Insert into UsuariosEmpleado (NombreUsuario, HoraInicio, HoraFin) values (@NombreUsuario, @HoraInicio, @HoraFin)
			
				COMMIT TRAN
			END TRY
			BEGIN CATCH
				ROLLBACK TRAN
				RETURN -2
			END CATCH	
		END		
		--asigno rol de servidor al usuario recien creado
		EXEC sp_addsrvrolemember @loginame=@NombreUsuario, @rolename='securityAdmin'

		EXEC sp_addrolemember @rolename='Empleado', @membername=@NombreUsuario  

		RETURN 1
End
go



-------------------------- Insert Data --------------------------------


--INSERT Usuarios (NombreUsuario, Contrasenia, Nombre) VALUES ('Juanjo Prueba', 'abg23!', 'Juan Jose Negris'),
--	('Mario Prueba', 'acb23@', 'Mario Puzo'),('James Prueba', 'adf23:', 'James Batdrock'),
--	('Ximena Prueba', 'afs24,', 'Ximena Smith'),('Boris Prueba', 'atr23!', 'Boris Hendrson')
--Go

--INSERT UsuariosEmpresa (NombreUsuario, Telefono, Direccion, Email) VALUES ('Juanjo Prueba', '098978987', 'Famailla 3293', 'juanjoprueba@gmail.com'),
--	('Mario Prueba', '098996654', '18 de Julio 1987', 'marioprueba@gmail.com'), ('James Prueba', '091971258', 'Elm Road 1875', 'jamesprueba@gmail.com')
--Go	

--INSERT UsuariosEmpleado (NombreUsuario, HoraInicio, HoraFin) VALUES ('Ximena Prueba', '09:00', '17:00'),
--	('Boris Prueba', '10:00', '18:00')
--Go

EXEC UsuariosEmpresaAlta 'JuanjoPrueba', 'abg23!', 'Juan Jose Negris', '098978987', 'Famailla 3293', 'juanjoprueba@gmail.com'
go
EXEC UsuariosEmpresaAlta 'MarioPrueba', 'acb23@', 'Mario Puzo',  '098996654', '18 de Julio 1987', 'marioprueba@gmail.com'
go
EXEC UsuariosEmpresaAlta 'JamesPrueba', 'adf23:', 'James Batdrock', '091971258', 'Elm Road 1875', 'jamesprueba@gmail.com'
go
EXEC UsuariosEmpleadoAlta 'XimenaPrueba', 'afs24,', 'Ximena Smith', '09:00', '17:00'
go
EXEC UsuariosEmpleadoAlta 'BorisPrueba', 'atr23!', 'Boris Hendrson',  '10:00', '18:00'
go


INSERT SolicitudesDeEntrega ( FechaDeEntrega, NombreDestinatario, DireccionDestinatario, NombreUsuarioEmpleado ) 
VALUES ( DATEADD(DAY, 105, GETDATE()), 'Ana Monterroso', 'Garibaldi 7941' ,'XimenaPrueba'),
( DATEADD(DAY, 105, GETDATE()), 'JaimeRoss', 'Ansina 1987' , 'BorisPrueba'),
( DATEADD(DAY, 98, GETDATE()), 'Peter Capusoto', '25 de mayo 789' , 'XimenaPrueba'),
( DATEADD(DAY, 59, GETDATE()), 'Ingrid Lopez', 'Ciganda 871' , 'BorisPrueba'),
( DATEADD(DAY, 67, GETDATE()), 'Sergio Dalmata', 'Miguelete 1972' , 'XimenaPrueba'),
( DATEADD(DAY, 72, GETDATE()), 'Jaime Anibal Gizman', 'Enric el Xavi 687' , 'BorisPrueba'),
( DATEADD(DAY, 120, GETDATE()), 'Vago President', 'Gianattasio km 25' , 'XimenaPrueba'),
( DATEADD(DAY, 158, GETDATE()), 'Eulalia Sierra', '17 metros 789' , 'BorisPrueba'),
( DATEADD(DAY, 78, GETDATE()), 'Kurtney Love', 'Gomensoro 1897', 'XimenaPrueba'),
( DATEADD(DAY, 85, GETDATE()), 'Salome Salomon', 'Trapani 7897' , 'BorisPrueba'),
( DATEADD(DAY, 115, GETDATE()), 'Dario Bravo', 'Roy Mondle 5454' , 'XimenaPrueba'),
( DATEADD(DAY, 100, GETDATE()), 'Eduardo Scott', '18 de Julio 8974', 'BorisPrueba'),

( DATEADD(DAY, 1, GETDATE()), 'Peter Capussoto', '18 de Julio 4978', 'BorisPrueba'),
( DATEADD(DAY, 1, GETDATE()), 'Ana Maria Perez', 'Colonia 1320', 'BorisPrueba'),
( DATEADD(DAY, 2, GETDATE()), 'Sonia Blaze', 'Roque Graseras 987', 'BorisPrueba'),
( DATEADD(DAY, 5, GETDATE()), 'Pity Alvarez', 'Perez Castellanos 1254', 'BorisPrueba'),
( DATEADD(DAY, 20, GETDATE()), 'Catty Ole', 'Sarandi 874', 'BorisPrueba'),
( DATEADD(DAY, 21, GETDATE()), 'Juan Random', '18 de Julio 999', 'BorisPrueba'),
( DATEADD(DAY, 21, GETDATE()), 'Serrana Doldan', 'Avda. Brasil 1958', 'BorisPrueba'),
( DATEADD(DAY, 20, GETDATE()), 'Jose Perco', 'Bvar España 1879', 'BorisPrueba'),
( DATEADD(DAY, 18, GETDATE()), 'Pablo Negris', 'Agraciada 3589', 'BorisPrueba'),
( DATEADD(DAY, 22, GETDATE()), 'Carlos Negris', 'Yi 9874', 'BorisPrueba'),
( DATEADD(DAY, 25, GETDATE()), 'Mauricio Caputti', 'Ciganda 871', 'BorisPrueba')
Go

INSERT Paquetes( CodigodeBarras, Tipo, Descripcion, Peso, NombreUsuarioEmpresa )  
VALUES ( 45454, 'fragil', 'Platos', 3200 , 'JuanjoPrueba'),
( 487878, 'común', 'Caja de zapatos', 800 , 'JuanjoPrueba'),
( 48788, 'fragil', 'IPad', 500 , 'JuanjoPrueba'),
( 1254, 'bulto', 'Caja comun', 3000 , 'JamesPrueba'),
( 8754, 'común', 'Mochila', 500 , 'MarioPrueba'),
( 98712, 'fragil', 'TV', 5000 , 'JamesPrueba'),
( 54458, 'bulto', 'Caja especial', 8500 , 'JuanjoPrueba'),
( 54545, 'fragil', 'Parabrisas', 16000 , 'JuanjoPrueba'),
( 885412, 'común', 'Caja comun', 3000 , 'MarioPrueba'),
( 85471, 'fragil', 'Parlantes', 2200 , 'JuanjoPrueba'),
( 66336, 'fragil', 'Auriculares', 300 , 'JuanjoPrueba'),
( 85214, 'común', 'Jeans', 800 , 'JuanjoPrueba'),
( 36255, 'fragil', 'Electrodomestico', 3200 , 'JamesPrueba'),
( 87456, 'bulto', 'Caja comun', 3000 , 'JuanjoPrueba'),
( 18744, 'común', 'Calzado', 1200 , 'MarioPrueba'),
( 84484, 'fragil', 'Lentes Caja', 2000 , 'JamesPrueba'),
( 78452, 'bulto', 'Caja grande', 6000 , 'JuanjoPrueba'),
( 23215, 'común', 'Sillas Plegables', 18000 , 'JuanjoPrueba'),
( 12121, 'fragil', 'Mesa vidrio', 20000 , 'JuanjoPrueba'),
( 22123, 'fragil', 'Heladera', 20500 , 'JuanjoPrueba'),
( 36952, 'común', 'Juguetes caja', 3800 , 'JamesPrueba'),
( 25321, 'fragil', 'Aticulos iluminacion', 6000 , 'JuanjoPrueba'),
( 84645, 'bulto', 'Caja pequeña', 1500 , 'MarioPrueba'),
( 231545, 'común', 'Mochila', 500 , 'JuanjoPrueba'),
( 96325, 'fragil', 'Espejo', 6500 , 'JuanjoPrueba'),
( 74145, 'bulto', 'Caja grande', 6000 , 'JuanjoPrueba'),
( 23123, 'fragil', 'Cristaleria', 3500 , 'JuanjoPrueba'),
( 84127, 'común', 'Remeras', 2000 , 'JamesPrueba'),
( 96365, 'fragil', 'Apple TV', 600 , 'JuanjoPrueba'),
( 35241, 'común', 'Championes bebe', 200 , 'JuanjoPrueba'),
( 85695, 'bulto', 'Caja pequeña', 1500 , 'MarioPrueba'),
( 5448, 'fragil', 'Caja pequeña', 1500 , 'JuanjoPrueba'),
( 4545, 'fragil', 'Caja pequeña', 2500 , 'JuanjoPrueba'),
( 5454, 'fragil', 'Caja pequeña', 800 , 'JuanjoPrueba'),
( 5487, 'fragil', 'Caja pequeña', 800 , 'JuanjoPrueba'),
( 987, 'bulto', 'Caja pequeña', 900 , 'JuanjoPrueba'),
( 328, 'bulto', 'Caja pequeña', 3500 , 'JuanjoPrueba'),
( 4936, 'común', 'Caja pequeña', 4500 , 'JuanjoPrueba'),
( 7845, 'común', 'Caja pequeña', 1200 , 'JuanjoPrueba'),
( 5489, 'común', 'Caja pequeña', 2158 , 'MarioPrueba'),
( 374, 'bulto', 'Caja pequeña', 798 , 'MarioPrueba'),
( 4544, 'bulto', 'Caja pequeña', 3650 , 'MarioPrueba'),
( 698, 'común', 'Caja pequeña', 490 , 'MarioPrueba'),
( 800, 'bulto', 'Caja pequeña', 1200 , 'MarioPrueba'),
( 4527, 'común', 'Caja pequeña', 1000 , 'MarioPrueba'),
( 2300, 'bulto', 'Caja pequeña', 1100 , 'JamesPrueba'),
( 856, 'común', 'Caja pequeña', 1200 , 'JamesPrueba'),
( 632, 'común', 'Caja pequeña', 500 , 'JamesPrueba'),
( 946, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),
( 7441, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),
( 441, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),

( 6895, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),
( 8902, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),
( 0321, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),

( 5987, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),

( 4444, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),
( 2255, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),

( 6902, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),
( 2121, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),
( 9092, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba'),
( 0236, 'bulto', 'Caja pequeña', 300 , 'JamesPrueba')
GO 

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
(5448, 12), (4545, 12),(5454, 12), 
(5487, 13), (987, 13), (328, 13),
(374, 14), (4544, 14), (698, 14),
(800, 15), (4527, 15),
(2300, 16), (856, 16),(632, 16),
(946, 17), (946, 17), (946, 17),
(7441, 18),(441, 18),
(6895, 19),(8902, 19),
(0321, 20), (5987, 20),
(4444, 21), (2255, 21),
(6902, 22),(2121, 22),
(9092, 23), (0236, 23)
GO



