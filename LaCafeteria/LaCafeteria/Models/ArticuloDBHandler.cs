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

		public void GuardarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<string> nombreTopicoPKTopicos)
		{
			string connectionString = AppSettings.GetConnectionString();

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				//Guardar registro del articulo en Articulo
				String sqlString = @"INSERT INTO Articulo(
															titulo, 
															tipo, 
															fechaPublicacion, 
															resumen, 
															contenido, 
															estado 
														 )
									VALUES(@titulo, @tipo, @fechaPublicacion, @resumen, @contenido, @estado)";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@titulo", articulo.titulo);
					sqlCommand.Parameters.AddWithValue("@tipo", articulo.tipo);
					sqlCommand.Parameters.AddWithValue("@fechaPublicacion", articulo.fechaPublicacion);
					sqlCommand.Parameters.AddWithValue("@resumen", articulo.resumen);
					sqlCommand.Parameters.AddWithValue("@contenido", articulo.contenido);
					sqlCommand.Parameters.AddWithValue("@estado", articulo.estado);

					sqlCommand.ExecuteNonQuery();

					//Guardar registros de relacion de MiembrosAutores con su Articulo
					articulo.idArticuloPK = ObtenerSiguienteId();
					sqlCommand.CommandText = "INSERT INTO MiembroAutorDeArticulo VALUES(@usernameMiemFK, @idArticuloFK)";

					foreach (string usernamePK in usernamePKMiembrosAutores)
					{
						sqlCommand.Parameters.Clear();
						sqlCommand.Parameters.AddWithValue("@usernameMiemFK", usernamePK);
						sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.idArticuloPK);
						sqlCommand.ExecuteNonQuery();
					}

					//Guardar registro de relaciones de Articulo con sus Topicos

					articulo.idArticuloPK = ObtenerSiguienteId();
					sqlCommand.CommandText = "INSERT INTO ArticuloTrataTopico VALUES(@nombreTopicoFK, @idArticuloFK)";

					foreach (string nombreTopicoPK in nombreTopicoPKTopicos)
					{
						sqlCommand.Parameters.Clear();
						sqlCommand.Parameters.AddWithValue("@nombreTopicoFK", nombreTopicoPK);
						sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.idArticuloPK);
						sqlCommand.ExecuteNonQuery();
					}
				}
			}
		}

        public void EditarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<string> nombreTopicoPKTopicos)
		{
			string connectionString = AppSettings.GetConnectionString();

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				//Guardar registro del articulo en Articulo
				String sqlString = @"UPDATE Articulo
									SET	titulo = @titulo, 
										tipo = @tipo, 
										fechaPublicacion = @fechaPublicacion, 
										resumen = @resumen, 
										contenido = @contenido, 
										estado = @estado
                                    WHERE idArticuloPK = @idArticuloPK";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
                    sqlCommand.Parameters.AddWithValue("@idArticuloPK", articulo.idArticuloPK);
					sqlCommand.Parameters.AddWithValue("@titulo", articulo.titulo);
					sqlCommand.Parameters.AddWithValue("@tipo", articulo.tipo);
					sqlCommand.Parameters.AddWithValue("@fechaPublicacion", articulo.fechaPublicacion);
					sqlCommand.Parameters.AddWithValue("@resumen", articulo.resumen);
					sqlCommand.Parameters.AddWithValue("@contenido", articulo.contenido);
					sqlCommand.Parameters.AddWithValue("@estado", articulo.estado);

					sqlCommand.ExecuteNonQuery();


                    //Borrar los registros de relacion de MiembrosAutores con su Articulo, ya que puedieron haber agregado nuevos autores o eliminado otros
                    sqlCommand.CommandText = "DELETE FROM MiembroAutorDeArticulo WHERE idArticuloFK = @idArticuloFK";

                    foreach (string usernamePK in usernamePKMiembrosAutores)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.idArticuloPK);
                        sqlCommand.ExecuteNonQuery();
                    }

                    //Guardar registros de relacion de MiembrosAutores con su Articulo
                    sqlCommand.CommandText = "INSERT INTO MiembroAutorDeArticulo VALUES(@usernameMiemFK, @idArticuloFK)";

					foreach (string usernamePK in usernamePKMiembrosAutores)
					{
						sqlCommand.Parameters.Clear();
						sqlCommand.Parameters.AddWithValue("@usernameMiemFK", usernamePK);
						sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.idArticuloPK);
						sqlCommand.ExecuteNonQuery();
					}

                    //Borrar los registros de relacion del Articulo con sus Tópicos, ya que puedieron haber agregado nuevos tópicos o eliminado otros
                    sqlCommand.CommandText = "DELETE FROM ArticuloTrataTopico WHERE idArticuloFK = @idArticuloFK";

                    foreach (string nombreTopicoPK in nombreTopicoPKTopicos)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.idArticuloPK);
                        sqlCommand.ExecuteNonQuery();
                    }

                    //Guardar registro de relaciones de Articulo con sus Topicos
					sqlCommand.CommandText = "INSERT INTO ArticuloTrataTopico VALUES(@nombreTopicoFK, @idArticuloFK)";

					foreach (string nombreTopicoPK in nombreTopicoPKTopicos)
					{
						sqlCommand.Parameters.Clear();
						sqlCommand.Parameters.AddWithValue("@nombreTopicoFK", nombreTopicoPK);
						sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.idArticuloPK);
						sqlCommand.ExecuteNonQuery();
					}
				}
			}
		}

		public int ObtenerSiguienteId()
        {
            String connectionString = AppSettings.GetConnectionString();
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

        public List<ArticuloModel> GetMisArticulos(string username)
        {
            String connectionString = AppSettings.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A " +
                        " JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.idArticuloPK = MAA.idArticuloFK " +
                        " WHERE usernameMiemFK = @username;", connection);

                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while (reader.Read())
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        idArticuloPK = (int)reader["idArticuloPK"],
                        titulo = (String)reader["titulo"],
                        tipo = (String)reader["tipo"],
                        fechaPublicacion = reader["fechaPublicacion"].ToString().Remove(reader["fechaPublicacion"].ToString().Length - 12, 12),
                        resumen = (String)reader["resumen"],
                        contenido = (String)reader["contenido"],
                        estado = (String)reader["estado"],
                        visitas = (int)reader["visitas"],
                        puntajeTotalRev = (!DBNull.Value.Equals(reader["puntajeTotalRev"])) ? (double?)reader["puntajeTotalRev"] : null,
                        calificacionTotalMiem = (int)reader["calificacionTotalMiem"]
                    };

                    artList.Add(articuloActual);
                }

                reader.Close();

                return artList;
            }
        }
        

        /*
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
        */

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
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN MiembroAutorDeArticulo MAA " +
                            " ON A.idArticuloPK = MAA.idArticuloFK " +
                        " JOIN Miembro M " +
                            " ON MAA.usernameMiemFK = M.usernamePK " +
                        " WHERE M.nombre + M.apellido1 + M.apellido2 LIKE @autor " +
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
                        puntajeTotalRev = (!DBNull.Value.Equals(reader["puntajeTotalRev"])) ? (double?)reader["puntajeTotalRev"] : null,
						calificacionTotalMiem = (int) reader["calificacionTotalMiem"]
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
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT  * " +
                        " FROM  Articulo " +
                        " WHERE titulo LIKE @titulo " +
                            " AND tipo = 'Largo' " +
                        " ORDER BY fechaPublicacion DESC;", connection);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT  * " +
                        " FROM  Articulo " +
                        " WHERE titulo LIKE @titulo " +
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

        public List<ArticuloModel> GetArticulosPorTopico(String topico, int tipos) {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd;

                switch ( tipos )
                {
                    case 1:
                        cmd = new SqlCommand("SELECT  DISTINCT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN ArticuloTrataTopico ATT " +
                            " ON A.idArticuloPK = ATT.idArticuloFK " +
                        " JOIN Topico T " +
                            " ON ATT.nombreTopicoFK = @topico " +
                        " WHERE A.tipo = 'Corto' " +
                        " ORDER BY A.fechaPublicacion DESC;", connection);
                        break;
                    case 2:
                        cmd = new SqlCommand("SELECT  DISTINCT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN ArticuloTrataTopico ATT " +
                            " ON A.idArticuloPK = ATT.idArticuloFK " +
                        " JOIN Topico T " +
                            " ON ATT.nombreTopicoFK = @topico " +
                        " WHERE A.tipo = 'Largo' " +
                        " ORDER BY A.fechaPublicacion DESC;", connection);
                        break;
                    default:
                        cmd = new SqlCommand("SELECT  DISTINCT A.idArticuloPK, A.titulo, A.tipo, A.fechaPublicacion, A.resumen, A.contenido, A.estado, A.visitas, A.puntajeTotalRev, A.calificacionTotalMiem " +
                        " FROM  Articulo A JOIN ArticuloTrataTopico ATT " +
                            " ON A.idArticuloPK = ATT.idArticuloFK " +
                        " JOIN Topico T " +
                            " ON ATT.nombreTopicoFK = @topico " +
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
                        idArticuloPK = (int) reader["idArticuloPK"],
                        titulo = (String) reader["titulo"],
                        tipo = (String) reader["tipo"],
                        fechaPublicacion = reader["fechaPublicacion"].ToString().Remove(reader["fechaPublicacion"].ToString().Length - 12, 12),
                        resumen = (String) reader["resumen"],
                        contenido = (String) reader["contenido"],
                        estado = (String) reader["estado"],
                        visitas = (int) reader["visitas"],
                        puntajeTotalRev = (!DBNull.Value.Equals(reader["puntajeTotalRev"])) ? (double?)reader["puntajeTotalRev"] : null,
						calificacionTotalMiem = (int) reader["calificacionTotalMiem"]
                    };

                    artList.Add(articuloActual);
                }

                reader.Close();

                return artList;
            }
        }

        public List<ArticuloModel> GetTodosArticulos()
        {
            String connectionString = AppSettings.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT idArticuloPK, titulo, tipo, fechaPublicacion, resumen, contenido, estado, visitas, puntajeTotalRev, calificacionTotalMiem " +
                        " FROM  Articulo;",  connection);
              
                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while (reader.Read())
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        idArticuloPK = (int)reader["idArticuloPK"],
                        titulo = (String)reader["titulo"],
                        tipo = (String)reader["tipo"],
                        fechaPublicacion = reader["fechaPublicacion"].ToString().Remove(reader["fechaPublicacion"].ToString().Length - 12, 12),
                        resumen = (String)reader["resumen"],
                        contenido = (String)reader["contenido"],
                        estado = (String)reader["estado"],
                        visitas = (int)reader["visitas"],
                        puntajeTotalRev = (!DBNull.Value.Equals(reader["puntajeTotalRev"])) ? (double?)reader["puntajeTotalRev"] : null,
                        calificacionTotalMiem = (int)reader["calificacionTotalMiem"]
                    };

                    artList.Add(articuloActual);
                }

                reader.Close();

                return artList;
            }
        }

        public byte[] DescargarArticuloDocx(int idArticulo) {
            String connectionString = AppSettings.GetConnectionString();
            SqlCommand cmd;
            string contenido = "";

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {

                connection.Open();

                cmd = new SqlCommand("SELECT contenido " +
                    " FROM [dbo].[Articulo] " +
                    " WHERE idArticuloPK = @idArticulo;", connection);

                cmd.Parameters.AddWithValue("@idArticulo", idArticulo);

                SqlDataReader reader = cmd.ExecuteReader();

                while ( reader.Read() )
                {
                    contenido = Convert.ToString(reader.GetValue(0));
                }

                reader.Close();

            }

            return System.Convert.FromBase64String(contenido);
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
        */
        public ArticuloModel GetInfoPaginaResumen(int id) {
            string connectionString = AppSettings.GetConnectionString();

            ArticuloModel articulo = null;

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Articulo WHERE Articulo.idArticuloPK = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while ( reader.Read() )
                {
                    articulo = new ArticuloModel()
                    {
                        idArticuloPK = id,
                        titulo = (string) reader["titulo"],
                        tipo = (string) reader["tipo"],
                        fechaPublicacion = reader["fechaPublicacion"].ToString().Remove(reader["fechaPublicacion"].ToString().Length - 12, 12),
                        resumen = (string) reader["resumen"],
                        contenido = (string) reader["contenido"],
                        estado = (string) reader["estado"],
                        visitas = (int) reader["visitas"],
                        puntajeTotalRev = (!DBNull.Value.Equals(reader["puntajeTotalRev"])) ? (double?)reader["puntajeTotalRev"] : null,
                        calificacionTotalMiem = (int) reader["calificacionTotalMiem"]
                    };
                }

                reader.Close();
            }
            return articulo;
        }

        public List<MiembroModel> GetAutoresDeArticulo(int id) {
            List<MiembroModel> autores = new List<MiembroModel>();
            string connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string cmdString = "SELECT M.nombre, M.apellido1, M.apellido2 " +
                    " FROM Miembro M JOIN MiembroAutorDeArticulo MAA " +
                    " ON M.usernamePK = MAA.usernameMiemFK " +
                    " WHERE MAA.idArticuloFK = @id;";
                SqlCommand command = new SqlCommand(cmdString, connection);
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();
                while ( reader.Read() )
                {
                    MiembroModel autor = new MiembroModel()
                    {
                        nombre = (string) reader["nombre"],
                        apellido1 = (string) reader["apellido1"],
                        apellido2 = (string) reader["apellido2"],
                    };
                    autores.Add(autor);
                }

                reader.Close();
            }
            return autores;
        }

        public void AgregarVisita(int id) {
            string connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string cmdString = "UPDATE Articulo SET Articulo.visitas = Articulo.visitas + 1 WHERE Articulo.idArticuloPK = @id";
                SqlCommand command = new SqlCommand(cmdString, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public List<ArticuloModel> GetArticulosPorEstado(string estadoArticulo) {
            String connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT idArticuloPK, titulo, tipo, estado" +
                        " FROM Articulo WHERE estado = @estadoArticulo", connection);
                cmd.Parameters.AddWithValue("@estadoArticulo", estadoArticulo);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArticuloModel> artList = new List<ArticuloModel>();

                while ( reader.Read() )
                {
                    ArticuloModel articuloActual = new ArticuloModel()
                    {
                        idArticuloPK = (int) reader["idArticuloPK"],
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
    }
}