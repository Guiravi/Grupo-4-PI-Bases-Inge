INSERT INTO [dbo].[Articulo] (titulo, tipo, fechaPublicacion, resumen, contenido, estado)
VALUES ('Artículo Con Todos Revisados', 'Corto', '2019-08-14','Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.', 'En Revisión');

INSERT INTO [dbo].[ArticuloTrataTopico]
VALUES ('Biología', 4);

INSERT INTO [dbo].[ArticuloTrataTopico]
VALUES ('Ciencias Sociales', 4);

INSERT INTO [dbo].[MiembroAutorDeArticulo]
VALUES ('BadBunny', 4);

INSERT INTO [dbo].[MiembroAutorDeArticulo]
VALUES ('alacer9', 4);

INSERT INTO [dbo].[NucleoRevisaArticulo]
VALUES ('nleate8', 4, 'Finalizado', 13, 'Bien perro, siga adelante!')

INSERT INTO [dbo].[NucleoRevisaArticulo]
VALUES ('zsallows0', 4, 'Finalizado', 15, 'Exceletente servicio!')

INSERT INTO [dbo].[NucleoRevisaArticulo]
VALUES ('auphilln', 4, 'Finalizado', 10.5, 'Pudo estar mejor, dislike :(')

INSERT INTO [dbo].[Articulo] (titulo, tipo, fechaPublicacion, resumen, contenido, estado)
VALUES ('Artículo Que le Faltan Revisores', 'Corto', '2019-07-10','Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.', 'En Revisión');

INSERT INTO [dbo].[ArticuloTrataTopico]
VALUES ('Física', 5);

INSERT INTO [dbo].[MiembroAutorDeArticulo]
VALUES ('zburnanda', 5);

INSERT INTO [dbo].[MiembroAutorDeArticulo]
VALUES ('sderricoi', 5);

INSERT INTO [dbo].[NucleoRevisaArticulo]
VALUES ('nleate8', 5, 'Finalizado', 12.5, 'Podría mejorar con ciertas mejoras que mejoran el producto mejorado')

INSERT INTO [dbo].[NucleoRevisaArticulo]
VALUES ('zsallows0', 5, 'Finalizado', 10, 'No me dice mucho sobre el tema')

INSERT INTO [dbo].[NucleoRevisaArticulo]
VALUES ('auphilln', 5, 'Revisando', null, null)