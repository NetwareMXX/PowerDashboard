USE [Enercable]
GO
/****** Object:  StoredProcedure [dbo].[GuardarFolio]    Script Date: 14/09/2018 12:45:12 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Alfredo
-- =============================================
ALTER PROCEDURE [dbo].[GuardarFolio]
	@IdFolio int,
	@Folio varchar(MAX),
	@Ciudad varchar(MAX),
	@Estado varchar(MAX),
	@Cluster varchar(MAX),
	@OLT varchar(MAX),
	@ClientesAfectados varchar(MAX),
	@FallaReportada varchar(MAX),
	@FechaYHoraDeAsignacionDespacho varchar(100),
	@FechaYHoraDeAsignacionProvista varchar(100),
	@HoraDeLlegadaALaZona varchar(100),
	@HoraDeLaPrimeraMedicion varchar(MAX),
	@UbicacionDePrimerSegundoNivelYDerivacionConSusFibras varchar(MAX),
	@CausaDelDano varchar(MAX),
	@UbicacionDelDano varchar(MAX),
	@CoordenadasDelDanoX varchar(MAX),
	@CoordenadasDelDanoY varchar(MAX),
	@DescripcionDeActividades varchar(MAX),
	@PotencialInicia varchar(MAX),
	@PotencialFinal varchar(MAX),
	@FechaHoraFinalReparacion varchar(100),
	@IdPersona_TecnicoAtiende int,
	@IdProveedor int,
	@IdPersona_Supervisor int,
	@IdPersona_AtendioDespacho int,
	@JustificacionDeSLA varchar(MAX),
	@ActualizarTrabajosRealizados bit,
	@TrabajosRealizados varchar(MAX),
	@ActualizarCordenadasCab24 bit,
	@CordenadasCab24 varchar(MAX),
	@IdSesion bigint	,
	@IdFormato int
AS
BEGIN
	SET NOCOUNT ON;

--    Declare
--	@IdFolio int = 0,
--	@Folio varchar(MAX) = 'Clean Up San José insurgentes ID-26885',
--	@Ciudad varchar(MAX) = 'CDMX',
--	@Estado varchar(MAX) = 'CDMX',
--	@Cluster varchar(MAX) = 'San José insurgentes',
--	@OLT varchar(MAX) = 'Álvaro Obregón',
--	@ClientesAfectados varchar(MAX) = 'NINGUNO',
--	@FallaReportada varchar(MAX) = 'anomalia en gasa y implementación',
--	@FechaYHoraDeAsignacionDespacho datetime = '17/05/18 16:45:00',
--	@FechaYHoraDeAsignacionProvista datetime = '17/05/18 16:45:00',
--	@HoraDeLlegadaALaZona datetime = '21/05/18 12:40:00',
--	@HoraDeLaPrimeraMedicion varchar(MAX) = 'hrs',
--	@UbicacionDePrimerSegundoNivelYDerivacionConSusFibras varchar(MAX) = '2N calle Juventino Rosas y Guty Cárdenas',
--	@CausaDelDano varchar(MAX) = 'anomalía en gasas y falta de Splitter',
--	@UbicacionDelDano varchar(MAX) = 'calle Juventino Rosas y Guty Cárdenas',
--	@CoordenadasDelDanoX varchar(MAX) = '-99.188857',
--	@CoordenadasDelDanoY varchar(MAX) = '19.357075',
--	@DescripcionDeActividades varchar(MAX) = 'Se atienden trabajos de clean-up , se encuentran 3 cierres 
--1.- cierre Mexfo se hace gasa y se coloca raqueta así como etiqueta
--2.- cierre AFL sin spliter se le realiza gasa y se le coloca raqueta, se busca 1N para alimentar un Splitter .
--En calle Pedro Luis y Angelina se valida en cierre mexfo pero no se encuentra puertos disponibles por lo que se necesita implementación de 1N, se queda con despacho de reforzar Splitter existente en el punto.
--2.- cierre LG con Splitter de 1/8 potencia de -20.4 Se realiza refinamiento. Una vez instalado Splitter de 1/16 con potencia de -23.3 Se hace gasa y se coloca raqueta así como etiqueta. Se validan clientes activos y finaliza trabajos',
--	@PotencialInicia varchar(MAX) = '-20.4 dBm',
--	@PotencialFinal varchar(MAX) = '-23.3dBm',
--	@FechaHoraFinalReparacion datetime = '21/05/18 16:30:00',
--	@IdPersona_TecnicoAtiende int = 1,
--	@IdProveedor int = 1,
--	@IdPersona_Supervisor int = 2,
--	@IdPersona_AtendioDespacho int = 3,
--	@JustificacionDeSLA varchar(MAX) = '',
--	@ActualizarTrabajosRealizados bit = 0,
--	@TrabajosRealizados varchar(MAX) = '1;75;3|2;64;2|3;81;3|4;1;2|5;82;1',
--	@ActualizarCordenadasCab24 bit = 0,
--	@CordenadasCab24 varchar(MAX) = '1;19.357075;-99.188857|2;19.350583;-99.189528',
--	@IdSesion bigint = 1	
	
	Declare @Accion Varchar(Max) = '';
	
		If (@IdFolio = 0)
		Begin
			Insert dbo.Folios (Folio, Ciudad, Estado, Cluster, OLT, ClientesAfectados, FallaReportada, FechaYHoraDeAsignacionDespacho, FechaYHoraDeAsignacionProvista, 
				HoraDeLlegadaALaZona, HoraDeLaPrimeraMedicion, UbicacionDePrimerSegundoNivelYDerivacionConSusFibras, CausaDelDano, UbicacionDelDano, CoordenadasDelDanoX, CoordenadasDelDanoY, 
				DescripcionDeActividades, PotencialInicia, PotencialFinal, FechaHoraFinalReparacion, IdPersona_TecnicoAtiende, IdProveedor, IdPersona_Supervisor, IdPersona_AtendioDespacho, 
				JustificacionDeSLA, FechaSistema, IdSesion, IdFormato)
			Values
				(@Folio, @Ciudad, @Estado, @Cluster, @OLT, @ClientesAfectados, @FallaReportada, @FechaYHoraDeAsignacionDespacho, @FechaYHoraDeAsignacionProvista, 
				@HoraDeLlegadaALaZona, @HoraDeLaPrimeraMedicion, @UbicacionDePrimerSegundoNivelYDerivacionConSusFibras, @CausaDelDano, @UbicacionDelDano, @CoordenadasDelDanoX, @CoordenadasDelDanoY, 
				@DescripcionDeActividades, @PotencialInicia, @PotencialFinal, @FechaHoraFinalReparacion, @IdPersona_TecnicoAtiende, @IdProveedor, @IdPersona_Supervisor, @IdPersona_AtendioDespacho, 
				@JustificacionDeSLA, Getdate(), @IdSesion,@IdFormato)
			Set @IdFolio = @@IDENTITY;

			--------------------------Limpiamos Trabajos Realizados--------------------------
			Delete From dbo.TrabajosRealizados Where IdFolio = @IdFolio;

			--------------------------Insertamos Trabajos Realizados--------------------------
			Insert dbo.TrabajosRealizados (IdFolio,Consecutivo,IdServicio,Cantidad,IdUnidad,PrecioUnitario,Total,FechaSistema,IdSesion)
			Select 
				@IdFolio IdFolio,
				Substring(Campo, 0, CharIndex(';',Campo,1)) Consecutivo,
				Substring(Right(Campo,(Len(Campo) - CharIndex(';',Campo,1))), 0, CharIndex(';',Right(Campo,(Len(Campo) - CharIndex(';',Campo,1))),1)) IdServicio,
				Right(Campo,CHARINDEX(';', REVERSE(Campo))-1) Cantidad,
				0 IdUnidad, 0 PrecioUnitario, 0 Total, Getdate() FechaSistema, 1 IdSesion
			From 
				dbo.DividirCadena(@TrabajosRealizados,'|')

			--------------------------Actualizamos Importes Trabajos Realizados--------------------------
			Update
				TR
			Set
				TR.IdUnidad = S.IdUnidad,
				TR.PrecioUnitario = S.PrecioUnitario,
				TR.Total = (S.PrecioUnitario * TR.Cantidad),
				TR.Descripcion= S.Descripcion
			From
				dbo.TrabajosRealizados TR
				Join dbo.Servicios S On TR.IdServicio = S.IdServicio
			Where
				TR.IdFolio = @IdFolio

			--------------------------Limpiamos Coordenadas CAB24--------------------------
			Delete From dbo.CordenadasCab24 Where IdFolio = @IdFolio;

			--------------------------Insertamos Coordenadas CAB24--------------------------		
			Insert dbo.CordenadasCab24 (IdFolio,Consecutivo,X,Y,FechaSistema,IdSesion)
			Select 
				@IdFolio IdFolio,
				Substring(Campo, 0, CharIndex(';',Campo,1)) Consecutivo,
				Right(Campo,CHARINDEX(';', REVERSE(Campo))-1) X,
				Substring(Right(Campo,(Len(Campo) - CharIndex(';',Campo,1))), 0, CharIndex(';',Right(Campo,(Len(Campo) - CharIndex(';',Campo,1))),1)) Y,			
				Getdate() FechaSistema, 1 IdSesion
			From 
				dbo.DividirCadena(@CordenadasCab24,'|')
		
			-----------------------------------Bitacora-----------------------------------
			Set @Accion = 'Registró el Folio: ' + @Folio + ', IdFolio: ' + Cast(@IdFolio As Varchar(10));
			Exec dbo.GuardarBitacora @Accion, @IdSesion 
		End
		Else Begin
			Update 
				dbo.Folios 
			Set 
				Folio = @Folio, 
				Ciudad = @Ciudad, 
				Estado = @Estado, 
				Cluster = @Cluster, 
				OLT = @OLT, 
				ClientesAfectados = @ClientesAfectados, 
				FallaReportada = @FallaReportada, 
				FechaYHoraDeAsignacionDespacho = @FechaYHoraDeAsignacionDespacho, 
				FechaYHoraDeAsignacionProvista = @FechaYHoraDeAsignacionProvista, 
				HoraDeLlegadaALaZona = @HoraDeLlegadaALaZona, 
				HoraDeLaPrimeraMedicion = @HoraDeLaPrimeraMedicion, 
				UbicacionDePrimerSegundoNivelYDerivacionConSusFibras = @UbicacionDePrimerSegundoNivelYDerivacionConSusFibras, 
				CausaDelDano = @CausaDelDano, 
				UbicacionDelDano = @UbicacionDelDano, 
				CoordenadasDelDanoX = @CoordenadasDelDanoX, 
				CoordenadasDelDanoY = @CoordenadasDelDanoY, 
				DescripcionDeActividades = @DescripcionDeActividades, 
				PotencialInicia = @PotencialInicia, 
				PotencialFinal = @PotencialFinal, 
				FechaHoraFinalReparacion = @FechaHoraFinalReparacion, 
				IdPersona_TecnicoAtiende = @IdPersona_TecnicoAtiende, 
				IdProveedor = @IdProveedor, 
				IdPersona_Supervisor = @IdPersona_Supervisor, 
				IdPersona_AtendioDespacho = @IdPersona_AtendioDespacho, 
				JustificacionDeSLA = @JustificacionDeSLA, 
				FechaSistema = Getdate(), 
				IdSesion = @IdSesion,
				IdFormato=@IdFormato
			where IdFolio = @IdFolio
			If (@ActualizarTrabajosRealizados = 1)
			Begin
				--------------------------Limpiamos Trabajos Realizados--------------------------
				Delete From dbo.TrabajosRealizados Where IdFolio = @IdFolio;

				--------------------------Insertamos Trabajos Realizados--------------------------
				Insert dbo.TrabajosRealizados (IdFolio,Consecutivo,IdServicio,Cantidad,IdUnidad,PrecioUnitario,Total,FechaSistema,IdSesion)
				Select 
					@IdFolio IdFolio,
					Substring(Campo, 0, CharIndex(';',Campo,1)) Consecutivo,
					Substring(Right(Campo,(Len(Campo) - CharIndex(';',Campo,1))), 0, CharIndex(';',Right(Campo,(Len(Campo) - CharIndex(';',Campo,1))),1)) IdServicio,
					Right(Campo,CHARINDEX(';', REVERSE(Campo))-1) Cantidad,
					0 IdUnidad, 0 PrecioUnitario, 0 Total, Getdate() FechaSistema, 1 IdSesion
				From 
					dbo.DividirCadena(@TrabajosRealizados,'|')

				--------------------------Actualizamos Importes Trabajos Realizados--------------------------
				Update
					TR
				Set
					TR.IdUnidad = S.IdUnidad,
					TR.PrecioUnitario = S.PrecioUnitario,
					TR.Total = (S.PrecioUnitario * TR.Cantidad),
					TR.Descripcion= S.Descripcion
				From
					dbo.TrabajosRealizados TR
					Join dbo.Servicios S On TR.IdServicio = S.IdServicio
				Where
					TR.IdFolio = @IdFolio
			End
		
			If (@ActualizarCordenadasCab24 = 1)
			Begin
				--------------------------Limpiamos Coordenadas CAB24--------------------------
				Delete From dbo.CordenadasCab24 Where IdFolio = @IdFolio;

				--------------------------Insertamos Coordenadas CAB24--------------------------		
				Insert dbo.CordenadasCab24 (IdFolio,Consecutivo,X,Y,FechaSistema,IdSesion)
				Select 
					@IdFolio IdFolio,
					Substring(Campo, 0, CharIndex(';',Campo,1)) Consecutivo,
					Right(Campo,CHARINDEX(';', REVERSE(Campo))-1) X,
					Substring(Right(Campo,(Len(Campo) - CharIndex(';',Campo,1))), 0, CharIndex(';',Right(Campo,(Len(Campo) - CharIndex(';',Campo,1))),1)) Y,			
					Getdate() FechaSistema, 1 IdSesion
				From 
					dbo.DividirCadena(@CordenadasCab24,'|')
			End

			-----------------------------------Bitacora-----------------------------------
			Set @Accion = 'Modificó el Folio: ' + @Folio + ', IdFolio: ' + Cast(@IdFolio As Varchar(10));
			Exec dbo.GuardarBitacora @Accion, @IdSesion 
		End
END
