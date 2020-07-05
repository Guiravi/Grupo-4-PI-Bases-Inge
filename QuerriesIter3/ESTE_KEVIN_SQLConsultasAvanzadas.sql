/* listaMiembrosParaSolictudRevision */
SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, informacionLaboral, 
											meritos, activo, nombreRolFK
	FROM Miembro
	WHERE nombreRolFK = 'Núcleo' AND
	NOT EXISTS
		(SELECT 1 FROM NucleoRevisaArticulo WHERE usernamePK = usernameMiemFK AND @articuloAID = idArticuloFK) AND
	NOT EXISTS
		(SELECT 1 FROM NucleoPuedeSerRevisorDeArticulo WHERE usernamePK = usernameMiemFK AND @articuloAID = idArticuloFK)


/*GetArticulosPorRol*/
SELECT M.nombreRolFK, COUNT(DISTINCT MAA.idArticuloFK) AS cantidad
	FROM [dbo].[Miembro] M
	JOIN [dbo].[MiembroAutorDeArticulo] MAA
		ON M.usernamePK = MAA.usernameMiemFK
	JOIN [dbo].[Articulo] A
		ON MAA.idArticuloFK = A.articuloAID
	WHERE A.estado = 'Publicado'
	GROUP BY M.nombreRolFK
	
/*GetHabilidadesPorIdioma*/
SELECT  MH.habilidad, MI.idiomaFK, COUNT(*) AS cantidad
                                    FROM [dbo].[Miembro] M
                                    JOIN [dbo].[MiembroHabilidad] MH
	                                    ON M.usernamePK = MH.usernameFK
                                    JOIN [dbo].[MiembroIdioma] MI
	                                    ON M.usernamePK = MI.usernameFK
                                    WHERE	MH.habilidad IN (SELECT habilidadPK
							         FROM [Catalogo].[Habilidad])
                                    GROUP BY MI.idiomaFK, MH.habilidad
                                    ORDER BY MH.habilidad, MI.idiomaFK

/*GetCategoriasTopicosNoAsociadosRol*/
SELECT CT.nombreCategoriaPK, CT.nombreTopicoPK
    FROM [dbo].[CategoriaTopico] CT
    WHERE CT.nombreCategoriaPK + ' ' + CT.nombreTopicoPK NOT IN (SELECT ATT.nombreCategoriaFK + ' ' + ATT.nombreTopicoFK
								FROM [dbo].[ArticuloTrataTopico] ATT
								JOIN [dbo].[Articulo] A
									ON ATT.idArticuloFK = A.articuloAID
								JOIN [dbo].[MiembroAutorDeArticulo] MAA
									ON A.articuloAID = MAA.idArticuloFK
								JOIN [dbo].[Miembro] M
									ON MAA.usernameMiemFK = M.usernamePK

								WHERE A.estado = 'Publicado')
																	
