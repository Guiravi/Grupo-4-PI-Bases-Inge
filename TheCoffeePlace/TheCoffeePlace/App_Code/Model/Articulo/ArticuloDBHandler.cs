using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ArticuloDBHandler
/// </summary>
namespace TheCoffeePlace.Models
{
	public class ArticuloDBHandler
	{
		public ArticuloDBHandler()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void  SaveArticulo(ArticuloModel articulo)
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

                connection.Close();
            }

            return current_id;
        }
    }
}