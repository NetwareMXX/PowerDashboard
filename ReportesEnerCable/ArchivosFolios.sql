-- =============================================
-- Author: la chucha
-- =============================================
CREATE PROCEDURE [dbo].[ArchivosPorFolio]
	@IdFolio Int
	,@EsCroquis int
AS
BEGIN
	SET NOCOUNT ON;

    --Declare @IdFolio Int = 4;
	if(@EsCroquis != 1)
	begin
		Select
			A.IdFolio,
			'C:/'+C.RutaCarpeta+'/'+CAST(a.idfolio as varchar)+'/'+A.Nombre Ruta
		From
				dbo.Archivos A
				join dbo.Carpetas C on A.IdCarpeta= C.IdCarpeta
			   and a.IdCarpeta not in (2)
		Where
			A.IdFolio = @IdFolio	
	end
	else
	begin
		Select
			A.IdFolio,
			'C:/'+C.RutaCarpeta+'/'+CAST(a.idfolio as varchar)+'/'+A.Nombre Ruta
		From
				dbo.Archivos A
				join dbo.Carpetas C on A.IdCarpeta= C.IdCarpeta
			   and a.IdCarpeta  = 2
		Where
			A.IdFolio = @IdFolio	
	end

END


