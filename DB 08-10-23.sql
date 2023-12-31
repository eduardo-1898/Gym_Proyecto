USE [BD_Proyecto_Gym]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/10/2023 16:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK__Roles__2A49584C0E14605F] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1001,1) NOT NULL,
	[Identificacion] [varchar](255) NULL,
	[NombreCompleto] [varchar](100) NULL,
	[IdRol] [int] NULL,
	[Correo] [varchar](255) NULL,
	[Contrasenna] [varchar](255) NULL,
	[Telefono] [int] NULL,
	[ClaveTemporal] [bit] NULL,
	[FechaCreacionUsuario] [datetime] NULL,
	[FechaCaducidad] [datetime] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK__Usuarios__5B65BF97A774DC34] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_VerUsuarios]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[View_VerUsuarios]
as
SELECT [IdUsuario]
      ,[Identificacion]
      ,[NombreCompleto]
      ,USU.[IdRol]
	  ,ROL.Nombre NombreRol
      ,[Correo]
      ,[Contrasenna]
      ,[Telefono]
      ,[ClaveTemporal]
      ,[FechaCreacionUsuario]
      ,[FechaCaducidad]
      ,USU.[Estado]
  FROM [BD_Proyecto_Gym].[dbo].[Usuarios] USU
  inner join [BD_Proyecto_Gym].[dbo].[Roles] ROL ON USU.[IdRol] = ROL.[IdRol]
  --order by USU.[IdUsuario] desc
GO
/****** Object:  Table [dbo].[Ejercicios_X_Rutina]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ejercicios_X_Rutina](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRutina] [int] NULL,
	[IdEjercicio] [int] NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK__Ejercici__3214EC0752724628] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ejercicios]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ejercicios](
	[IdEjercicio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[FechaCreacionEjercicio] [datetime] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK__Ejercici__F0E6D60546E81E0C] PRIMARY KEY CLUSTERED 
(
	[IdEjercicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rutinas]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rutinas](
	[IdRutina] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[FechaCreacionRutina] [datetime] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK__Rutinas__6E34CA3D822BCA0F] PRIMARY KEY CLUSTERED 
(
	[IdRutina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_VerEjercicios_X_Rutina]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[View_VerEjercicios_X_Rutina]
as
  SELECT  [Id]
      ,EJE.[IdRutina]
	  ,RU.Nombre NombreRutina
      ,EJE.[IdEjercicio]
	  ,RU.Nombre NombreEjercicio
      ,[FechaCreacion]
  FROM [BD_Proyecto_Gym].[dbo].[Ejercicios_X_Rutina] EJE
  INNER JOIN [BD_Proyecto_Gym].[dbo].[Rutinas] RU ON EJE.[IdRutina] = RU.[IdRutina]
  INNER JOIN [BD_Proyecto_Gym].[dbo].[Ejercicios] EJER ON  EJE.[IdEjercicio] = EJER.[IdEjercicio] 
GO
/****** Object:  Table [dbo].[Paquetes]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paquetes](
	[IdPaquete] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[CostoPaquete] [decimal](18, 2) NULL,
	[FechaCreacionPaquete] [datetime] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK__Paquetes__DE278F8BF9513C22] PRIMARY KEY CLUSTERED 
(
	[IdPaquete] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rutinas_X_Paquete]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rutinas_X_Paquete](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPaquete] [int] NULL,
	[IdRutina] [int] NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK__Rutinas___3214EC07A2C68BEA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_VerRutinas_X_Paquete]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[View_VerRutinas_X_Paquete]
as
SELECT  [Id]
      ,RXP.[IdPaquete]
	  ,RU.Nombre NombrePaquete
      ,RXP.[IdRutina]
	  ,PA.Nombre NombreRutina
      ,[FechaCreacion]
  FROM [BD_Proyecto_Gym].[dbo].[Rutinas_X_Paquete] RXP
INNER JOIN [BD_Proyecto_Gym].[dbo].[Rutinas] RU ON RXP.[IdRutina] = RU.[IdRutina]
INNER JOIN [BD_Proyecto_Gym].[dbo].[Paquetes] PA ON RXP.[IdPaquete] = PA.[IdPaquete]
GO
/****** Object:  Table [dbo].[Inscripciones]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inscripciones](
	[IdInscripcion] [int] IDENTITY(1001,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[IdPaquete] [int] NULL,
	[IdPago] [int] NULL,
	[FechaCreacionInscripcion] [datetime] NULL,
	[FechaCaducidadInscripcion] [datetime] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK__Inscripc__A122F2BFF3992514] PRIMARY KEY CLUSTERED 
(
	[IdInscripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_VerInscripciones]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[View_VerInscripciones]
as
  select 
       [IdInscripcion]
      ,INS.[IdUsuario]
	  ,USU.[NombreCompleto]
      ,INS.[IdPaquete]
	  ,PAQ.Nombre NombrePaquete
      ,INS.[IdPago]
      ,[FechaCreacionInscripcion]
      ,[FechaCaducidadInscripcion]
      ,INS.[Estado] 
  from [BD_Proyecto_Gym].[dbo].[Inscripciones] INS
  INNER JOIN  [BD_Proyecto_Gym].[dbo].[Usuarios] USU ON INS.[IdUsuario] = USU.[IdUsuario] 
  INNER JOIN  [BD_Proyecto_Gym].[dbo].[Paquetes] PAQ ON INS.[IdPaquete] = PAQ.[IdPaquete] 
GO
/****** Object:  Table [dbo].[Pagos]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos](
	[IdPago] [int] IDENTITY(1001,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[IdPaquete] [int] NULL,
	[Descripcion] [nvarchar](max) NULL,
	[MontoPago] [decimal](18, 2) NULL,
	[FechaCreacionPago] [datetime] NULL,
 CONSTRAINT [PK__Pagos__FC851A3A14407C17] PRIMARY KEY CLUSTERED 
(
	[IdPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_VerPagos]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[View_VerPagos]
as
 SELECT 
       [IdPago]
      ,PAG.[IdUsuario]
	  ,USU.[NombreCompleto]
      ,PAG.[IdPaquete]
	  ,PAQ.Nombre NombrePaquete
      ,[Descripcion]
      ,[MontoPago]
      ,[FechaCreacionPago]
 FROM [BD_Proyecto_Gym].[dbo].[Pagos] PAG
INNER JOIN  [BD_Proyecto_Gym].[dbo].[Usuarios] USU ON PAG.[IdUsuario] = USU.[IdUsuario]
 INNER JOIN  [BD_Proyecto_Gym].[dbo].[Paquetes] PAQ ON PAQ.[IdPaquete] = PAG.[IdPaquete] 
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([IdUsuario], [Identificacion], [NombreCompleto], [IdRol], [Correo], [Contrasenna], [Telefono], [ClaveTemporal], [FechaCreacionUsuario], [FechaCaducidad], [Estado]) VALUES (1001, N'string', N'string', NULL, N'string', N'string', 0, NULL, CAST(N'2023-10-08T14:29:26.570' AS DateTime), NULL, 0)
INSERT [dbo].[Usuarios] ([IdUsuario], [Identificacion], [NombreCompleto], [IdRol], [Correo], [Contrasenna], [Telefono], [ClaveTemporal], [FechaCreacionUsuario], [FechaCaducidad], [Estado]) VALUES (1002, N'string', N'string', NULL, N'string', N'string', 0, NULL, CAST(N'2023-10-08T14:35:38.507' AS DateTime), NULL, 0)
INSERT [dbo].[Usuarios] ([IdUsuario], [Identificacion], [NombreCompleto], [IdRol], [Correo], [Contrasenna], [Telefono], [ClaveTemporal], [FechaCreacionUsuario], [FechaCaducidad], [Estado]) VALUES (1003, N'213', N'we', NULL, N'wqe@qwe.wqe', N'23123', 123, NULL, CAST(N'2023-10-08T14:37:20.213' AS DateTime), NULL, 0)
INSERT [dbo].[Usuarios] ([IdUsuario], [Identificacion], [NombreCompleto], [IdRol], [Correo], [Contrasenna], [Telefono], [ClaveTemporal], [FechaCreacionUsuario], [FechaCaducidad], [Estado]) VALUES (1004, N'305100195', N'Mau', NULL, N'vargas7384@gmail.com', N'123', 6210, NULL, CAST(N'2023-10-08T14:37:21.230' AS DateTime), NULL, 0)
INSERT [dbo].[Usuarios] ([IdUsuario], [Identificacion], [NombreCompleto], [IdRol], [Correo], [Contrasenna], [Telefono], [ClaveTemporal], [FechaCreacionUsuario], [FechaCaducidad], [Estado]) VALUES (1005, N'123456', N'manzur', NULL, N'man@gmail.com', N'123', 123456, NULL, CAST(N'2023-10-08T16:00:05.577' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[Ejercicios] ADD  CONSTRAINT [DF_Ejercicios_FechaCreacionEjercicio]  DEFAULT (getdate()) FOR [FechaCreacionEjercicio]
GO
ALTER TABLE [dbo].[Ejercicios] ADD  CONSTRAINT [DF_Ejercicios_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Ejercicios_X_Rutina] ADD  CONSTRAINT [DF_Ejercicios_X_Rutina_FechaCreacionPaquete]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Inscripciones] ADD  CONSTRAINT [DF_Inscripciones_FechaCreacionInscripcion]  DEFAULT (getdate()) FOR [FechaCreacionInscripcion]
GO
ALTER TABLE [dbo].[Inscripciones] ADD  CONSTRAINT [DF_Inscripciones_FechaCaducidadInscripcion]  DEFAULT (dateadd(month,(1),getdate())) FOR [FechaCaducidadInscripcion]
GO
ALTER TABLE [dbo].[Inscripciones] ADD  CONSTRAINT [DF_Inscripciones_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Pagos] ADD  CONSTRAINT [DF_Pagos_FechaCreacionPago]  DEFAULT (getdate()) FOR [FechaCreacionPago]
GO
ALTER TABLE [dbo].[Paquetes] ADD  CONSTRAINT [DF_Paquetes_FechaCreacionPaquete]  DEFAULT (getdate()) FOR [FechaCreacionPaquete]
GO
ALTER TABLE [dbo].[Paquetes] ADD  CONSTRAINT [DF_Paquetes_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Rutinas] ADD  CONSTRAINT [DF_Rutinas_FechaCreacionRutina]  DEFAULT (getdate()) FOR [FechaCreacionRutina]
GO
ALTER TABLE [dbo].[Rutinas] ADD  CONSTRAINT [DF_Rutinas_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Rutinas_X_Paquete] ADD  CONSTRAINT [DF_Rutinas_X_Paquete_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [DF_Usuarios_FechaCreacionUsuario]  DEFAULT (getdate()) FOR [FechaCreacionUsuario]
GO
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [DF_Usuarios_Estado]  DEFAULT ((0)) FOR [Estado]
GO
ALTER TABLE [dbo].[Ejercicios_X_Rutina]  WITH CHECK ADD  CONSTRAINT [FK__Ejercicio__IdEje__60A75C0F] FOREIGN KEY([IdEjercicio])
REFERENCES [dbo].[Ejercicios] ([IdEjercicio])
GO
ALTER TABLE [dbo].[Ejercicios_X_Rutina] CHECK CONSTRAINT [FK__Ejercicio__IdEje__60A75C0F]
GO
ALTER TABLE [dbo].[Ejercicios_X_Rutina]  WITH CHECK ADD  CONSTRAINT [FK__Ejercicio__IdRut__5FB337D6] FOREIGN KEY([IdRutina])
REFERENCES [dbo].[Rutinas] ([IdRutina])
GO
ALTER TABLE [dbo].[Ejercicios_X_Rutina] CHECK CONSTRAINT [FK__Ejercicio__IdRut__5FB337D6]
GO
ALTER TABLE [dbo].[Inscripciones]  WITH CHECK ADD  CONSTRAINT [FK__Inscripci__PagoI__778AC167] FOREIGN KEY([IdPago])
REFERENCES [dbo].[Pagos] ([IdPago])
GO
ALTER TABLE [dbo].[Inscripciones] CHECK CONSTRAINT [FK__Inscripci__PagoI__778AC167]
GO
ALTER TABLE [dbo].[Inscripciones]  WITH CHECK ADD  CONSTRAINT [FK__Inscripci__Usuar__75A278F5] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Inscripciones] CHECK CONSTRAINT [FK__Inscripci__Usuar__75A278F5]
GO
ALTER TABLE [dbo].[Inscripciones]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_X_Paquetes] FOREIGN KEY([IdPaquete])
REFERENCES [dbo].[Paquetes] ([IdPaquete])
GO
ALTER TABLE [dbo].[Inscripciones] CHECK CONSTRAINT [FK_Inscripcion_X_Paquetes]
GO
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK__Pagos__IdUsuario__6754599E] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK__Pagos__IdUsuario__6754599E]
GO
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Paquete] FOREIGN KEY([IdPaquete])
REFERENCES [dbo].[Paquetes] ([IdPaquete])
GO
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_Paquete]
GO
ALTER TABLE [dbo].[Rutinas_X_Paquete]  WITH CHECK ADD  CONSTRAINT [FK__Rutinas_X__IdPaq__6383C8BA] FOREIGN KEY([IdPaquete])
REFERENCES [dbo].[Paquetes] ([IdPaquete])
GO
ALTER TABLE [dbo].[Rutinas_X_Paquete] CHECK CONSTRAINT [FK__Rutinas_X__IdPaq__6383C8BA]
GO
ALTER TABLE [dbo].[Rutinas_X_Paquete]  WITH CHECK ADD  CONSTRAINT [FK__Rutinas_X__IdRut__6477ECF3] FOREIGN KEY([IdRutina])
REFERENCES [dbo].[Rutinas] ([IdRutina])
GO
ALTER TABLE [dbo].[Rutinas_X_Paquete] CHECK CONSTRAINT [FK__Rutinas_X__IdRut__6477ECF3]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK__Usuarios__IdRol__3B75D760] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([IdRol])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK__Usuarios__IdRol__3B75D760]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEjercicio]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEjercicio]

( 	@IdEjercicio int,
    @Nombre [varchar](100)
	,@Estado bit)
AS
BEGIN
UPDATE [dbo].[Ejercicios]
   SET [Nombre] = @Nombre
      ,[Estado] = @Estado
 WHERE IdEjercicio = @IdEjercicio
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEjercicio_X_Rutina]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarEjercicio_X_Rutina]

( 
@Id int,
@IdRutina int,
@IdEjercicio int
)
AS
BEGIN
UPDATE [dbo].[Ejercicios_X_Rutina]
   SET [IdRutina] = @IdRutina
      ,[IdEjercicio] = @IdEjercicio
 WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarInscripcion]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarInscripcion]
( 
	@IdInscripcion int,
	@IdUsuario [int] ,
	@IdPaquete [int] ,
	@IdPago [int] ,
	@Estado bit
)
AS
BEGIN
UPDATE [dbo].[Inscripciones]
   SET [IdUsuario] = @IdUsuario
      ,[IdPaquete] = @IdPaquete
      ,[Estado] = @Estado
 WHERE IdInscripcion = @IdInscripcion
 END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarPago]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarPago]
( 
@IdPago int,
@IdUsuario int ,
@IdPaquete int,
@Descripcion [nvarchar](max) NULL,
@MontoPago [decimal](18, 2) NULL
)
AS
BEGIN
UPDATE [dbo].[Pagos]
   SET [IdUsuario] = @IdUsuario
      ,[IdPaquete] = @IdPaquete
      ,[Descripcion] = @Descripcion
      ,[MontoPago] = @MontoPago
 WHERE IdPago = @IdPago
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarPaquete]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarPaquete]

(  
	@IdPaquete int,
	@Nombre [varchar](100) ,
	@CostoPaquete [decimal](18, 2),
	@Estado bit

)
AS
BEGIN
UPDATE [dbo].[Paquetes]
   SET [Nombre] = @Nombre
      ,[CostoPaquete] = @CostoPaquete
      ,[Estado] = @Estado
 WHERE IdPaquete = @IdPaquete
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarRol]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarRol]

(   @IdRol int,
	@Nombre [varchar](50)
	,@Estado [bit] 
)
AS
BEGIN
UPDATE [dbo].[Roles]
   SET [Nombre] = @Nombre
      ,[Estado] = @Estado
 WHERE IdRol = @IdRol
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarRutina]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarRutina]

( 
@IdRutina int,
@Nombre [varchar](100),
@Estado bit
)
AS
BEGIN
UPDATE [dbo].[Rutinas]
   SET [Nombre] = @Nombre
      ,[Estado] = @Estado
 WHERE  IdRutina = @IdRutina
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarRutina_X_Paquete]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarRutina_X_Paquete]

( 
@Id int,
@IdPaquete int ,
@IdRutina int
)
AS
BEGIN
UPDATE [dbo].[Rutinas_X_Paquete]
   SET [IdPaquete] = @IdPaquete
      ,[IdRutina] = @IdRutina
 WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarUsuario]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarUsuario]
( 
	@IdUsuario int,
	 @Identificacion [varchar](255) 
	,@NombreCompleto [varchar](100) 
	,@IdRol [int] 
	,@Correo [varchar](255) 
	,@Contrasenna [varchar](255) 
	,@Telefono [int] 
	,@ClaveTemporal [bit] 
	,@FechaCaducidad datetime
	,@Estado bit

)
AS
BEGIN
UPDATE [dbo].[Usuarios]
   SET [Identificacion] = @Identificacion
      ,[NombreCompleto] = @NombreCompleto
      ,[IdRol] = @IdRol
      ,[Correo] = @Correo
      ,[Contrasenna] = @Contrasenna
      ,[Telefono] = @Telefono
      ,[ClaveTemporal] = @ClaveTemporal
      ,[FechaCaducidad] = @FechaCaducidad
      ,[Estado] = @Estado
 WHERE IdUsuario = @IdUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuario]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create   PROCEDURE  [dbo].[ConsultarUsuario] 
    @Correo VARCHAR(50),
    @Contrasenna        VARCHAR(8000)
AS
BEGIN
    SELECT *
      FROM [BD_Proyecto_Gym].[dbo].[Usuarios]
      WHERE RTRIM(LTRIM(UPPER(Correo))) = RTRIM(LTRIM(UPPER(@Correo)))
            AND Contrasenna = @Contrasenna
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarEjercicio]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarEjercicio]
(@IdEjercicio int )
AS
BEGIN
DELETE FROM [BD_Proyecto_Gym].[dbo].[Ejercicios] where IdEjercicio = @IdEjercicio
END

GO
/****** Object:  StoredProcedure [dbo].[EliminarEjercicio_X_Rutina]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarEjercicio_X_Rutina]
(  @Id int)
AS
BEGIN
DELETE FROM [BD_Proyecto_Gym].[dbo].[Ejercicios_X_Rutina] where Id =@Id
END


GO
/****** Object:  StoredProcedure [dbo].[EliminarInscripcion]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarInscripcion]

(  @IdInscripcion int)
AS
BEGIN
DELETE FROM [BD_Proyecto_Gym].[dbo].[Inscripciones] where IdInscripcion =@IdInscripcion
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarPago]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarPago]
(  @IdPago int)
AS
BEGIN
DELETE FROM [BD_Proyecto_Gym].[dbo].[Pagos] where  IdPago =@IdPago
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarPaquete]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarPaquete]
(@IdPaquete int)
AS
BEGIN
DELETE FROM [BD_Proyecto_Gym].[dbo].[Paquetes] where IdPaquete = @IdPaquete
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarRol]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarRol]
(@IdRol int)
AS
BEGIN
DELETE FROM [BD_Proyecto_Gym].[dbo].[Roles] where IdRol = @IdRol
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarRutina]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarRutina]

(@IdRutina int)
AS
BEGIN
DELETE FROM [BD_Proyecto_Gym].[dbo].[Rutinas] where IdRutina =@IdRutina
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarRutina_X_Paquete]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarRutina_X_Paquete]

(  @Id int)
AS
BEGIN
DELETE FROM [BD_Proyecto_Gym].[dbo].[Rutinas_X_Paquete] where Id =@Id
END

GO
/****** Object:  StoredProcedure [dbo].[EliminarUsuario]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarUsuario]
(@IdUsuario int)
AS
BEGIN
DELETE FROM [BD_Proyecto_Gym].[dbo].[Usuarios] where IdUsuario = @IdUsuario
END 
GO
/****** Object:  StoredProcedure [dbo].[InsertarEjercicio]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarEjercicio]
( 
	@Nombre [varchar](100) 
)
AS
BEGIN
INSERT INTO [dbo].[Ejercicios] ([Nombre]) VALUES (@Nombre)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarEjercicio_X_Rutina]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarEjercicio_X_Rutina]

( 
	@IdRutina [int],
	@IdEjercicio [int]


)
AS
BEGIN
INSERT INTO [dbo].[Ejercicios_X_Rutina]
           ([IdRutina]
           ,[IdEjercicio])
     VALUES
           (@IdRutina, @IdEjercicio )
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarInscripcion]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarInscripcion]

(  

	@IdUsuario [int] ,
	@IdPaquete [int] ,
	@IdPago [int] 

)
AS
BEGIN
INSERT INTO [dbo].[Inscripciones] 
			([IdUsuario]
           ,[IdPaquete]
           ,[IdPago])
     VALUES
           (@IdUsuario
           ,@IdPaquete
           ,@IdPago )
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarPago]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarPago]

(

	@IdUsuario [int],
	@IdPaquete [int],
	@Descripcion [nvarchar](max) NULL,
	@MontoPago [decimal](18, 2) NULL
)
AS
BEGIN
INSERT INTO [dbo].[Pagos]
           ([IdUsuario]
           ,[IdPaquete]
           ,[Descripcion]
           ,[MontoPago]
		   )
     VALUES
           (@IdUsuario
           ,@IdPaquete
           ,@Descripcion
           ,@MontoPago )
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarPaquete]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarPaquete]
(  
	@Nombre [varchar](100) ,
	@CostoPaquete [decimal](18, 2) 
)
as
BEGIN
INSERT INTO [dbo].[Paquetes] ([Nombre] ,[CostoPaquete] )  VALUES   (@Nombre, @CostoPaquete)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarRol]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarRol]
(  
	@Nombre [varchar](50)
	,@Estado [bit] 
)
as
BEGIN
INSERT INTO [BD_Proyecto_Gym].[dbo].[Roles] ([Nombre], [Estado]) VALUES (@Nombre , @Estado);
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarRutina]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarRutina]

( 

	@Nombre [varchar](100)

)
AS
BEGIN
INSERT INTO [dbo].[Rutinas] ([Nombre]) VALUES (@Nombre)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarRutina_X_Paquete]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarRutina_X_Paquete]
( 
	@IdPaquete [int] ,
	@IdRutina [int] 
)
AS
BEGIN
INSERT INTO [dbo].[Rutinas_X_Paquete]
           ([IdPaquete]
           ,[IdRutina] )
     VALUES
           ( @IdPaquete
           ,@IdRutina )
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarUsuario]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarUsuario]
(  

	 @Identificacion [varchar](255) 
	,@NombreCompleto [varchar](100) 
	,@IdRol [int] NULL
	,@Correo [varchar](255) 
	,@Contrasenna [varchar](255) 
	,@Telefono [int] NULL
	,@ClaveTemporal [bit] NULL
)
AS
BEGIN
INSERT INTO  [BD_Proyecto_Gym].[dbo].[Usuarios] 
([Identificacion]  ,[NombreCompleto] ,[IdRol] ,[Correo] ,[Contrasenna] ,[Telefono] ,[ClaveTemporal])
VALUES (@Identificacion,@NombreCompleto,@IdRol,@Correo,@Contrasenna,@Telefono,@ClaveTemporal)
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 8/10/2023 16:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Identificacion varchar(255),
	@NombreCompleto varchar(100),
	@Correo		varchar(255),
	@Telefono		int,
	@Contrasenna	varchar(255)
	
AS
BEGIN
	
	INSERT INTO [dbo].[Usuarios] (Identificacion,NombreCompleto,Correo,Telefono,Contrasenna)
    VALUES (@Identificacion, @NombreCompleto, @Correo, @Telefono, @Contrasenna)

END
GO
