CREATE PROC USP_GetMiembro @usernamePK NVARCHAR(50) AS
	SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, informacionLaboral, meritos, activo, nombreRolFK
	FROM Miembro
	WHERE @usernamePK =  usernamePK
GO

CREATE PROC USP_GetCategoriasTopicosArticulo @idArticuloPK INT AS
	SELECT nombreCategoriaFK , nombreTopicoFK 
	FROM ArticuloTrataTopico 
	WHERE idArticuloFK = @idArticuloPK
GO

CREATE PROC USP_GetAutoresDeArticulo @idArticuloPK INT AS
	SELECT M.usernamePK, M.nombre, M.apellido1, M.apellido2
	FROM Miembro M JOIN MiembroAutorDeArticulo MAA
		ON M.usernamePK = MAA.usernameMiemFK
    WHERE MAA.idArticuloFK = @idArticuloPK;
GO

CREATE PROC USP_GetArticulosPorAutorYTipo @autores NVARCHAR(MAX), @tipo NVARCHAR(50) AS
	IF(@tipo = '')
		BEGIN
		SELECT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem
                        FROM  Articulo A JOIN MiembroAutorDeArticulo MAA
                            ON A.articuloAID = MAA.idArticuloFK
                        JOIN Miembro M
                            ON MAA.usernameMiemFK = M.usernamePK
                        WHERE M.nombre + M.apellido1 + ISNULL(M.apellido2, '') LIKE @autores
							AND A.estado = 'Publicado'
                        ORDER BY fechaPublicacion DESC;
		END
	ELSE
		BEGIN
		SELECT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem
                        FROM  Articulo A JOIN MiembroAutorDeArticulo MAA
                            ON A.articuloAID = MAA.idArticuloFK
                        JOIN Miembro M
                            ON MAA.usernameMiemFK = M.usernamePK
                        WHERE M.nombre + M.apellido1 + ISNULL(M.apellido2, '') LIKE @autores
                            AND tipo = @tipo
                            AND A.estado = 'Publicado'
                        ORDER BY fechaPublicacion DESC;
		END
GO

CREATE PROC USP_GetArticulosPorTituloYTipo @titulo NVARCHAR(MAX), @tipo NVARCHAR(50) AS
	IF(@tipo = '')
		BEGIN
			SELECT  *
			FROM  Articulo
			WHERE titulo LIKE @titulo
				AND estado = 'Publicado'
			ORDER BY fechaPublicacion DESC;
		END
	ELSE
		BEGIN
			SELECT  * 
            FROM  Articulo
            WHERE titulo LIKE @titulo
                AND tipo = @tipo
                AND estado = 'Publicado'
            ORDER BY fechaPublicacion DESC;
		END
GO

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