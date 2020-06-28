using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class AlmacenadorArticuloDBHandler
    {
        public void GuardarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<CategoriaTopicoModel> listaCategoriaTopicos) {
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
                    articulo.articuloAID = ObtenerSiguienteId();
                    sqlCommand.CommandText = "INSERT INTO MiembroAutorDeArticulo VALUES(@usernameMiemFK, @idArticuloFK)";

                    foreach ( string usernamePK in usernamePKMiembrosAutores )
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("@usernameMiemFK", usernamePK);
                        sqlCommand.Parameters.AddWithValue("@idArticuloFK", articulo.articuloAID);
                        sqlCommand.ExecuteNonQuery();
                    }

                    //Guardar registro de relaciones de Articulo con sus Topicos

                    articulo.articuloAID = ObtenerSiguienteId();
                    sqlCommand.CommandText = "INSERT INTO ArticuloTrataTopico VALUES(@nombreCategoriaFK,@nombreTopicoFK, @idArticuloFK)";

                    foreach ( CategoriaTopicoModel categoriaTopico in listaCategoriaTopicos )
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

        private int ObtenerSiguienteId() {
            String connectionString = AppSettings.GetConnectionString();
            SqlCommand cmd;
            int current_id;

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {

                connection.Open();

                cmd = new SqlCommand("SELECT IDENT_CURRENT ('Articulo')", connection);

                SqlDataReader identReader = cmd.ExecuteReader();

                current_id = 0;

                while ( identReader.Read() )
                {
                    current_id = Convert.ToInt32(identReader.GetValue(0));
                }

                identReader.Close();

            }

            return current_id;
        }
    }
}
