CREATE TRIGGER TR_AU_PuntajeNucleoRevisaArticulo
ON [dbo].[NucleoRevisaArticulo]
AFTER UPDATE
AS
DECLARE @username char(50);
DECLARE @meritos int;
DECLARE @idArticulo int;

SELECT @username = usernameMiemFK FROM inserted;
SELECT @idArticulo = idArticuloFK FROM inserted;
SELECT @meritos = meritos FROM Miembro 
	WHERE usernamePK = @username;

BEGIN
	UPDATE [dbo].[NucleoRevisaArticulo]
	SET puntaje = (@meritos*(opinionGeneral+contribucion+forma))
	WHERE usernameMiemFK = @username AND idArticuloFK = @idArticulo
END
