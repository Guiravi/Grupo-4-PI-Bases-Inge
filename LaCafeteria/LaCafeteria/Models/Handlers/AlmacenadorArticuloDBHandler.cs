﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class AlmacenadorArticuloDBHandler
    {
        public void GuardarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<string> nombreTopicoPKTopicos) {
            string connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection sqlConnection = new SqlConnection(connectionString) )
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
                using ( SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection) )
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

                    foreach ( string usernamePK in usernamePKMiembrosAutores )
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("@usernameMiemFK", usernamePK);
                        sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.idArticuloPK);
                        sqlCommand.ExecuteNonQuery();
                    }

                    //Guardar registro de relaciones de Articulo con sus Topicos

                    articulo.idArticuloPK = ObtenerSiguienteId();
                    sqlCommand.CommandText = "INSERT INTO ArticuloTrataTopico VALUES(@nombreTopicoFK, @idArticuloFK)";

                    foreach ( string nombreTopicoPK in nombreTopicoPKTopicos )
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
