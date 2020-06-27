CREATE TRIGGER TR_AI_PublicarArticuloAsignarMeritos
ON [dbo].[Articulo]
AFTER UPDATE
AS
DECLARE @estado nvarchar(30)
DECLARE @articuloAID INT
SELECT @estado = I.estado FROM inserted I
SELECT @articuloAID = I.articuloAID FROM inserted I
IF UPDATE(estado) AND (@estado LIKE 'Publicado')
	BEGIN
		UPDATE Articulo
		SET puntajeTotalRev = (SELECT AVG([dbo].NucleoRevisaArticulo.puntaje) 
									FROM [dbo].NucleoRevisaArticulo 
									WHERE idArticuloFK = @articuloAID)
		WHERE articuloAID = @articuloAID

		UPDATE Miembro
		SET meritos = meritos + (SELECT [dbo].Articulo.puntajeTotalRev
									FROM [dbo].Articulo 
									WHERE articuloAID = @articuloAID)
		WHERE usernamePK IN (SELECT usernameMiemFK 
							FROM [dbo].[MiembroAutorDeArticulo]
							WHERE idArticuloFK = @articuloAID)
	END