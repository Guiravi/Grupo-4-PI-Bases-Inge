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
