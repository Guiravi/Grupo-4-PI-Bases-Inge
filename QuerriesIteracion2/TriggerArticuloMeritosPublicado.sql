CREATE TRIGGER [dbo].[TR_AI_MeritosPublicacion]
ON [dbo].[Articulo]
AFTER UPDATE
AS
DECLARE @estado nvarchar(30)
DECLARE @idArticuloPK INT
SELECT @estado = I.estado FROM inserted I
SELECT @idArticuloPK = I.idArticuloPK FROM inserted I
BEGIN
	IF(@estado LIKE 'Publicado')
	BEGIN
		UPDATE Miembro
		SET meritos = meritos + (SELECT AVG([dbo].NucleoRevisaArticulo.puntaje) 
									FROM [dbo].NucleoRevisaArticulo 
									WHERE @idArticuloPK = [dbo].NucleoRevisaArticulo.idArticuloFK)

		WHERE usernamePK IN (SELECT usernameMiemFK 
							FROM [dbo].[MiembroAutorDeArticulo]
							WHERE idArticuloFK = @idArticuloPK)

		UPDATE Miembro
		SET nombreRolFK = 'Activo'
		WHERE usernamePK IN (SELECT usernameMiemFK 
							FROM [dbo].[MiembroAutorDeArticulo]
							WHERE idArticuloFK = @idArticuloPK)
		AND nombreRolFK = 'Periférico'
	END
END