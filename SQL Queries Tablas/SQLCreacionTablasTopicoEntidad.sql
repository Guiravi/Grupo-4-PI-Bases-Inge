CREATE TABLE Autor
(	usernamePK NVARCHAR(20) NOT NULL,
	email	NVARCHAR(50) NOT NULL,
	nombre	NVARCHAR(30) NOT NULL,
	apellido1 NVARCHAR(30) NOT NULL,
	apellido2	NVARCHAR(30) NOT NULL,
	pais NVARCHAR(30),
	estado NVARCHAR(30),
	ciudad	NVARCHAR(30),
	imagenPerfil NVARCHAR(MAX) NOT NULL,
	hobbies NVARCHAR(MAX),
	habilidades NVARCHAR(MAX),
	idiomas NVARCHAR(MAX)

	CONSTRAINT PK_Autor PRIMARY KEY (usernamePK),
	CONSTRAINT UQ_Autor_email UNIQUE (email)
);

CREATE TABLE Articulo
(
	idArticuloPK INTEGER IDENTITY(0,1) NOT NULL,
	titulo NVARCHAR(200) NOT NULL,
	resumen NVARCHAR(MAX) NOT NULL,
	tipo INTEGER NOT NULL,
	contenido NVARCHAR(MAX) NOT NULL,
	fechaPublicacion DATE NOT NULL

	CONSTRAINT PK_Articulo PRIMARY KEY (idArticuloPK),
	CONSTRAINT UQ_Articulo_titulo UNIQUE (titulo),
	CONSTRAINT CK_Articulo_tipo CHECK (tipo IN (0,1))
);

CREATE TABLE Topico
(
	nombreTopicoPK NVARCHAR(50),
	descripcion NVARCHAR(MAX),

	CONSTRAINT PK_TopicoArticulo PRIMARY KEY (nombreTopicoPK)
);

CREATE TABLE TopicosArticulo
(
	idArticuloFK INTEGER NOT NULL,
	nombreTopicoFK NVARCHAR(50) NOT NULL

	CONSTRAINT PK_TopicosArticulo PRIMARY KEY(idArticuloFK, nombreTopicoFK),
	CONSTRAINT FK_TopicosArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT FK_TopicosArticulo_Topico FOREIGN KEY (nombreTopicoFK) REFERENCES Topico(nombreTopicoPK)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE ImagenArticuloCorto
(
	idImagenPK INTEGER IDENTITY(0,1),
	rutaImagen NVARCHAR(MAX) NOT NULL,
	idArticuloFK INTEGER NOT NULL

	CONSTRAINT PK_ImagenArticuloCorto PRIMARY KEY (idImagenPK),
	CONSTRAINT FK_ImagenArticuloCorto_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);