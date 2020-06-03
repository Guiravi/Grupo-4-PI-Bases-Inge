CREATE TRIGGER [dbo].[TR_AD_MiembrosAutores]
ON [dbo].[MiembroAutorDeArticulo]
AFTER DELETE
AS
DECLARE @idArticuloFK INT
DECLARE @usernameMiemFK nvarchar(20)
DECLARE @estado nvarchar(30)
SELECT @idArticuloFK = D.idArticuloFK FROM deleted D
SELECT @usernameMiemFK = D.usernameMiemFK FROM deleted D
SELECT @estado = Articulo.estado FROM Articulo WHERE idArticuloPK = @idArticuloFK
IF ( @estado = 'Publicado')
	BEGIN
		UPDATE Miembro
		SET meritos = meritos - (SELECT puntajeTotalRev
									FROM [dbo].Articulo 
									WHERE @idArticuloFK = [dbo].Articulo.idArticuloPK)
		WHERE usernamePK = @usernameMiemFK
	END
