using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class BuscadorArticuloDBHandler
    {
        public List<ArticuloModel> GetArticulosPorAutor(String autores, int tipos) {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd;

                switch ( tipos )
                {
                    case 1:
                        cmd = new SqlCommand("SELECT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.idArticuloPK = MAA.idArticuloFK " +
                        " JOIN Miembro M " +
                            " ON MAA.usernameMiemFK = M.usernamePK " +
                        " WHERE M.nombre +' '+ M.apellido1 +' '+ M.apellido2 LIKE @autor " +
                            " AND tipo = 'Corto' " +
                            " AND A.estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.idArticuloPK = MAA.idArticuloFK " +
                        " JOIN Miembro M " +
                            " ON MAA.usernameMiemFK = M.usernamePK " +
                        " WHERE M.nombre + M.apellido1 + M.apellido2 LIKE @autor " +
                            " AND tipo = 'Largo' " +
                            " AND A.estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    case 3:
                        cmd = new SqlCommand("SELECT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.idArticuloPK = MAA.idArticuloFK " +
                        " JOIN Miembro M " +
                            " ON MAA.usernameMiemFK = M.usernamePK " +
                        " WHERE M.nombre + M.apellido1 + M.apellido2 LIKE @autor " +
                            " AND tipo = 'Link' " +
                            " AND A.estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.idArticuloPK = MAA.idArticuloFK " +
                        " JOIN Miembro M " +
                            " ON MAA.usernameMiemFK = M.usernamePK " +
                        " WHERE M.nombre + M.apellido1 + M.apellido2 LIKE @autor " +
                            " AND A.estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                }

                cmd.Parameters.AddWithValue("@autor", "%" + autores + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while ( reader.Read() )
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        idArticuloPK = (int) reader["idArticuloPK"],
                        titulo = (String) reader["titulo"],
                        tipo = (String) reader["tipo"],
                        fechaPublicacion = reader["fechaPublicacion"].ToString().Remove(reader["fechaPublicacion"].ToString().Length - 12, 12),
                        resumen = (String) reader["resumen"],
                        contenido = (String) reader["contenido"],
                        estado = (String) reader["estado"],
                        visitas = (int) reader["visitas"],
                        puntajeTotalRev = (!DBNull.Value.Equals(reader["puntajeTotalRev"])) ? (double?) reader["puntajeTotalRev"] : null,
                        calificacionTotalMiem = (int) reader["calificacionTotalMiem"]
                    };

                    artList.Add(articuloActual);
                }

                reader.Close();

                return artList;
            }

        }
    }
}
