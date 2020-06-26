CREATE TRIGGER TR_AD_EliminarMiembroAutorQuitarMeritos
ON [dbo].[MiembroAutorDeArticulo]
AFTER DELETE
AS
DECLARE @idArticuloFK INT
DECLARE @usernameMiemFK nvarchar(20)
DECLARE @estado nvarchar(30)
SELECT @idArticuloFK = D.idArticuloFK FROM deleted D
SELECT @usernameMiemFK = D.usernameMiemFK FROM deleted D
SELECT @estado = estado FROM Articulo WHERE articuloAID = @idArticuloFK
IF ( @estado = 'Publicado')
	BEGIN
		UPDATE Miembro
		SET meritos = meritos - (SELECT puntajeTotalRev
									FROM [dbo].Articulo 
									WHERE articuloAID = @idArticuloFK)
		WHERE usernamePK = @usernameMiemFK
	END