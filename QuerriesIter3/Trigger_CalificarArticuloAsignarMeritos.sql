CREATE TRIGGER TR_AI_CalificarArticulo
ON [dbo].[MiembroCalificaArticulo]
AFTER INSERT
AS
DECLARE @idArticuloFK int;
DECLARE @calificacion int;
SELECT @idArticuloFK = I.idArticuloFK FROM inserted I;
SELECT @calificacion = I.calificacion FROM inserted I;

BEGIN
	UPDATE [dbo].[Articulo]
	SET calificacionTotalMiem += @calificacion
	WHERE @idArticuloFK = [dbo].[Articulo].articuloAID

	UPDATE [dbo].[Miembro]
	SET meritos += @calificacion
	WHERE usernamePK IN (	SELECT usernameMiemFK 
							FROM [dbo].[MiembroAutorDeArticulo]
							WHERE idArticuloFK = @idArticuloFK)				
END