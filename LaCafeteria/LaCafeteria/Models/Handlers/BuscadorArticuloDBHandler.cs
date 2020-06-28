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
        public List<ArticuloModel> GetArticulosPorAutorYTipo(String autores, int tipos) {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd;

                switch ( tipos )
                {
                    case 1:
                        cmd = new SqlCommand("SELECT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.articuloAID = MAA.idArticuloFK " +
                        " JOIN Miembro M " +
                            " ON MAA.usernameMiemFK = M.usernamePK " +
                        " WHERE M.nombre +' '+ M.apellido1 +' '+ M.apellido2 LIKE @autor " +
                            " AND tipo = 'Corto' " +
                            " AND A.estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.articuloAID = MAA.idArticuloFK " +
                        " JOIN Miembro M " +
                            " ON MAA.usernameMiemFK = M.usernamePK " +
                        " WHERE M.nombre + M.apellido1 + M.apellido2 LIKE @autor " +
                            " AND tipo = 'Largo' " +
                            " AND A.estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    case 3:
                        cmd = new SqlCommand("SELECT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.articuloAID = MAA.idArticuloFK " +
                        " JOIN Miembro M " +
                            " ON MAA.usernameMiemFK = M.usernamePK " +
                        " WHERE M.nombre + M.apellido1 + M.apellido2 LIKE @autor " +
                            " AND tipo = 'Link' " +
                            " AND A.estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.articuloAID = MAA.idArticuloFK " +
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
                        articuloAID = (int) reader["articuloAID"],
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

        public List<ArticuloModel> GetArticulosPorMiembro(string username) {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A " +
                        " JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.articuloAID = MAA.idArticuloFK " +
                        " WHERE usernameMiemFK = @username;", connection);

                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while ( reader.Read() )
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        articuloAID = (int) reader["articuloAID"],
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

        public List<ArticuloModel> GetArticulosPorEstado(string estadoArticulo) {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT articuloAID, titulo, tipo, estado" +
                        " FROM Articulo WHERE estado = @estadoArticulo", connection);
                cmd.Parameters.AddWithValue("@estadoArticulo", estadoArticulo);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while ( reader.Read() )
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        articuloAID = (int) reader["articuloAID"],
                        titulo = (String) reader["titulo"],
                        tipo = (String) reader["tipo"],
                        estado = (String) reader["estado"],
                    };
                    artList.Add(articuloActual);
                }

                reader.Close();

                return artList;
            }
        }

        public List<ArticuloModel> GetArticulosPorEstadoAsignaMiem(string estadoArticulo, string username)
        {
            String connectionString = AppSettings.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT articuloAID, titulo, tipo, estado" +
                        " FROM Articulo JOIN NucleoRevisaArticulo ON idArticuloFK = articuloAID " +
                        "WHERE estado = @estadoArticulo AND @username = usernameMiemFK", connection);
                cmd.Parameters.AddWithValue("@estadoArticulo", estadoArticulo);
                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while (reader.Read())
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        articuloAID = (int)reader["articuloAID"],
                        titulo = (String)reader["titulo"],
                        tipo = (String)reader["tipo"],
                        estado = (String)reader["estado"],
                    };
                    artList.Add(articuloActual);
                }

                reader.Close();

                return artList;
            }
        }

        public List<ArticuloModel> GetArticulosPorTitulo(String titulo, int tipos) {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd;

                switch ( tipos )
                {
                    case 1:
                        cmd = new SqlCommand("SELECT  * " +
                        " FROM  Articulo " +
                        " WHERE titulo LIKE @titulo " +
                            " AND tipo = 'Corto' " +
                            " AND estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT  * " +
                        " FROM  Articulo " +
                        " WHERE titulo LIKE @titulo " +
                            " AND tipo = 'Largo' " +
                            " AND estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    case 3:
                        cmd = new SqlCommand("SELECT  * " +
                        " FROM  Articulo " +
                        " WHERE titulo LIKE @titulo " +
                            " AND tipo = 'Link' " +
                            " AND estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT  * " +
                        " FROM  Articulo " +
                        " WHERE titulo LIKE @titulo " +
                            " AND estado = 'Publicado' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                }

                cmd.Parameters.AddWithValue("@titulo", "%" + titulo + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while ( reader.Read() )
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        articuloAID = (int) reader["articuloAID"],
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

        public List<ArticuloModel> GetArticulosPorTopico(String topico, int tipos) {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd;

                switch ( tipos )
                {
                    case 1:
                        cmd = new SqlCommand("SELECT  DISTINCT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN ArticuloTrataTopico ATT " +
                            " ON A.articuloAID = ATT.idArticuloFK " +
                        " JOIN Topico T " +
                            " ON ATT.nombreTopicoFK = @topico " +
                        " WHERE A.tipo = 'Corto' " +
                            " AND A.estado = 'Publicado' " +
                        " ORDER BY A.fechaPublicacion DESC;", connection);
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT  DISTINCT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN ArticuloTrataTopico ATT " +
                            " ON A.articuloAID = ATT.idArticuloFK " +
                        " JOIN Topico T " +
                            " ON ATT.nombreTopicoFK = @topico " +
                        " WHERE A.tipo = 'Largo' " +
                            " AND A.estado = 'Publicado' " +
                        " ORDER BY A.fechaPublicacion DESC;", connection);
                        break;
                    case 3:
                        cmd = new SqlCommand("SELECT  DISTINCT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN ArticuloTrataTopico ATT " +
                            " ON A.articuloAID = ATT.idArticuloFK " +
                        " JOIN Topico T " +
                            " ON ATT.nombreTopicoFK = @topico " +
                        " WHERE A.tipo = 'Link' " +
                            " AND A.estado = 'Publicado' " +
                        " ORDER BY A.fechaPublicacion DESC;", connection);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT  DISTINCT A.articuloAID, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN ArticuloTrataTopico ATT " +
                            " ON A.articuloAID = ATT.idArticuloFK " +
                        " JOIN Topico T " +
                            " ON ATT.nombreTopicoFK = @topico " +
                        " WHERE A.estado = 'Publicado' " +
                        " ORDER BY A.fechaPublicacion DESC;", connection);
                        break;
                }

                cmd.Parameters.AddWithValue("@topico", topico);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while ( reader.Read() )
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        articuloAID = (int) reader["articuloAID"],
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

        public List<ArticuloModel> GetArticulosRevisionFinalizada() {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT DISTINCT A.articuloAID, A.titulo, A.tipo " +
                    "FROM Articulo A JOIN NucleoRevisaArticulo NRA " +
                    "ON A.articuloAID = NRA.idArticuloFK " +
                    "WHERE A.estado != 'Publicado' AND (SELECT COUNT(*) FROM NucleoRevisaArticulo NRA WHERE NRA.estadoRevision = 'Finalizada'  " +
                    "AND A.articuloAID = NRA.idArticuloFK) = " +
                    "(SELECT COUNT(*) FROM NucleoRevisaArticulo NRA " +
                    "WHERE A.articuloAID = NRA.idArticuloFK)", connection);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while ( reader.Read() )
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        articuloAID = (int) reader["articuloAID"],
                        titulo = (String) reader["titulo"],
                        tipo = (String) reader["tipo"]
                    };
                    artList.Add(articuloActual);
                }

                reader.Close();

                return artList;
            }
        }

        public List<ArticuloModel> GetTodosArticulos() {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT articuloAID, titulo, tipo, fechaPublicacion, resumen, contenido, estado, visitas, puntajeTotalRev, calificacionTotalMiem " +
                        " FROM  Articulo" +
                        " WHERE estado = 'Publicado';", connection);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while ( reader.Read() )
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        articuloAID = (int) reader["articuloAID"],
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
