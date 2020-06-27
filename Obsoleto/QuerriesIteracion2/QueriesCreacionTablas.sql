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
	CONSTRAINT CK_Articulo_estado CHECK (estado in ('En Progreso','Requiere Revisión', 'En Revisión', 'Rechazado', 'Aceptado con Correcciones', 'Publicado'))
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
	nombreRolFK NVARCHAR(25) CONSTRAINT DF_Miembro_nombreRolFK DEFAULT 'Periférico' CONSTRAINT NN_Miembro_nombreRolFK NOT NULL,

	CONSTRAINT PK_Autor PRIMARY KEY (usernamePK),
	CONSTRAINT UQ_Autor_email UNIQUE (email),
	CONSTRAINT FK_Miembro_Rol FOREIGN KEY (nombreRolFK) REFERENCES ROL(nombrePK)
		ON DELETE NO ACTION --No se quieren tener en la base de datos usuarios asignados a un rol que no existe, y si se quiere 
					--eliminar un rol se deberían eliminar o cambiar todos los usuarios asignados a ese rol primero.
		ON UPDATE CASCADE
);

CREATE TABLE MiembroAutorDeArticulo 
(
	usernameMiemFK NVARCHAR(20) CONSTRAINT NN_MiembroAutorDeArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_MiembroAutorDeArticulo_idArticuloFK NOT NULL,

	CONSTRAINT PK_MiembroAutorDeArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_MiembroAutorDeArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE NO ACTION  --Se quiere mantener un historial de los autores de este artículo aunque ya estos no sean 
					-- miembros activos de la comunidad.
		ON UPDATE CASCADE,
	CONSTRAINT FK_MiembroAutorDeArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE --Si un autor elimina el artículo se quiere eliminar la relación que conecta al autor con este artículo
					--para que este ya no esté asociado a un artículo inexistente.
		ON UPDATE CASCADE
);

CREATE TABLE MiembroCalificaArticulo
(
	usernameMiemFK NVARCHAR(20) CONSTRAINT NN_MiembroCalificaArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_MiembroCalificaArticulo_idArticuloFK NOT NULL,
	calificacion INTEGER CONSTRAINT NN_MiembroCalificaArticulo_calificacion NOT NULL,

	CONSTRAINT PK_MiembroCalificaArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_MiembroCalificaArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE NO ACTION --Se quiere mantener el registro de que este usuario calificó este artículo para evitar de que si el
					--usuario vuelve a la comunidad que este no pueda volver a calificar estos artículos.
		ON UPDATE CASCADE,
	CONSTRAINT FK_MiembroCalificaArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE --Si eliminan el artículo no se quiere mantener un registro de calificaciones para un artículo que no existe.
		ON UPDATE CASCADE
);

CREATE TABLE NucleoInteresaRevisarArticulo
(
	usernameMiemFK NVARCHAR(20) CONSTRAINT NN_NucleoInteresaRevisarArticulo_usernameMiemFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_NucleoInteresaRevisarArticulo_idArticuloFK NOT NULL,

	CONSTRAINT PK_NucleoInteresaRevisarArticulo PRIMARY KEY (usernameMiemFK, idArticuloFK),
	CONSTRAINT FK_NucleoInteresaRevisarArticulo_Miembro FOREIGN KEY (usernameMiemFK) REFERENCES Miembro(usernamePK)
		ON DELETE CASCADE --Si el usuario revisor se va ya no se desea mantener un registro de que este usuario quería calificar
					--este artículo.
		ON UPDATE CASCADE,
	CONSTRAINT FK_NucleoInteresaRevisarArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE --Si eliminan el artículo se quieren eliminar todos los registros de interesados a este artículo inexistente.
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
		ON DELETE NO ACTION --Se quiere mantener el registro del puntaje y comentarios dados por este revisor, además que esto 
					--influye en el cálculo de los méritos del autor.
		ON UPDATE CASCADE,
	CONSTRAINT FK_NucleoRevisaArticulo_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE --Se quieren borrar todos los registros de revisiones para los artículos que son borrados de la base de datos.
		ON UPDATE CASCADE
);

CREATE TABLE ArticuloTrataTopico
(
	nombreTopicoFK NVARCHAR(50) CONSTRAINT NN_ArticuloTrataTopico_nombreTopicoFK NOT NULL,
	idArticuloFK INTEGER CONSTRAINT NN_ArticuloTrataTopico_idArticuloFK NOT NULL,

	CONSTRAINT PK_ArticuloTrataTopico PRIMARY KEY(idArticuloFK, nombreTopicoFK),
	CONSTRAINT FK_ArticuloTrataTopico_Topico FOREIGN KEY (nombreTopicoFK) REFERENCES Topico(nombrePK)
		ON DELETE NO ACTION --Se tienen que borrar todos los artículos asociados al tópico antes de borrar el tópico,
					--para que en algunos casos no exista un artículo que no esté asociado a algún tópico.
		ON UPDATE CASCADE,
	CONSTRAINT FK_ArticuloTrataTopico_Articulo FOREIGN KEY (idArticuloFK) REFERENCES Articulo(idArticuloPK)
		ON DELETE CASCADE --Si se elimina el artículo se desea borrar todos los registros de que este artículo estaba asociado
					--a sus distintos tópicos.
		ON UPDATE CASCADE
);
