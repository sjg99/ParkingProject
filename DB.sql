USE [ElectivaIV]
GO
/****** Object:  Table [dbo].[tbingreso]    Script Date: 20/05/19 2:39:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbingreso](
	[Placa] [varchar](6) NULL,
	[Fecha] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbsalida]    Script Date: 20/05/19 2:39:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbsalida](
	[Placa] [varchar](6) NULL,
	[Fecha] [datetime] NULL,
	[Precio] [varchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbusuarios]    Script Date: 20/05/19 2:39:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbusuarios](
	[ID] [bigint] NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Telefono] [bigint] NOT NULL,
	[FechaNacido] [date] NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[FechaActivo] [date] NOT NULL,
	[FechaInactivo] [date] NULL,
	[Area] [varchar](50) NOT NULL,
	[Estado] [varchar](10) NOT NULL
) ON [PRIMARY]
GO
