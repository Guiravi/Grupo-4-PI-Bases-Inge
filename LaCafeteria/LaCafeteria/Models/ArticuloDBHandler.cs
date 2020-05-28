using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models
{
    public class ArticuloDBHandler
    {

        /*
        public void SaveArticulo(ArticuloModel articulo, List<TopicoModel> topicos)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sqlString = "INSERT INTO Articulo(titulo, resumen, tipo, contenido, fechaPublicacion, nombreAutor, usernameFK)";
                sqlString += "VALUES(@titulo, @resumen, @tipo, @contenido, @fechaPublicacion, @nombreAutor, @usernameFK)";

                using (SqlCommand command = new SqlCommand(sqlString, connection))
                {
                    command.Parameters.AddWithValue("@titulo", articulo.titulo);
                    command.Parameters.AddWithValue("@resumen", articulo.resumen);
                    command.Parameters.AddWithValue("@tipo", articulo.tipo);
                    command.Parameters.AddWithValue("@contenido", articulo.contenido);
                    command.Parameters.AddWithValue("@fechaPublicacion", articulo.fechaPublicacion);
                    command.Parameters.AddWithValue("@nombreAutor", articulo.nombreAutor);
                    command.Parameters.AddWithValue("@usernameFK", articulo.usernameFK);

                    connection.Open();
                    command.ExecuteNonQuery();

                    articulo.idArticuloPK = ObtenerSiguienteId();
                    command.CommandText = "INSERT INTO TopicosArticulo VALUES(@idArticuloFK, @nombreTopicoFK)";

                    foreach (TopicoModel topico in topicos)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@idArticuloFK", articulo.idArticuloPK);
                        command.Parameters.AddWithValue("@nombreTopicoFK", topico.nombre);
                        command.ExecuteNonQuery();
                    }
                }

            }
        }


        public void UpdateArticulo(ArticuloModel articulo, List<TopicoModel> topicos)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                String sqlString = "UPDATE Articulo SET titulo = @titulo, resumen = @resumen, tipo = @tipo , contenido = @contenido, fechaPublicacion = @fechaPublicacion, nombreAutor = @nombreAutor, usernameFK = @usernameFK";
                sqlString += " WHERE idArticuloPK = @idArticuloPK";

                using (SqlCommand command = new SqlCommand(sqlString, connection))
                {
                    command.Parameters.AddWithValue("@idArticuloPK", articulo.idArticuloPK);
                    command.Parameters.AddWithValue("@titulo", articulo.titulo);
                    command.Parameters.AddWithValue("@resumen", articulo.resumen);
                    command.Parameters.AddWithValue("@tipo", articulo.tipo);
                    command.Parameters.AddWithValue("@contenido", articulo.contenido);
                    command.Parameters.AddWithValue("@fechaPublicacion", articulo.fechaPublicacion);
                    command.Parameters.AddWithValue("@nombreAutor", articulo.nombreAutor);
                    command.Parameters.AddWithValue("@usernameFK", articulo.usernameFK);

                    connection.Open();
                    command.ExecuteNonQuery();

                    articulo.idArticuloPK = ObtenerSiguienteId();



                    foreach (TopicoModel topico in topicos)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@idArticuloFK", articulo.idArticuloPK);
                        command.Parameters.AddWithValue("@nombreTopicoFK", topico.nombre);
                        command.CommandText = "DELETE FROM TopicosArticulo WHERE nombreTopicoFK= @nombreTopicoFK AND idArticuloFK = @idArticuloFK";
                        command.ExecuteNonQuery();
                        command.CommandText = "INSERT INTO TopicosArticulo VALUES(@idArticuloFK, @nombreTopicoFK)";
                        command.ExecuteNonQuery();
                    }
                }

            }
        }
        public int ObtenerSiguienteId()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;
            SqlCommand cmd;
            int current_id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                cmd = new SqlCommand("SELECT IDENT_CURRENT ('Articulo')", connection);

                SqlDataReader identReader = cmd.ExecuteReader();

                current_id = 0;

                while (identReader.Read())
                {
                    current_id = Convert.ToInt32(identReader.GetValue(0));
                }

                identReader.Close();

            }

            return current_id;
        }

        public byte[] DescargarArticuloDocx(int idArticulo)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;
            SqlCommand cmd;
            string contenido = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                cmd = new SqlCommand("SELECT contenido FROM [dbo].[Articulo] WHERE idArticuloPK = @idArticulo", connection);

                cmd.Parameters.AddWithValue("@idArticulo", idArticulo);

                SqlDataReader identReader = cmd.ExecuteReader();

                while (identReader.Read())
                {
                    contenido = Convert.ToString(identReader.GetValue(0));
                }

                identReader.Close();

            }

            return System.Convert.FromBase64String(contenido);
        }

       */

        public List<ArticuloModel> GetArticulosPorAutor(String autores, int tipos)
        {
            String connectionString = AppSettings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd;

                switch (tipos)
                {
                    case 1:
                        cmd = new SqlCommand("SELECT  idArticuloPK, titulo, resumen, tipo, contenido, fechaPublicacion, nombreAutor, usernameFK " +
                        " FROM  Articulo  WHERE nombreAutor LIKE @autor AND tipo = 0 ORDER BY fechaPublicacion DESC", connection);
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT  idArticuloPK, titulo, resumen, tipo, contenido, fechaPublicacion, nombreAutor, usernameFK " +
                       " FROM  Articulo  WHERE nombreAutor LIKE @autor AND tipo = 1 ORDER BY fechaPublicacion DESC", connection);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT  idArticuloPK, titulo, resumen, tipo, contenido, fechaPublicacion, nombreAutor, usernameFK " +
                       " FROM  Articulo  WHERE nombreAutor LIKE @autor ORDER BY fechaPublicacion DESC", connection);
                        break;
                }

                cmd.Parameters.AddWithValue("@autor", "%" + autores + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while (reader.Read())
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        idArticuloPK = (int)reader["idArticuloPK"],
                        titulo = (String)reader["titulo"],
                        resumen = (String)reader["resumen"],
                        tipo = (int)reader["tipo"], //Cambiar por string en BD
                        contenido = (String)reader["contenido"],
                        fechaPublicacion = reader["fechaPublicacion"].ToString().Remove(reader["fechaPublicacion"].ToString().Length - 12, 12),
                        nombreAutor = (String)reader["nombreAutor"], //Quitar
                        usernameFK = (String)reader["usernameFK"]
                    };

                    artList.Add(articuloActual);
                }

                reader.Close();

                return artList;
            }

        }

        public List<ArticuloModel> GetArticulosPorTitulo(String titulo, int tipos)
        {
            String connectionString = AppSettings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd;

                switch (tipos)
                {
                    case 1:
                        cmd = new SqlCommand("SELECT  idArticuloPK, titulo, resumen, tipo, contenido, fechaPublicacion, nombreAutor, usernameFK " +
                        " FROM  Articulo  WHERE titulo LIKE @titulo AND tipo = 0 ORDER BY fechaPublicacion DESC", connection);
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT  idArticuloPK, titulo, resumen, tipo, contenido, fechaPublicacion, nombreAutor, usernameFK " +
                       " FROM  Articulo  WHERE titulo LIKE @titulo AND tipo = 1 ORDER BY fechaPublicacion DESC", connection);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT  idArticuloPK, titulo, resumen, tipo, contenido, fechaPublicacion, nombreAutor, usernameFK " +
                       " FROM  Articulo  WHERE titulo LIKE @titulo ORDER BY fechaPublicacion DESC", connection);
                        break;
                }

                cmd.Parameters.AddWithValue("@titulo", "%" + titulo + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while (reader.Read())
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        idArticuloPK = (int)reader["idArticuloPK"],
                        titulo = (String)reader["titulo"],
                        resumen = (String)reader["resumen"],
                        tipo = (int)reader["tipo"], //Cambiar por string en BD
                        contenido = (String)reader["contenido"],
                        fechaPublicacion = reader["fechaPublicacion"].ToString().Remove(reader["fechaPublicacion"].ToString().Length - 12, 12),
                        nombreAutor = (String)reader["nombreAutor"], //Quitar
                        usernameFK = (String)reader["usernameFK"]
                    };

                    artList.Add(articuloActual);
                }

                reader.Close();

                return artList;
            }
        }

        public List<ArticuloModel> GetArticulosPorTopico(String topico, int tipos)
        {
            String connectionString = AppSettings.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd;

                switch (tipos)
                {
                    case 1:
                        cmd = new SqlCommand("SELECT  DISTINCT Articulo.idArticuloPK, Articulo.titulo, Articulo.resumen, Articulo.tipo, Articulo.contenido, Articulo.fechaPublicacion, Articulo.nombreAutor, Articulo.usernameFK " +
                        " FROM  Articulo JOIN TopicosArticulo ON " +
                        " Articulo.idArticuloPK = TopicosArticulo.idArticuloFK JOIN Topico ON " +
                        " TopicosArticulo.nombreTopicoFK = @topico AND Articulo.tipo = 0 ORDER BY fechaPublicacion DESC", connection);
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT  DISTINCT Articulo.idArticuloPK, Articulo.titulo, Articulo.resumen, Articulo.tipo, Articulo.contenido, Articulo.fechaPublicacion, Articulo.nombreAutor, Articulo.usernameFK " +
                        " FROM  Articulo JOIN TopicosArticulo ON " +
                        " Articulo.idArticuloPK = TopicosArticulo.idArticuloFK JOIN Topico ON " +
                        " TopicosArticulo.nombreTopicoFK = @topico AND Articulo.tipo = 1 ORDER BY fechaPublicacion DESC", connection);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT  DISTINCT Articulo.idArticuloPK, Articulo.titulo, Articulo.resumen, Articulo.tipo, Articulo.contenido, Articulo.fechaPublicacion, Articulo.nombreAutor, Articulo.usernameFK " +
                        " FROM  Articulo JOIN TopicosArticulo ON " +
                        " Articulo.idArticuloPK = TopicosArticulo.idArticuloFK JOIN Topico ON " +
                        " TopicosArticulo.nombreTopicoFK = @topico ORDER BY fechaPublicacion DESC", connection);
                        break;
                }


                cmd.Parameters.AddWithValue("@topico", topico);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while (reader.Read())
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        idArticuloPK = (int)reader["idArticuloPK"],
                        titulo = (String)reader["titulo"],
                        resumen = (String)reader["resumen"],
                        tipo = (int)reader["tipo"], //Cambiar por string en BD
                        contenido = (String)reader["contenido"],
                        fechaPublicacion = reader["fechaPublicacion"].ToString().Remove(reader["fechaPublicacion"].ToString().Length - 12, 12),
                        nombreAutor = (String)reader["nombreAutor"], //Quitar
                        usernameFK = (String)reader["usernameFK"]
                    };

                    artList.Add(articuloActual);
                }

                reader.Close();

                return artList;
            }
        }

        /**

        public string GetResumenDB(int idArticuloPK)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd;
                cmd = new SqlCommand("SELECT resumen FROM Articulo WHERE idArticuloPK = @idArticuloPK", connection);


                cmd.Parameters.AddWithValue("@idArticuloPK", idArticuloPK);

                SqlDataReader identReader = cmd.ExecuteReader();
                string resumen = "";
                while (identReader.Read())
                {
                    resumen = (string)identReader.GetValue(0);
                }

                identReader.Close();
                return resumen;
            }
        }
        public string GetAutoresDB(int idArticuloPK)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd;
                cmd = new SqlCommand("SELECT nombreAutor FROM Articulo WHERE idArticuloPK = @idArticuloPK", connection);


                cmd.Parameters.AddWithValue("@idArticuloPK", idArticuloPK);

                SqlDataReader identReader = cmd.ExecuteReader();
                string autores = "";
                while (identReader.Read())
                {
                    autores = (string)identReader.GetValue(0);
                }

                identReader.Close();
                return autores;
            }
        }
        public string GetTituloDB(int idArticuloPK)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd;
                cmd = new SqlCommand("SELECT titulo FROM Articulo WHERE idArticuloPK = @idArticuloPK", connection);


                cmd.Parameters.AddWithValue("@idArticuloPK", idArticuloPK);

                SqlDataReader identReader = cmd.ExecuteReader();
                string titulo = "";
                while (identReader.Read())
                {
                    titulo = (string)identReader.GetValue(0);
                }

                identReader.Close();
                return titulo;
            }
        }
        public string GetContenidoCortoDB(int idArticuloPK)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd;
                cmd = new SqlCommand("SELECT contenido FROM Articulo WHERE idArticuloPK = @idArticuloPK", connection);


                cmd.Parameters.AddWithValue("@idArticuloPK", idArticuloPK);

                SqlDataReader identReader = cmd.ExecuteReader();
                string contenido = "";
                while (identReader.Read())
                {
                    contenido = (string)identReader.GetValue(0);
                }

                identReader.Close();
                return contenido;
            }
        }
        

        public List<ArticuloModel> GetArticlesByAutorEdit(String autor)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd;

                cmd = new SqlCommand("SELECT  DISTINCT Articulo.idArticuloPK, Articulo.titulo, Articulo.resumen, Articulo.tipo, Articulo.contenido, Articulo.fechaPublicacion, Articulo.nombreAutor, Articulo.usernameFK " +
                       " FROM  Articulo  WHERE usernameFK LIKE @autor ORDER BY Articulo.titulo ", connection);
                cmd.Parameters.AddWithValue("@autor", autor);
                SqlDataReader identReader = cmd.ExecuteReader();

                List<ArticuloModel> art = new List<ArticuloModel>();
                while (identReader.Read())
                {
                    ArticuloModel am = new ArticuloModel((int)identReader.GetValue(0),
                        (String)identReader.GetValue(1), (String)identReader.GetValue(2),
                        (int)identReader.GetValue(3), (String)identReader.GetValue(4),
                        identReader.GetValue(5).ToString().Remove(identReader.GetValue(5).ToString().Length - 12, 12),
                        (String)identReader.GetValue(6), (String)identReader.GetValue(7));
                    art.Add(am);
                }

                identReader.Close();

                return art;
            }
        }
       

        public List<ArticuloModel> GetTodosArticulos()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT  idArticuloPK, titulo, resumen, tipo, contenido, fechaPublicacion, nombreAutor, usernameFK " +
                        " FROM  Articulo ORDER BY fechaPublicacion DESC", connection);

                SqlDataReader identReader = cmd.ExecuteReader();

                List<ArticuloModel> art = new List<ArticuloModel>();
                while (identReader.Read())
                {
                    ArticuloModel am = new ArticuloModel((int)identReader.GetValue(0),
                        (String)identReader.GetValue(1), (String)identReader.GetValue(2),
                        (int)identReader.GetValue(3), (String)identReader.GetValue(4),
                        identReader.GetValue(5).ToString().Remove(identReader.GetValue(5).ToString().Length - 12, 12),
                        (String)identReader.GetValue(6), (String)identReader.GetValue(7));
                    art.Add(am);
                }

                identReader.Close();

                return art;
            }
        }

        public ArticuloModel GetInfoPaginaResumen(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;

            ArticuloModel articulo = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Articulo WHERE Articulo.idArticuloPK = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader identReader = cmd.ExecuteReader();

                while (identReader.Read())
                {
                    articulo = new ArticuloModel((string)identReader["titulo"], (string)identReader["resumen"], (int)identReader["tipo"],
                        (string)identReader["contenido"],
                        identReader["fechaPublicacion"].ToString().Remove(identReader["fechaPublicacion"].ToString().Length - 12, 12),
                        (string)identReader["nombreAutor"], (string)identReader["usernameFK"]);
                }

                identReader.Close();
            }
            return articulo;
        }

        */
    }
}