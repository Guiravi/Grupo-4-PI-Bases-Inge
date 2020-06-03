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
		public bool ExisteMiembro(string usernamePK)
		{
			bool existe = false;

			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = "SELECT 1 FROM Miembro WHERE @usernamePK = usernamePK";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
					SqlDataReader dataReader = sqlCommand.ExecuteReader();
					if(dataReader.HasRows)
					{
						existe = true;
					}
				}
			}
			return existe;
		}

		public void CrearMiembro(MiembroModel miembro)
		{
			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = @"INSERT INTO Miembro(usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, pais, estado, ciudad, rutaImagenPerfil, 
											hobbies, habilidades, idiomas, informacionLaboral)

									VALUES(@usernamePK, @email, @nombre, @apellido1, @apellido2, @fechaNacimiento, @pais, @estado, @ciudad, @rutaImagenPerfil, @hobbies, @habilidades, @idiomas, @informacionLaboral)";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@usernamePK", miembro.usernamePK);
					sqlCommand.Parameters.AddWithValue("@email", miembro.email);
					sqlCommand.Parameters.AddWithValue("@nombre", miembro.nombre);
					sqlCommand.Parameters.AddWithValue("@apellido1", miembro.apellido1);
					sqlCommand.Parameters.AddWithValue("@apellido2", miembro.apellido2);
					sqlCommand.Parameters.AddWithValue("@fechaNacimiento", miembro.fechaNacimiento);
					if (miembro.pais != null)
					{
						sqlCommand.Parameters.AddWithValue("@pais", miembro.pais);
					}
					else
					{
						sqlCommand.Parameters.AddWithValue("@pais", DBNull.Value);
					}
					if (miembro.estado != null)
					{
						sqlCommand.Parameters.AddWithValue("@estado", miembro.estado);
					}
					else
					{
						sqlCommand.Parameters.AddWithValue("@estado", DBNull.Value);
					}
					
					if (miembro.ciudad != null)
					{
						sqlCommand.Parameters.AddWithValue("@ciudad", miembro.ciudad);
					}
					else
					{
						sqlCommand.Parameters.AddWithValue("@ciudad", DBNull.Value);
					}
					sqlCommand.Parameters.AddWithValue("@rutaImagenPerfil", miembro.rutaImagenPerfil);
					if (miembro.hobbies != null)
					{
						sqlCommand.Parameters.AddWithValue("@hobbies", miembro.hobbies);
					}
					else
					{
						sqlCommand.Parameters.AddWithValue("@hobbies", DBNull.Value);
					}
					if (miembro.habilidades != null)
					{
						sqlCommand.Parameters.AddWithValue("@habilidades", miembro.habilidades);
					}
					else
					{
						sqlCommand.Parameters.AddWithValue("@habilidades", DBNull.Value);
					}
					if (miembro.idiomas != null)
					{
						sqlCommand.Parameters.AddWithValue("@idiomas", miembro.idiomas);
					}
					else
					{
						sqlCommand.Parameters.AddWithValue("@idiomas", DBNull.Value);
					}
					if (miembro.informacionLaboral != null)
					{
						sqlCommand.Parameters.AddWithValue("@informacionLaboral", miembro.informacionLaboral);
					}
					else
					{
						sqlCommand.Parameters.AddWithValue("@informacionLaboral", DBNull.Value);
					}

                    sqlCommand.ExecuteNonQuery();
				}
			}
		}

        public void ActualizarMiembro(string usernamePK, MiembroModel miembro)
        {

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlString = @"UPDATE Miembro SET 
                                                        hobbies = @hobbies, 
                                                        habilidades = @habilidades
									WHERE usernamePK = @usernamePK ";
                SqlCommand command = new SqlCommand(sqlString, connection);
                command.Parameters.AddWithValue("@usernamePK", usernamePK);
                if (miembro.hobbies != null)
                {
                    command.Parameters.AddWithValue("@hobbies", miembro.hobbies);
                }
                else
                {
                    command.Parameters.AddWithValue("@hobbies", DBNull.Value);
                }
                if (miembro.habilidades != null)
                {
                    command.Parameters.AddWithValue("@habilidades", miembro.habilidades);
                }
                else
                {
                    command.Parameters.AddWithValue("@habilidades", DBNull.Value);
                }
                if (miembro.idiomas != null)
                {
                    command.Parameters.AddWithValue("@idiomas", miembro.idiomas);
                }
                else
                {
                    command.Parameters.AddWithValue("@idiomas", DBNull.Value);
                }
                command.ExecuteNonQuery();
            }
        }

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
								meritos = (int)dataReader["meritos"],
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
								meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (int)dataReader["meritos"] : 0,
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
                                meritos = (int)dataReader["meritos"],
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
