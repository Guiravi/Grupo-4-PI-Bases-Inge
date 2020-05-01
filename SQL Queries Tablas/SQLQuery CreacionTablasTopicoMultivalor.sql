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
	fechaPublicacion DATE NOT NULL,
	nombreAutor NVARCHAR(MAX) NOT NULL,
	usernameFK NVARVCHAR(20) NOT NULL

	CONSTRAINT PK_Articulo PRIMARY KEY (idArticuloPK),
	CONSTRAINT UQ_Articulo_titulo UNIQUE (titulo),
	CONSTRAINT CK_Articulo_tipo CHECK (tipo IN (0,1))
	CONSTRAINT FK_Articulo_Autor FOREIGN KEY (usernameFK) REFERENCES Autor(usernamePK)
		ON DELETE SET NULL
		ON UPDATE CASCADE
);

CREATE TABLE Topico
(
	nombreTopico NVARCHAR(50) NOT NULL,
	idArticuloFK INTEGER	NOT NULL

	CONSTRAINT PK_Topico PRIMARY KEY (nombreTopico, idArticuloFK),
	CONSTRAINT FK_Topico_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE ImagenArticuloCorto
(
	idImagenPK INTEGER IDENTITY(0,1) NOT NULL,
	rutaImagen NVARCHAR(MAX) NOT NULL,
	idArticuloFK INTEGER NOT NULL

	CONSTRAINT PK_ImagenArticuloCorto PRIMARY KEY (idImagenPK),
	CONSTRAINT FK_ImagenArticuloCorto_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);