--PARA VER ESTRUCTURAS USAR ESTOS SELECT
---LAS QUE DICEN View SON VISTAS QUE NOS TRAEN LOS CAMPOS DESCRIPTIVOS DE LAS TABLAS 

  select * from [BD_Proyecto_Gym].[dbo].View_VerUsuarios
  select * from [BD_Proyecto_Gym].[dbo].Ejercicios
  select * from [BD_Proyecto_Gym].[dbo].[Rutinas]
  select * from [BD_Proyecto_Gym].[dbo].[Paquetes]
  select * from [BD_Proyecto_Gym].[dbo].View_VerEjercicios_X_Rutina
  select * from [BD_Proyecto_Gym].[dbo].View_VerRutinas_X_Paquete
  select * from	[BD_Proyecto_Gym].[dbo].View_VerInscripciones
  select * from	[BD_Proyecto_Gym].[dbo].View_VerPagos



 --PARA INSERTAR, MODIFICAR, ELIMINAR ESTRUCTURAS USAR ESTOS

select * from [BD_Proyecto_Gym].[dbo].[Roles]
select * from [BD_Proyecto_Gym].[dbo].[Usuarios]
select * from [BD_Proyecto_Gym].[dbo].[Ejercicios]
select * from [BD_Proyecto_Gym].[dbo].[Rutinas]
select * from [BD_Proyecto_Gym].[dbo].[Paquetes]
select * from [BD_Proyecto_Gym].[dbo].[Ejercicios_X_Rutina]
select * from [BD_Proyecto_Gym].[dbo].[Rutinas_X_Paquete]
select * from [BD_Proyecto_Gym].[dbo].[Pagos]
select * from [BD_Proyecto_Gym].[dbo].[Inscripciones]


----DATA

--PARA INSERTAR

----CREATE PROCEDURE dbo.InsertarRol
----(  
----	@Nombre [varchar](50)
----	,@Estado [bit] 
----)
----as
----BEGIN
----INSERT INTO [BD_Proyecto_Gym].[dbo].[Roles] ([Nombre], [Estado]) VALUES (@Nombre , @Estado);
----END


----CREATE PROCEDURE dbo.InsertarUsuario
----(  

----	 @Identificacion [varchar](255) 
----	,@NombreCompleto [varchar](100) 
----	,@IdRol [int] NULL
----	,@Correo [varchar](255) 
----	,@Contrasenna [varchar](255) 
----	,@Telefono [int] NULL
----	,@ClaveTemporal [bit] NULL
----)
----AS
----BEGIN
----INSERT INTO  [BD_Proyecto_Gym].[dbo].[Usuarios] 
----([Identificacion]  ,[NombreCompleto] ,[IdRol] ,[Correo] ,[Contrasenna] ,[Telefono] ,[ClaveTemporal])
----VALUES (@Identificacion,@NombreCompleto,@IdRol,@Correo,@Contrasenna,@Telefono,@ClaveTemporal)
----END


----CREATE PROCEDURE dbo.InsertarEjercicio
----( 
----	@Nombre [varchar](100) 
----)
----AS
----BEGIN
----INSERT INTO [dbo].[Ejercicios] ([Nombre]) VALUES (@Nombre)
----END


----CREATE PROCEDURE dbo.InsertarRutina
----( 
----	@Nombre [varchar](100)
----)
----AS
----BEGIN
----INSERT INTO [dbo].[Rutinas] ([Nombre]) VALUES (@Nombre)
----END


----CREATE PROCEDURE dbo.InsertarPaquete
----(  
----	@Nombre [varchar](100) ,
----	@CostoPaquete [decimal](18, 2) 
----)
----as
----BEGIN
----INSERT INTO [dbo].[Paquetes] ([Nombre] ,[CostoPaquete] )  VALUES   (@Nombre, @CostoPaquete)
----END




----CREATE PROCEDURE dbo.InsertarEjercicio_X_Rutina

----( 
----	@IdRutina [int],
----	@IdEjercicio [int]


----)
----AS
----BEGIN
----INSERT INTO [dbo].[Ejercicios_X_Rutina]
----           ([IdRutina]
----           ,[IdEjercicio])
----     VALUES
----           (@IdRutina, @IdEjercicio )
----END



----CREATE PROCEDURE dbo.InsertarRutina_X_Paquete
----( 
----	@IdPaquete [int] ,
----	@IdRutina [int] 
----)
----AS
----BEGIN
----INSERT INTO [dbo].[Rutinas_X_Paquete]
----           ([IdPaquete]
----           ,[IdRutina] )
----     VALUES
----           ( @IdPaquete
----           ,@IdRutina )
----END



----CREATE PROCEDURE dbo.InsertarPago

----(

----	@IdUsuario [int],
----	@IdPaquete [int],
----	@Descripcion [nvarchar](max) NULL,
----	@MontoPago [decimal](18, 2) NULL
----)
----AS
----BEGIN
----INSERT INTO [dbo].[Pagos]
----           ([IdUsuario]
----           ,[IdPaquete]
----           ,[Descripcion]
----           ,[MontoPago]
----		   )
----     VALUES
----           (@IdUsuario
----           ,@IdPaquete
----           ,@Descripcion
----           ,@MontoPago )
----END





----CREATE PROCEDURE dbo.InsertarInscripcion

----(  

----	@IdUsuario [int] ,
----	@IdPaquete [int] ,
----	@IdPago [int] 

----)
----AS
----BEGIN
----INSERT INTO [dbo].[Inscripciones] 
----			([IdUsuario]
----           ,[IdPaquete]
----           ,[IdPago])
----     VALUES
----           (@IdUsuario
----           ,@IdPaquete
----           ,@IdPago )
----END







----PARA MODIFICAR

----CREATE PROCEDURE dbo.ActualizarRol

----(   @IdRol int,
----	@Nombre [varchar](50)
----	,@Estado [bit] 
----)
----AS
----BEGIN
----UPDATE [dbo].[Roles]
----   SET [Nombre] = @Nombre
----      ,[Estado] = @Estado
---- WHERE IdRol = @IdRol
----END






----CREATE PROCEDURE dbo.ActualizarUsuario
----( 
----	@IdUsuario int,
----	 @Identificacion [varchar](255) 
----	,@NombreCompleto [varchar](100) 
----	,@IdRol [int] 
----	,@Correo [varchar](255) 
----	,@Contrasenna [varchar](255) 
----	,@Telefono [int] 
----	,@ClaveTemporal [bit] 
----	,@FechaCaducidad datetime
----	,@Estado bit

----)
----AS
----BEGIN
----UPDATE [dbo].[Usuarios]
----   SET [Identificacion] = @Identificacion
----      ,[NombreCompleto] = @NombreCompleto
----      ,[IdRol] = @IdRol
----      ,[Correo] = @Correo
----      ,[Contrasenna] = @Contrasenna
----      ,[Telefono] = @Telefono
----      ,[ClaveTemporal] = @ClaveTemporal
----      ,[FechaCaducidad] = @FechaCaducidad
----      ,[Estado] = @Estado
---- WHERE IdUsuario = @IdUsuario
----END






----CREATE PROCEDURE dbo.ActualizarEjercicio

----( 	@IdEjercicio int,
----    @Nombre [varchar](100)
----	,@Estado bit)
----AS
----BEGIN
----UPDATE [dbo].[Ejercicios]
----   SET [Nombre] = @Nombre
----      ,[Estado] = @Estado
---- WHERE IdEjercicio = @IdEjercicio
----END






----CREATE PROCEDURE dbo.ActualizarRutina

----( 
----@IdRutina int,
----@Nombre [varchar](100),
----@Estado bit
----)
----AS
----BEGIN
----UPDATE [dbo].[Rutinas]
----   SET [Nombre] = @Nombre
----      ,[Estado] = @Estado
---- WHERE  IdRutina = @IdRutina
----END




----CREATE PROCEDURE dbo.ActualizarPaquete

----(  
----	@IdPaquete int,
----	@Nombre [varchar](100) ,
----	@CostoPaquete [decimal](18, 2),
----	@Estado bit

----)
----AS
----BEGIN
----UPDATE [dbo].[Paquetes]
----   SET [Nombre] = @Nombre
----      ,[CostoPaquete] = @CostoPaquete
----      ,[Estado] = @Estado
---- WHERE IdPaquete = @IdPaquete
----END




----CREATE PROCEDURE dbo.ActualizarEjercicio_X_Rutina

----( 
----@Id int,
----@IdRutina int,
----@IdEjercicio int
----)
----AS
----BEGIN
----UPDATE [dbo].[Ejercicios_X_Rutina]
----   SET [IdRutina] = @IdRutina
----      ,[IdEjercicio] = @IdEjercicio
---- WHERE Id = @Id
----END








----CREATE PROCEDURE dbo.ActualizarRutina_X_Paquete

----( 
----@Id int,
----@IdPaquete int ,
----@IdRutina int
----)
----AS
----BEGIN
----UPDATE [dbo].[Rutinas_X_Paquete]
----   SET [IdPaquete] = @IdPaquete
----      ,[IdRutina] = @IdRutina
---- WHERE Id = @Id
----END






----CREATE PROCEDURE dbo.ActualizarPago
----( 
----@IdPago int,
----@IdUsuario int ,
----@IdPaquete int,
----@Descripcion [nvarchar](max) NULL,
----@MontoPago [decimal](18, 2) NULL
----)
----AS
----BEGIN
----UPDATE [dbo].[Pagos]
----   SET [IdUsuario] = @IdUsuario
----      ,[IdPaquete] = @IdPaquete
----      ,[Descripcion] = @Descripcion
----      ,[MontoPago] = @MontoPago
---- WHERE IdPago = @IdPago
----END





----CREATE PROCEDURE dbo.ActualizarInscripcion
----( 
----	@IdInscripcion int,
----	@IdUsuario [int] ,
----	@IdPaquete [int] ,
----	@IdPago [int] ,
----	@Estado bit
----)
----AS
----BEGIN
----UPDATE [dbo].[Inscripciones]
----   SET [IdUsuario] = @IdUsuario
----      ,[IdPaquete] = @IdPaquete
----      ,[Estado] = @Estado
---- WHERE IdInscripcion = @IdInscripcion
---- END






----PARA ELIMINAR



----CREATE PROCEDURE dbo.EliminarRol
----(@IdRol int)
----AS
----BEGIN
----DELETE FROM [BD_Proyecto_Gym].[dbo].[Roles] where IdRol = @IdRol
----END



----CREATE PROCEDURE dbo.EliminarUsuario
----(@IdUsuario int)
----AS
----BEGIN
----DELETE FROM [BD_Proyecto_Gym].[dbo].[Usuarios] where IdUsuario = @IdUsuario
----END 



----CREATE PROCEDURE dbo.EliminarEjercicio
----(@IdEjercicio int )
----AS
----BEGIN
----DELETE FROM [BD_Proyecto_Gym].[dbo].[Ejercicios] where IdEjercicio = @IdEjercicio
----END




----CREATE PROCEDURE dbo.EliminarRutina

----(@IdRutina int)
----AS
----BEGIN
----DELETE FROM [BD_Proyecto_Gym].[dbo].[Rutinas] where IdRutina =@IdRutina
----END



----CREATE PROCEDURE dbo.EliminarPaquete
----(@IdPaquete int)
----AS
----BEGIN
----DELETE FROM [BD_Proyecto_Gym].[dbo].[Paquetes] where IdPaquete = @IdPaquete
----END




----CREATE PROCEDURE dbo.EliminarEjercicio_X_Rutina
----(  @Id int)
----AS
----BEGIN
----DELETE FROM [BD_Proyecto_Gym].[dbo].[Ejercicios_X_Rutina] where Id =@Id
----END




----CREATE PROCEDURE dbo.EliminarRutina_X_Paquete

----(  @Id int)
----AS
----BEGIN
----DELETE FROM [BD_Proyecto_Gym].[dbo].[Rutinas_X_Paquete] where Id =@Id
----END



----CREATE PROCEDURE dbo.EliminarPago
----(  @IdPago int)
----AS
----BEGIN
----DELETE FROM [BD_Proyecto_Gym].[dbo].[Pagos] where  IdPago =@IdPago
----END



----CREATE PROCEDURE dbo.EliminarInscripcion

----(  @IdInscripcion int)
----AS
----BEGIN
----DELETE FROM [BD_Proyecto_Gym].[dbo].[Inscripciones] where IdInscripcion =@IdInscripcion
----END