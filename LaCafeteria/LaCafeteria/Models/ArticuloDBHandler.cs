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


        public void ActualizarEstadoArticulo(int id, string estadoArticulo)
        {
            string connectionString = AppSettings.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string cmdString = "UPDATE Articulo SET Articulo.estado = @estadoArticulo WHERE Articulo.idArticuloPK = @id";
                SqlCommand command = new SqlCommand(cmdString, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@estadoArticulo", estadoArticulo);
                command.ExecuteNonQuery();
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



    }
}