using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models
{
	public class MiembroDBHandler
	{	
		public MiembroModel GetMiembro(string usernamePK)
		{
			MiembroModel miembro = null;

			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, pais, estado, ciudad, rutaImagenPerfil, 
											hobbies, habilidades, idiomas, informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro
									WHERE @usernamePK =  usernamePK";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
					using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
					{
						while (dataReader.Read())
						{
							miembro = new MiembroModel()
							{
								usernamePK = (string)dataReader["usernamePK"],
								email = (string)dataReader["email"],
								nombre = (string)dataReader["nombre"],
								apellido1 = (string)dataReader["apellido1"],
								apellido2 = (string)dataReader["apellido2"],
								fechaNacimiento = (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12),
								pais = (!DBNull.Value.Equals(dataReader["pais"])) ? (string)dataReader["pais"] : null,
								estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
								ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
								rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],
								hobbies = (!DBNull.Value.Equals(dataReader["hobbies"])) ? (string)dataReader["hobbies"] : null,
								habilidades = (!DBNull.Value.Equals(dataReader["habilidades"])) ? (string)dataReader["habilidades"] : null,
								idiomas = (!DBNull.Value.Equals(dataReader["idiomas"])) ? (string)dataReader["idiomas"] : null,
								informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
								meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (int?)dataReader["meritos"] : null,
								activo = (bool)dataReader["activo"],
								nombreRolFK = (string)dataReader["nombreRolFK"]
							};

						}
					}
				}
			}

			return miembro;
		}

		public List<MiembroModel> GetListaMiembros()
		{
			List<MiembroModel> listaMiembros = new List<MiembroModel>();

			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, pais, estado, ciudad, rutaImagenPerfil, 
											hobbies, habilidades, idiomas, informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
					{
						while (dataReader.Read())
						{
							MiembroModel miembroAutor = new MiembroModel()
							{
								usernamePK = (string)dataReader["usernamePK"],
								email = (string)dataReader["email"],
								nombre = (string)dataReader["nombre"],
								apellido1 = (string)dataReader["apellido1"],
								apellido2 = (string)dataReader["apellido2"],
								fechaNacimiento = (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12),
								pais = (!DBNull.Value.Equals(dataReader["pais"])) ? (string)dataReader["pais"] : null,
								estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
								ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
								rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],
								hobbies = (!DBNull.Value.Equals(dataReader["hobbies"])) ? (string)dataReader["hobbies"] : null,
								habilidades = (!DBNull.Value.Equals(dataReader["habilidades"])) ? (string)dataReader["habilidades"] : null,
								idiomas = (!DBNull.Value.Equals(dataReader["idiomas"])) ? (string)dataReader["idiomas"] : null,
								informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
								meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (int?)dataReader["meritos"] : null,
								activo = (bool)dataReader["activo"],
								nombreRolFK = (string)dataReader["nombreRolFK"]
							};

							listaMiembros.Add(miembroAutor);
						}
					}
				}
			}

			return listaMiembros;
		}

        public List<MiembroModel> GetAutoresArticulo(int idArticulo)
        {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, pais, estado, ciudad, rutaImagenPerfil
                                            , hobbies, habilidades, idiomas, informacionLaboral, meritos, activo, nombreRolFK
					                FROM Miembro M JOIN MiembroAutorDeArticulo MAA
                                        ON M.usernamePK = MAA.usernameMiemFK
                                    WHERE idArticuloFK = @idArticulo;";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {

                    sqlCommand.Parameters.AddWithValue("@idArticulo", idArticulo);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            MiembroModel miembroAutor = new MiembroModel()
                            {
                                usernamePK = (string)dataReader["usernamePK"],
                                email = (string)dataReader["email"],
                                nombre = (string)dataReader["nombre"],
                                apellido1 = (string)dataReader["apellido1"],
                                apellido2 = (string)dataReader["apellido2"],
                                fechaNacimiento = (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12),
                                pais = (!DBNull.Value.Equals(dataReader["pais"])) ? (string)dataReader["pais"] : null,
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],
                                hobbies = (!DBNull.Value.Equals(dataReader["hobbies"])) ? (string)dataReader["hobbies"] : null,
                                habilidades = (!DBNull.Value.Equals(dataReader["habilidades"])) ? (string)dataReader["habilidades"] : null,
                                idiomas = (!DBNull.Value.Equals(dataReader["idiomas"])) ? (string)dataReader["idiomas"] : null,
                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (int?)dataReader["meritos"] : null,
                                activo = (bool)dataReader["activo"],
                                nombreRolFK = (string)dataReader["nombreRolFK"]
                            };

                            listaMiembros.Add(miembroAutor);
                        }
                    }
                }
            }

            return listaMiembros;
        }

        public int GetCalificacionMiembro(string username, int idArticulo)
        {
            int calificacion = 10;

            string connectionString = AppSettings.GetConnectionString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {                
                string consulta = "SELECT MCA.calificacion " +
                    "FROM Miembro M " +
                    "JOIN MiembroCalificaArticulo MCA " +
                        "ON M.usernamePK = MCA.usernameMiemFK " +
                    "JOIN Articulo A " +
                        "ON MCA.idArticuloFK = A.idArticuloPK " +
                    "WHERE MCA.usernameMiemFK = @user " +
                        "AND MCA.idArticuloFK = @id;";

                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand(consulta, sqlConnection))
                {

                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@id", idArticulo);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            calificacion = reader.GetInt32(0);
                        }
                    }
                }
            }

            return calificacion;
        }

        public void CalificarArticulo(string username, int idArticulo, int valorCalif)
        {
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string consulta = "INSERT INTO MiembroCalificaArticulo (usernameMiemFK, idArticuloFK, calificacion) " +
                    " VALUES (@user, @id, @calif);";

                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand(consulta, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@id", idArticulo);
                    cmd.Parameters.AddWithValue("@calif", valorCalif);
                    cmd.ExecuteNonQuery();
                }
            }
        }


	}
}
