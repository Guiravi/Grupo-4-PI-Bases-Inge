CREATE TABLE Rol
(
	nombrePK NVARCHAR(25) CONSTRAINT NN_Rol_nombrePK NOT NULL,
	peso INTEGER CONSTRAINT NN_Rol_peso NOT NULL,

	CONSTRAINT PK_Rol PRIMARY KEY(nombrePK)
);

CREATE TABLE Topico
(
	nombrePK NVARCHAR(50) CONSTRAINT NN_Topico_nombrePK NOT NULL,

	CONSTRAINT PK_Topico PRIMARY KEY (nombrePK),
);

CREATE TABLE Articulo
(
	idArticuloPK INTEGER IDENTITY(0,1) CONSTRAINT NN_Articulo_idArticuloPK NOT NULL,
	titulo NVARCHAR(200) CONSTRAINT NN_Articulo_titulo NOT NULL,
	tipo NVARCHAR(25) CONSTRAINT NN_Articulo_tipo NOT NULL,
	fechaPublicacion DATE CONSTRAINT NN_Articulo_fechaPublicacion NOT NULL,
	resumen NVARCHAR(MAX) CONSTRAINT NN_Articulo_resumen NOT NULL,
	contenido NVARCHAR(MAX) CONSTRAINT NN_Articulo_contenido NOT NULL,
	estado NVARCHAR(30) CONSTRAINT NN_Articulo_estado NOT NULL,
	visitas INTEGER CONSTRAINT DF_Articulo_visitas DEFAULT 0 CONSTRAINT NN_Articulo_visitas NOT NULL,
	puntajeTotalRev FLOAT,
	calificacionTotalMiem INTEGER CONSTRAINT DF_Articulo_calificacionTotalMiem DEFAULT 0 CONSTRAINT NN_Articulo_calificacionTotalMiem NOT NULL,

	CONSTRAINT PK_Articulo PRIMARY KEY (idArticuloPK),
	CONSTRAINT UQ_Articulo_titulo UNIQUE (titulo),
	CONSTRAINT CK_Articulo_tipo CHECK (tipo in ('Corto','Largo','Link')),
	CONSTRAINT CK_Articulo_estado CHECK (estado in ('En Progreso','Requiere Revisi�n', 'En Revisi�n', 'Rechazado', 'Aceptado con Correcciones', 'Publicado'))
);

CREATE TABLE Miembro
(
	usernamePK NVARCHAR(20) CONSTRAINT NN_Miembro_usernamePK NOT NULL,
	email	NVARCHAR(50) CONSTRAINT NN_Miembro_email NOT NULL,
	nombre	NVARCHAR(30)  CONSTRAINT NN_Miembro_nombre NOT NULL,
	apellido1 NVARCHAR(30) CONSTRAINT NN_Miembro_apellido1 NOT NULL,
	apellido2	NVARCHAR(30) CONSTRAINT NN_Miembro_apellido2 NOT NULL,
	fechaNacimiento DATE CONSTRAINT NN_Articulo_fechaNacimiento NOT NULL,
	pais NVARCHAR(50),
	estado NVARCHAR(50),
	ciudad	NVARCHAR(50),
	rutaImagenPerfil NVARCHAR(MAX) CONSTRAINT NN_Miembro_rutaImagenPerfil NOT NULL,
	hobbies NVARCHAR(MAX),
	habilidades NVARCHAR(MAX),
	idiomas NVARCHAR(MAX),
	informacionLaboral NVARCHAR(MAX),
	meritos INTEGER CONSTRAINT DF_Miembro_merito DEFAULT 0 CONSTRAINT NN_Miembro_merito NOT NULL,
	activo BIT CONSTRAINT DF_Miembro_activo DEFAULT 1 CONSTRAINT NN_Miembro_activo NOT NULL,
	nombreRolFK NVARCHAR(25) CONSTRAINT DF_Miembro_nombreRolFK DEFAULT 'Perif�rico' CONSTRAINT NN_Miembro_nombreRolFK NOT NULL,

	CONSTRAINT PK_Autor PRIMARY KEY (usernamePK),
	CONSTRAINT UQ_Autor_email UNIQUE (email),
	CONSTRAINT FK_Miembro_Rol FOREIGN KEY (nombreRolFK) REFERENCES ROL(nombrePK)
		ON DELETE NO ACTION --Se deber�an eliminar todos los usuarios asociados a este rol antes de eliminar el rol.
		ON UPDATE CASCADE
);

CREATE TABLE MiembroAutorDeArticulo 
(
	usernameMiemFK NVARCHAR(20) CONSTRAINT NN_MiembroAutorDeArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_MiembroAutorDeArticulo_idArticuloFK NOT NULL,

	CONSTRAINT PK_MiembroAutorDeArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_MiembroAutorDeArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE NO ACTION  --Se quiere mantener un historial de los autores de este art�culo aunque ya sean activos.
		ON UPDATE CASCADE,
	CONSTRAINT FK_MiembroAutorDeArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE --Se eliminan las tuplas que asocian a los autores con el articulo borrado.
		ON UPDATE CASCADE
);

CREATE TABLE MiembroCalificaArticulo
(
	usernameMiemFK NVARCHAR(20) CONSTRAINT NN_MiembroCalificaArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_MiembroCalificaArticulo_idArticuloFK NOT NULL,
	calificacion INTEGER CONSTRAINT NN_MiembroCalificaArticulo_calificacion NOT NULL,

	CONSTRAINT PK_MiembroCalificaArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_MiembroCalificaArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE NO ACTION --Se quiere mantener el registro de que este usuario calific� este art�culo
		ON UPDATE CASCADE,
	CONSTRAINT FK_MiembroCalificaArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE --Se eliminan todas las calificaciones de este art�culo.
		ON UPDATE CASCADE
);

CREATE TABLE NucleoInteresaRevisarArticulo
(
	usernameMiemFK NVARCHAR(20) CONSTRAINT NN_NucleoInteresaRevisarArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_NucleoInteresaRevisarArticulo_idArticuloFK NOT NULL,

	CONSTRAINT PK_NucleoInteresaRevisarArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_NucleoInteresaRevisarArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE CASCADE --Ya el usuario no va a existir, por lo que ya no tiene interes en revisar el art�culo
		ON UPDATE CASCADE,
	CONSTRAINT FK_NucleoInteresaRevisarArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE --Se eliminan todos las tuplas relacionadas a querer revisar este art�culo.
		ON UPDATE CASCADE
);

CREATE TABLE NucleoRevisaArticulo
(
	usernameMiemFK NVARCHAR(20) CONSTRAINT NN_NucleoRevisaArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_NucleoRevisaArticulo_idArticuloFK NOT NULL,	
	estadoRevision NVARCHAR(25)  CONSTRAINT DF_NucleoRevisaArticulo_estadoRevision DEFAULT 'En revision' CONSTRAINT NN_NucleoRevisaArticulo_estadoRevision NOT NULL,
	puntaje FLOAT,
	comentarios NVARCHAR(MAX),

	CONSTRAINT PK_NucleoRevisaArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_NucleoRevisaArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE NO ACTION --Se quiere mantener el registro del puntaje y comentarios dados por este autor, adem�s que influye en el c�lculo de los m�ritos.
		ON UPDATE CASCADE,
	CONSTRAINT FK_NucleoRevisaArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE --Se borran todas las revisiones asociadas al art�culo borrado.
		ON UPDATE CASCADE
);

CREATE TABLE ArticuloTrataTopico
(
	nombreTopicoFK NVARCHAR(50) CONSTRAINT NN_ArticuloTrataTopico_nombreTopicoFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_ArticuloTrataTopico_idArticuloFK NOT NULL,

	CONSTRAINT PK_ArticuloTrataTopico PRIMARY KEY(idArticuloFK, nombreTopicoFK),
	CONSTRAINT FK_ArticuloTrataTopico_Topico FOREIGN KEY (nombreTopicoFK) REFERENCES Topico(nombrePK)
		ON DELETE NO ACTION --Se tienen que borrar todos los art�culos asociados al t�pico antes de borrar el t�pico.
		ON UPDATE CASCADE,
	CONSTRAINT FK_ArticuloTrataTopico_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE --Se elimina la tupla que asociaba al art�culo con el t�pico.
		ON UPDATE CASCADE
);