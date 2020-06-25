CREATE SCHEMA Catalogo
GO

CREATE TABLE Catalogo.Pais
(
	paisPK NVARCHAR(50)

	CONSTRAINT PK_Pais PRIMARY KEY (paisPK),
);

CREATE TABLE Catalogo.Habilidad
(
	habilidadPK NVARCHAR(50)

	CONSTRAINT PK_Habilidad PRIMARY KEY (habilidadPK),
);

CREATE TABLE Catalogo.Idioma
(
	idiomaPK NVARCHAR(50)

	CONSTRAINT PK_Idioma PRIMARY KEY (idiomaPK),
);

CREATE TABLE Catalogo.Pasatiempo
(
	pasatiempoPK NVARCHAR(50)

	CONSTRAINT PK_Pasatiempo PRIMARY KEY (pasatiempoPK),
);

CREATE TABLE Rol
(
	nombrePK NVARCHAR(50) CONSTRAINT NN_Rol_nombrePK NOT NULL,
	peso INTEGER CONSTRAINT NN_Rol_peso NOT NULL,

	CONSTRAINT PK_Rol PRIMARY KEY(nombrePK)
);

CREATE TABLE CategoriaTopico
(
	nombreCategoriaPK NVARCHAR(50) CONSTRAINT NN_CategoriaTopico_nombreCategoriaPK NOT NULL,
	nombreTopicoPK NVARCHAR(50) CONSTRAINT NN_CategoriaTopico_nombreTopicoPK NOT NULL,
	
	CONSTRAINT PK_CategoriaTopico PRIMARY KEY (nombreCategoriaPK, nombreTopicoPK),
);

CREATE TABLE Articulo
(
	articuloAID INTEGER IDENTITY(0,1) CONSTRAINT NN_Articulo_articuloAID NOT NULL,
	titulo NVARCHAR(200) CONSTRAINT NN_Articulo_titulo NOT NULL,
	tipo NVARCHAR(50) CONSTRAINT NN_Articulo_tipo NOT NULL,
	fechaPublicacion DATE CONSTRAINT NN_Articulo_fechaPublicacion NOT NULL,
	resumen NVARCHAR(MAX) CONSTRAINT NN_Articulo_resumen NOT NULL,
	contenido NVARCHAR(MAX) CONSTRAINT NN_Articulo_contenido NOT NULL,
	estado NVARCHAR(50) CONSTRAINT NN_Articulo_estado NOT NULL,
	visitas INTEGER CONSTRAINT DF_Articulo_visitas DEFAULT 0 CONSTRAINT NN_Articulo_visitas NOT NULL,
	puntajeTotalRev FLOAT,
	calificacionTotalMiem INTEGER CONSTRAINT DF_Articulo_calificacionTotalMiem DEFAULT 0 CONSTRAINT NN_Articulo_calificacionTotalMiem NOT NULL,

	CONSTRAINT PK_Articulo PRIMARY KEY (articuloAID),
	CONSTRAINT UQ_Articulo_titulo UNIQUE (titulo),
	CONSTRAINT CK_Articulo_tipo CHECK (tipo in ('Corto','Largo','Link')),
	CONSTRAINT CK_Articulo_estado CHECK (estado in ('En Progreso','Requiere Revisión', 'En Revisión', 'Rechazado', 'Aceptado con Correcciones', 'Publicado'))
);

CREATE TABLE Miembro
(
	usernamePK NVARCHAR(50) CONSTRAINT NN_Miembro_usernamePK NOT NULL,
	email	NVARCHAR(100) CONSTRAINT NN_Miembro_email NOT NULL,
	nombre	NVARCHAR(50)  CONSTRAINT NN_Miembro_nombre NOT NULL,
	apellido1 NVARCHAR(50) CONSTRAINT NN_Miembro_apellido1 NOT NULL,
	apellido2	NVARCHAR(50),
	fechaNacimiento DATE,
	paisFK NVARCHAR(50) CONSTRAINT DF_Miembro_paisFK DEFAULT 'N/A' CONSTRAINT NN_Articulo_paisFK NOT NULL,
	estado NVARCHAR(50),
	ciudad	NVARCHAR(50),
	rutaImagenPerfil NVARCHAR(MAX) CONSTRAINT NN_Miembro_rutaImagenPerfil NOT NULL,
	informacionLaboral NVARCHAR(MAX),
	meritos FLOAT CONSTRAINT DF_Miembro_merito DEFAULT 0.0 CONSTRAINT NN_Miembro_merito NOT NULL,
	activo BIT CONSTRAINT DF_Miembro_activo DEFAULT 1 CONSTRAINT NN_Miembro_activo NOT NULL,
	nombreRolFK NVARCHAR(50) CONSTRAINT DF_Miembro_nombreRolFK DEFAULT 'Periférico' CONSTRAINT NN_Miembro_nombreRolFK NOT NULL,

	CONSTRAINT PK_Miembro PRIMARY KEY (usernamePK),
	CONSTRAINT UQ_Miembro_email UNIQUE (email),
	CONSTRAINT FK_Miembro_Rol FOREIGN KEY (nombreRolFK) REFERENCES ROL(nombrePK)
		ON DELETE NO ACTION --Se deberían eliminar todos los usuarios asociados a este rol antes de eliminar el rol.
		ON UPDATE CASCADE,
	CONSTRAINT FK_Miembro_CatalogoPais FOREIGN KEY (paisFK) REFERENCES Catalogo.Pais(paisPK)
		ON DELETE SET DEFAULT --A nivel de opciones para el usuario no se va a mostrar el valor N/A, pero si el país escopgido llegara a borrarse se quiere representar esta falta de país con el valor N/A
		ON UPDATE CASCADE 
);

CREATE TABLE MiembroAutorDeArticulo 
(
	usernameMiemFK NVARCHAR(50) CONSTRAINT NN_MiembroAutorDeArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_MiembroAutorDeArticulo_idArticuloFK NOT NULL,

	CONSTRAINT PK_MiembroAutorDeArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_MiembroAutorDeArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE NO ACTION  --Se quiere mantener un historial de los autores de este artículo aunque ya sean activos.
		ON UPDATE CASCADE,
	CONSTRAINT FK_MiembroAutorDeArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(articuloAID)
		ON DELETE CASCADE --Se eliminan las tuplas que asocian a los autores con el articulo borrado.
		ON UPDATE CASCADE
);

CREATE TABLE MiembroCalificaArticulo
(
	usernameMiemFK NVARCHAR(50) CONSTRAINT NN_MiembroCalificaArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_MiembroCalificaArticulo_idArticuloFK NOT NULL,
	calificacion INTEGER CONSTRAINT NN_MiembroCalificaArticulo_calificacion NOT NULL,

	CONSTRAINT PK_MiembroCalificaArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_MiembroCalificaArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE NO ACTION --Se quiere mantener el registro de que este usuario calificó este artículo
		ON UPDATE CASCADE,
	CONSTRAINT FK_MiembroCalificaArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(articuloAID)
		ON DELETE CASCADE --Se eliminan todas las calificaciones de este artículo.
		ON UPDATE CASCADE
);

CREATE TABLE NucleoPuedeSerRevisorDeArticulo
(
	usernameMiemFK NVARCHAR(50) CONSTRAINT NN_NucleoInteresaRevisarArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_NucleoInteresaRevisarArticulo_idArticuloFK NOT NULL,
	estado	NVARCHAR(50) CONSTRAINT NN_NucleoPuedeSerRevisorDeArticulo_estado NOT NULL,

	CONSTRAINT PK_NucleoInteresaRevisarArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_NucleoInteresaRevisarArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE CASCADE --Ya el usuario no va a existir, por lo que ya no tiene interes en revisar el artículo
		ON UPDATE CASCADE,
	CONSTRAINT FK_NucleoInteresaRevisarArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(articuloAID)
		ON DELETE CASCADE --Se eliminan todos las tuplas relacionadas a querer revisar este artículo.
		ON UPDATE CASCADE,
	CONSTRAINT CK_NucleoPuedeSerRevisorDeArticulo_estado CHECK (estado in ('Interesa','Solicitado')),
);

CREATE TABLE NucleoRevisaArticulo
(
	usernameMiemFK NVARCHAR(50) CONSTRAINT NN_NucleoRevisaArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_NucleoRevisaArticulo_idArticuloFK NOT NULL,	
	estadoRevision NVARCHAR(25)  CONSTRAINT DF_NucleoRevisaArticulo_estadoRevision DEFAULT 'En Revisión' CONSTRAINT NN_NucleoRevisaArticulo_estadoRevision NOT NULL,
	puntaje FLOAT,
	opinionGeneral INTEGER,
	contribucion INTEGER,
	forma INTEGER,
	comentarios NVARCHAR(MAX),

	CONSTRAINT PK_NucleoRevisaArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_NucleoRevisaArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE NO ACTION --Se quiere mantener el registro del puntaje y comentarios dados por este autor, además que influye en el cálculo de los méritos.
		ON UPDATE CASCADE,
	CONSTRAINT FK_NucleoRevisaArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(articuloAID)
		ON DELETE CASCADE --Si se elimina el artículo en su proceso de revisión se quieren eliminar todos los registros de revisión asociados a este artículo.
		ON UPDATE CASCADE,
	CONSTRAINT CK_NucleoRevisaArticulo_estadoRevision CHECK (estadoRevision in ('En Revisión', 'Finalizada')),
);

CREATE TABLE ArticuloTrataTopico
(
	nombreCategoriaFK NVARCHAR(50) CONSTRAINT NN_ArticuloTrataTopico_nombreCategoriaFK NOT NULL,
	nombreTopicoFK NVARCHAR(50) CONSTRAINT NN_ArticuloTrataTopico_nombreTopicoFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_ArticuloTrataTopico_idArticuloFK NOT NULL,

	CONSTRAINT PK_ArticuloTrataTopico PRIMARY KEY(nombreCategoriaFK, nombreTopicoFK, idArticuloFK),
	CONSTRAINT FK_ArticuloTrataTopico_CategoriaTopico FOREIGN KEY (nombreCategoriaFK, nombreTopicoFK) REFERENCES CategoriaTopico(nombreCategoriaPK, nombreTopicoPK)
		ON DELETE NO ACTION --Se tienen que borrar todos los artículos asociados al tópico antes de borrar el tópico.
		ON UPDATE CASCADE,
	CONSTRAINT FK_ArticuloTrataTopico_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(articuloAID)
		ON DELETE CASCADE --Se elimina la tupla que asociaba al artículo con el tópico.
		ON UPDATE CASCADE
);

CREATE TABLE MiembroPasatiempo
(
	pasatiempoFK NVARCHAR(50) CONSTRAINT NN_MiembroPasatiempo_pasatiempoFK NOT NULL,
	usernameFK NVARCHAR(50) CONSTRAINT NN_MiembroPasatiempo_usernameFK NOT NULL,

	CONSTRAINT PK_MiembroPasatiempo PRIMARY KEY(pasatiempoFK, usernameFK),
	CONSTRAINT FK_MiembroPasatiempo_CatalogoPasatiempo FOREIGN KEY (pasatiempoFK) REFERENCES Catalogo.Pasatiempo(pasatiempoPK)
		ON DELETE CASCADE --Si se elimina el pasatiempo, se quiere eliminar la tupla en esta tabla que asocia al miembro con este pasatiempo borrado.
		ON UPDATE CASCADE,
	CONSTRAINT FK_MiembroPasatiempo_Miembro FOREIGN KEY (usernameFK) REFERENCES Miembro(usernamePK)
		ON DELETE CASCADE --Si se elimina al miembro, se quiere eliminar la relación que tenía este miembro con sus pasatiempos.
		ON UPDATE CASCADE,
);

CREATE TABLE MiembroIdioma
(
	idiomaFK NVARCHAR(50) CONSTRAINT NN_MiembroIdioma_idiomaFK NOT NULL,
	usernameFK NVARCHAR(50) CONSTRAINT NN_MiembroIdioma_usernameFK NOT NULL,

	CONSTRAINT PK_MiembroIdioma PRIMARY KEY(idiomaFK, usernameFK),
	CONSTRAINT FK_MiembroIdioma_CatalogoIdioma FOREIGN KEY (idiomaFK) REFERENCES Catalogo.Idioma(idiomaPK)
		ON DELETE CASCADE --Si se elimina el idioma, se quiere eliminar la tupla en esta tabla que asocia al miembro con este idioma borrado.
		ON UPDATE CASCADE,
	CONSTRAINT FK_MiembroIdioma_Miembro FOREIGN KEY (usernameFK) REFERENCES Miembro(usernamePK)
		ON DELETE CASCADE --Si se elimina al miembro, se quiere eliminar la relación que tenía este miembro con sus idiomas.
		ON UPDATE CASCADE,
);

CREATE TABLE MiembroHabilidad
(
	habilidadFK NVARCHAR(50) CONSTRAINT NN_MiembroHabilidad_habilidadFK NOT NULL,
	usernameFK NVARCHAR(50) CONSTRAINT NN_MiembroHabilidad_usernameFK NOT NULL,

	CONSTRAINT PK_MiembroHabilidad PRIMARY KEY(habilidadFK, usernameFK),
	CONSTRAINT FK_MiembroHabilidado_CatalogoHabilidad FOREIGN KEY (habilidadFK) REFERENCES Catalogo.Habilidad(habilidadPK)
		ON DELETE CASCADE --Si se elimina la habilidad, se quiere eliminar la tupla en esta tabla que asocia al miembro con esta habilidad borrado.
		ON UPDATE CASCADE,
	CONSTRAINT FK_MiembroHabilidad_Miembro FOREIGN KEY (usernameFK) REFERENCES Miembro(usernamePK)
		ON DELETE CASCADE --Si se elimina al miembro, se quiere eliminar la relación que tenía este miembro con sus habilidades.
		ON UPDATE CASCADE,
);

CREATE TABLE Notificacion
(
	notificacionAID INTEGER IDENTITY(0,1) CONSTRAINT NN_Notificacion_notificacionAID NOT NULL,
	usernameFK NVARCHAR(50) CONSTRAINT NN_Notificacion_usernameFK NOT NULL,
	fechaCreacion DATE CONSTRAINT NN_Notificacion_fechaCreacion NOT NULL,
	mensaje NVARCHAR(MAX) CONSTRAINT NN_Notificacion_mensaje NOT NULL,
	estado NVARCHAR(50)  CONSTRAINT DF_Notificacion_estado DEFAULT 'Nueva' CONSTRAINT NN_Notificacion_mensaje NOT NULL,
	url NVARCHAR(MAX),

	CONSTRAINT PK_Notificacion PRIMARY KEY (notificacionAID),
	CONSTRAINT FK_Notificacion_Miembro FOREIGN KEY (usernameFK) REFERENCES Miembro(usernamePK)
		ON DELETE CASCADE --Si se elimina al miembro, se quieren eliminar todas sus notificaciones asociadas para no tener notificaciones sin dueño.
		ON UPDATE CASCADE,
	CONSTRAINT CK_Notificacion_estado CHECK (estado in ('Nueva','Leída')),
);

CREATE TABLE MiembroSolicitaSubirRangoNucleo
(
	usernameNucleoFK NVARCHAR(50) CONSTRAINT NN_MiembroSolicitaSubirRangoNucleo_usernameNucleoFK NOT NULL,
	usernameMiembroFK NVARCHAR(50) CONSTRAINT NN_MiembroSolicitaSubirRangoNucleo_usernameMiembroFK NOT NULL,
	estado NVARCHAR(50),

	CONSTRAINT FK_MiembroSolicitaSubirRangoNucleo_Miembro_Nucleo FOREIGN KEY (usernameNucleoFK) REFERENCES Miembro(usernamePK)
		ON DELETE NO ACTION --Si se elimina al miembro núcleo, no se quiere que este miembro se tome en cuenta para la decisión de la solicitud.
		ON UPDATE NO ACTION,
	CONSTRAINT FK_MiembroSolicitaSubirRangoNucleo_Miembro FOREIGN KEY (usernameMiembroFK) REFERENCES Miembro(usernamePK)
		ON DELETE NO ACTION --Si se elimina al miembro que solicita el ascenso, se quieren eliminar todos los registros de aprobaciones y rechazos que hay almacenados.
		ON UPDATE NO ACTION,
	CONSTRAINT CK_MiembroSolicitaSubirRangoNucleo_estado CHECK (estado in ('Aceptado','Rechazado')),
);