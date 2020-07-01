using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

                string tipoArt = "";
                    
                switch ( tipos )
                {
                    case 1:
                        tipoArt = "Corto";
                        break;
                    case 2:
                        tipoArt = "Largo";
                        break;
                    case 3:
                        tipoArt = "Link";
                        break;
                    default:
                        tipoArt = "";
                    break;
                }

                cmd = new SqlCommand("USP_GetArticulosPorAutorYTipo", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@autores", "%" + autores + "%");
                cmd.Parameters.AddWithValue("@tipo", tipoArt);

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
                        " WHERE usernameMiemFK = @username " +
                        "ORDER BY A.fechaPublicacion DESC;", connection);

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

        public List<ArticuloModel> GetArticulosPorTituloYTipo(String titulo, int tipos) {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd;

                string tipoArt = "";

                switch (tipos)
                {
                    case 1:
                        tipoArt = "Corto";
                        break;
                    case 2:
                        tipoArt = "Largo";
                        break;
                    case 3:
                        tipoArt = "Link";
                        break;
                    default:
                        tipoArt = "";
                        break;
                }

                cmd = new SqlCommand("USP_GetArticulosPorTituloYTipo", connection);
                cmd.CommandType = CommandType.StoredProcedure;              
                cmd.Parameters.AddWithValue("@titulo", "%" + titulo + "%");
                cmd.Parameters.AddWithValue("@tipo", tipoArt);

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

        public List<ArticuloModel> GetArticulosPorTopicoYTipo(CategoriaTopicoModel catTop, int tipos) {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd;

                string tipoArt = "";

                switch (tipos)
                {
                    case 1:
                        tipoArt = "Corto";
                        break;
                    case 2:
                        tipoArt = "Largo";
                        break;
                    case 3:
                        tipoArt = "Link";
                        break;
                    default:
                        tipoArt = "";
                        break;
                }

                cmd = new SqlCommand("USP_GetArticulosPorTopicoYTipo", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoria", catTop.nombreCategoriaPK);
                cmd.Parameters.AddWithValue("@topico", catTop.nombreTopicoPK);
                cmd.Parameters.AddWithValue("@tipo", tipoArt);

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
