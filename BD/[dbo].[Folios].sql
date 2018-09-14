USE [Enercable]
GO

/****** Object:  Table [dbo].[Folios]    Script Date: 14/09/2018 01:34:38 a.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Folios](
	[IdFolio] [int] IDENTITY(1,1) NOT NULL,
	[Folio] [varchar](max) NOT NULL,
	[Ciudad] [varchar](max) NOT NULL,
	[Estado] [varchar](max) NOT NULL,
	[Cluster] [varchar](max) NOT NULL,
	[OLT] [varchar](max) NOT NULL,
	[ClientesAfectados] [varchar](max) NOT NULL,
	[FallaReportada] [varchar](max) NOT NULL,
	[FechaYHoraDeAsignacionDespacho] [varchar](100) NOT NULL,
	[FechaYHoraDeAsignacionProvista] [varchar](100) NOT NULL,
	[HoraDeLlegadaALaZona] [varchar](100) NOT NULL,
	[HoraDeLaPrimeraMedicion] [varchar](max) NOT NULL,
	[UbicacionDePrimerSegundoNivelYDerivacionConSusFibras] [varchar](max) NOT NULL,
	[CausaDelDano] [varchar](max) NOT NULL,
	[UbicacionDelDano] [varchar](max) NOT NULL,
	[CoordenadasDelDanoX] [varchar](max) NOT NULL,
	[CoordenadasDelDanoY] [varchar](max) NOT NULL,
	[DescripcionDeActividades] [varchar](max) NOT NULL,
	[PotencialInicia] [varchar](max) NOT NULL,
	[PotencialFinal] [varchar](max) NOT NULL,
	[FechaHoraFinalReparacion] [varchar](100) NOT NULL,
	[IdPersona_TecnicoAtiende] [int] NOT NULL,
	[IdProveedor] [int] NOT NULL,
	[IdPersona_Supervisor] [int] NOT NULL,
	[IdPersona_AtendioDespacho] [int] NOT NULL,
	[JustificacionDeSLA] [varchar](max) NOT NULL,
	[FechaSistema] [datetime] NOT NULL,
	[IdSesion] [bigint] NOT NULL,
	[IdDepartamento] [int] NOT NULL,
	[IdFormato] [int] NULL,
 CONSTRAINT [PK_Folios] PRIMARY KEY CLUSTERED 
(
	[IdFolio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Folios] ADD  CONSTRAINT [DF_Folios_FechaSistema]  DEFAULT (getdate()) FOR [FechaSistema]
GO

ALTER TABLE [dbo].[Folios] ADD  CONSTRAINT [DF_Folios_IdDepartamento]  DEFAULT ((0)) FOR [IdDepartamento]
GO


