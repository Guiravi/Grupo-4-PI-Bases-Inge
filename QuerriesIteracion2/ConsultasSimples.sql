-- Consulta simple 1
-- Esta consulta recupera todos los atributos para cada uno de los miembros.
-- Se utiliza en la solución para la historia de prioridad 1 (Escribir articulo) para desplegar los miembros en la interfaz de usuario
-- y que el usuario pueda seleccionar autores del articulo.
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

-- Consulta simple 2
-- Esta consulta recupera la llave primaria (nombrePK) de todos los topicos.
-- Se utiliza para desplegar todos los topicos que existen en la interfaz de usuario para las historias de prioridad 1 (Escribir articulo)
-- y prioridad 7(Navegar y buscar)
SELECT Topico.nombrePK FROM Topico

-- Consulta simple 3
-- Esta consulta recupera todos los articulos de un miembro
-- Se utiliza para la solucion de la historia con prioridad 4 (Acceder y editar mis articulos). Los articulos del miembro se despliegan
-- en una interfaz para que este los pueda leer y editar.
SELECT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem  
FROM  Articulo A 
JOIN MiembroAutorDeArticulo MAA 
ON A.idArticuloPK = MAA.idArticuloFK 
WHERE usernameMiemFK = @username
