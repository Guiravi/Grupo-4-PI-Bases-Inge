CREATE TRIGGER TR_AI_AsignarMeritosPorVisita
ON [dbo].[Articulo]
AFTER UPDATE
AS
DECLARE @articuloAID int;
DECLARE @estado nvarchar(30);
SELECT @articuloAID = I.articuloAID FROM inserted I;
SELECT @estado = I.estado FROM inserted I;
IF UPDATE(visitas) AND (@estado = 'Publicado')
	BEGIN
			UPDATE [dbo].[Miembro]
			SET meritos += 1
			WHERE usernamePK IN (	SELECT usernameMiemFK 
									FROM [dbo].[MiembroAutorDeArticulo]
									WHERE idArticuloFK = @articuloAID)				
	END