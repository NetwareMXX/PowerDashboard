USE [Enercable]
GO
/****** Object:  StoredProcedure [dbo].[EliminarServicioPorFormato]    Script Date: 12/09/2018 01:33:47 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Alfredo
-- =============================================
ALTER PROCEDURE [dbo].[EliminarServicioPorFormato]
	@IdServicio int,
	@IdFormato int,
	@IdSesion bigint
AS
BEGIN
	SET NOCOUNT ON;

	Declare @Accion Varchar(Max) = '';
    
	--Delete From dbo.ServiciosPorFormato Where IdServicioPorFormato = @IdServicioPorFormato;
	Delete From dbo.ServiciosPorFormato Where IdFormato = @IdFormato and IdServicio =@IdServicio
	-----------------------------------Bitacora-----------------------------------
	Set @Accion = 'Eliminó el ServiciosPorFormato: IdServicio' + Cast(@IdServicio As Varchar(10))+
	' y IdFormato: '+Cast(@IdFormato As Varchar(10));
	Exec dbo.GuardarBitacora @Accion, @IdSesion 
END
