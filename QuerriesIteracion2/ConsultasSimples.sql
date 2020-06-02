/*Consulta simple 1*/
SELECT usernamePK
, email
, nombre
, apellido1
, apellido2
, fechaNacimiento
, pais
, estado
, ciudad
, rutaImagenPerfil
, hobbies
, habilidades
, idiomas
, informacionLaboral
, meritos
, activo
, nombreRolFK
FROM Miembro
/*Consulta simple 2*/
SELECT Topico.nombrePK FROM Topico
/*Consulta simple 3*/
SELECT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem  
FROM  Articulo A 
JOIN MiembroAutorDeArticulo MAA 
ON A.idArticuloPK = MAA.idArticuloFK 
WHERE usernameMiemFK = @username