CREATE PROC USP_GetArticulosPorTopicoYTipo @categoria NVARCHAR(50), @topico NVARCHAR(50), @tipo NVARCHAR(50) AS
	IF(@tipo = '')
		BEGIN
			SELECT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem
            FROM  Articulo A
            JOIN ArticuloTrataTopico ATT
                ON A.articuloAID = ATT.idArticuloFK
            WHERE ATT.nombreCategoriaFK = @categoria
                AND ATT.nombreTopicoFK = @topico
                AND A.estado = 'Publicado'
                ORDER BY A.fechaPublicacion DESC; 
		END
	ELSE
		BEGIN
			SELECT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem
            FROM  Articulo A
            JOIN ArticuloTrataTopico ATT
                ON A.articuloAID = ATT.idArticuloFK
            WHERE ATT.nombreCategoriaFK = @categoria
                AND ATT.nombreTopicoFK = @topico
                AND A.tipo = @tipo
                AND A.estado = 'Publicado'
                ORDER BY A.fechaPublicacion DESC; 
		END
GO

CREATE PROC USP_GetAutoresDeArticulo @idArticuloPK INT AS
	SELECT M.usernamePK, M.nombre, M.apellido1, M.apellido2
	FROM Miembro M JOIN MiembroAutorDeArticulo MAA
		ON M.usernamePK = MAA.usernameMiemFK
    WHERE MAA.idArticuloFK = @idArticuloPK;
GO

CREATE TRIGGER TR_BI_AsignarRevisor
ON [dbo].[NucleoRevisaArticulo]
FOR INSERT
AS
DECLARE @articuloAID INT,
		@usernameMiemFK NVARCHAR(50)
SELECT @usernameMiemFK = I.usernameMiemFK FROM inserted I
SELECT @articuloAID = I.idArticuloFK FROM inserted I

BEGIN
		DELETE FROM [NucleoPuedeSerRevisorDeArticulo]
		WHERE @usernameMiemFK = [dbo].[NucleoPuedeSerRevisorDeArticulo].usernameMiemFK AND
         @articuloAID = [dbo].[NucleoPuedeSerRevisorDeArticulo].idArticuloFK

		IF('Requiere Revisión' = (SELECT estado FROM Articulo WHERE articuloAID = @articuloAID))
			BEGIN
				UPDATE Articulo
				SET estado = 'En Revisión'
				WHERE articuloAID = @articuloAID
			END
END

CREATE TRIGGER TR_IOI_CalificarArticuloAsignarMeritos
ON [dbo].[MiembroCalificaArticulo]
INSTEAD OF INSERT
AS
DECLARE @idArticuloFK int;
DECLARE @usernameFK_new NVARCHAR(50);
DECLARE @usernameFK_old NVARCHAR(50);
DECLARE @calificacion_new int;
DECLARE @calificacion_old int;
SELECT @idArticuloFK = I.idArticuloFK FROM inserted I;
SELECT @usernameFK_new = I.usernameMiemFK FROM inserted I;
SELECT @usernameFK_old = (SELECT usernameMiemFK
							FROM [dbo].[MiembroCalificaArticulo]
							WHERE idArticuloFK = @idArticuloFK
								AND usernameMiemFK = @usernameFK_new);
SET @calificacion_old = NULL;
SELECT @calificacion_new = I.calificacion FROM inserted I;
BEGIN
	IF(@usernameFK_old IS NOT NULL)
		BEGIN
			SELECT @calificacion_old = (SELECT calificacion
										FROM [dbo].[MiembroCalificaArticulo]
										WHERE idArticuloFK = @idArticuloFK
											AND usernameMiemFK = @usernameFK_old);

			DELETE FROM [dbo].[MiembroCalificaArticulo]
			WHERE usernameMiemFK = @usernameFK_old
				AND idArticuloFK = @idArticuloFK
						
			UPDATE [dbo].[Articulo]
			SET calificacionTotalMiem -= @calificacion_old
			WHERE @idArticuloFK = [dbo].[Articulo].articuloAID

			UPDATE [dbo].[Miembro]
			SET meritos -= @calificacion_old
			WHERE usernamePK IN (	SELECT usernameMiemFK 
									FROM [dbo].[MiembroAutorDeArticulo]
									WHERE idArticuloFK = @idArticuloFK)	
	END
	
	IF(@calificacion_new != @calificacion_old OR @calificacion_old IS NULL)
	BEGIN
		INSERT INTO [dbo].[MiembroCalificaArticulo]
		VALUES (@usernameFK_new, @idArticuloFK, @calificacion_new)

		UPDATE [dbo].[Articulo]
		SET calificacionTotalMiem += @calificacion_new
		WHERE @idArticuloFK = [dbo].[Articulo].articuloAID

		UPDATE [dbo].[Miembro]
		SET meritos += @calificacion_new
		WHERE usernamePK IN (	SELECT usernameMiemFK 
								FROM [dbo].[MiembroAutorDeArticulo]
								WHERE idArticuloFK = @idArticuloFK)	
	END				
END
