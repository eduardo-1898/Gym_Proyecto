
CREATE TABLE [dbo].[BitacoraErrores](
	[IdBitacoraErrores] [bigint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[Tipo] [varchar](50) NOT NULL,
	[FechaError] [datetime] NOT NULL,
 CONSTRAINT [PK_BitacoraErrores] PRIMARY KEY CLUSTERED 
(
	[IdBitacoraErrores] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-----------------------
  alter PROCEDURE [dbo].[InsertarBitacoraErrores]
  (		   
           @Descripcion varchar(100),
		   @Tipo varchar(50)
)
AS
BEGIN
INSERT INTO [dbo].[BitacoraErrores]
           ([Descripcion]
           ,FechaError
           ,[Tipo]
		   )   
     VALUES
	      (@Descripcion ,
		  		 GETDATE(),
				 @Tipo
)
END
