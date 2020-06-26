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

		

        
              

        

        

        

       



    }
}