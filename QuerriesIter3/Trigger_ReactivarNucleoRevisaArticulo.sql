CREATE TRIGGER TR_AU_ReactivarNucleoRevisaArticulo
ON [dbo].Articulo
AFTER UPDATE
AS
DECLARE @estadoI nvarchar(50);
DECLARE @estadoD nvarchar(50);
DECLARE @idArticulo int;

SELECT @idArticulo = articuloAID FROM inserted;
SELECT @estadoI = estado FROM inserted;
SELECT @estadoD = estado FROM deleted;

IF (@estadoD = 'Aceptado con Correcciones' AND @estadoI = 'En Revisión')
BEGIN
	UPDATE NucleoRevisaArticulo
	SET estadoRevision = 'En Revisión'
	WHERE @idArticulo = idArticuloFK;
END
