﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class EditorArticuloDBHandler
    {
        public void ActualizarEstadoArticulo(int id, string estadoArticulo) {
            string connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string cmdString = "UPDATE Articulo SET Articulo.estado = @estadoArticulo WHERE Articulo.articuloAID = @id";
                SqlCommand command = new SqlCommand(cmdString, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@estadoArticulo", estadoArticulo);
                command.ExecuteNonQuery();
            }

        }

        public void AgregarVisita(int id) {
            string connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string cmdString = "UPDATE Articulo SET Articulo.visitas = Articulo.visitas + 1 WHERE Articulo.articuloAID = @id AND Articulo.estado = 'Publicado'";
                SqlCommand command = new SqlCommand(cmdString, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public void EditarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<CategoriaTopicoModel> listaCategoriaTopico) {
            string connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection sqlConnection = new SqlConnection(connectionString) )
            {
                //Guardar registro del articulo en Articulo
                String sqlString = @"UPDATE Articulo
									SET	titulo = @titulo, 
										tipo = @tipo, 
										fechaPublicacion = @fechaPublicacion, 
										resumen = @resumen, 
										contenido = @contenido, 
										estado = @estado
                                    WHERE articuloAID = @idArticuloPK";

                sqlConnection.Open();
                using ( SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection) )
                {
                    sqlCommand.Parameters.AddWithValue("@idArticuloPK", articulo.articuloAID);
                    sqlCommand.Parameters.AddWithValue("@titulo", articulo.titulo);
                    sqlCommand.Parameters.AddWithValue("@tipo", articulo.tipo);
                    sqlCommand.Parameters.AddWithValue("@fechaPublicacion", articulo.fechaPublicacion);
                    sqlCommand.Parameters.AddWithValue("@resumen", articulo.resumen);
                    sqlCommand.Parameters.AddWithValue("@contenido", articulo.contenido);
                    sqlCommand.Parameters.AddWithValue("@estado", articulo.estado);

                    sqlCommand.ExecuteNonQuery();


                    //Borrar los registros de relacion de MiembrosAutores con su Articulo, ya que puedieron haber agregado nuevos autores o eliminado otros
                    sqlCommand.CommandText = "DELETE FROM MiembroAutorDeArticulo WHERE idArticuloFK = @idArticuloFK";

                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.articuloAID);
                    sqlCommand.ExecuteNonQuery();

                    //Guardar registros de relacion de MiembrosAutores con su Articulo
                    sqlCommand.CommandText = "INSERT INTO MiembroAutorDeArticulo VALUES(@usernameMiemFK, @idArticuloFK)";

                    foreach ( string usernamePK in usernamePKMiembrosAutores )
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("@usernameMiemFK", usernamePK);
                        sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.articuloAID);
                        sqlCommand.ExecuteNonQuery();
                    }

                    //Borrar los registros de relacion del Articulo con sus Tópicos, ya que puedieron haber agregado nuevos tópicos o eliminado otros
                    sqlCommand.CommandText = "DELETE FROM ArticuloTrataTopico WHERE idArticuloFK = @idArticuloFK";

                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.articuloAID);
                    sqlCommand.ExecuteNonQuery();

                    //Guardar registro de relaciones de Articulo con sus Topicos
                    sqlCommand.CommandText = "INSERT INTO ArticuloTrataTopico VALUES(@nombreCategoriaFK, @nombreTopicoFK, @idArticuloFK)";

                    foreach ( CategoriaTopicoModel categoriaTopico in listaCategoriaTopico )
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("@nombreCategoriaFK", categoriaTopico.nombreCategoriaPK);
                        sqlCommand.Parameters.AddWithValue("@nombreTopicoFK", categoriaTopico.nombreTopicoPK);
                        sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.articuloAID);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
