﻿using System;
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



        public void ActualizarMiembro(string usernamePK, MiembroModel miembro)
        {

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlString = @"UPDATE Miembro SET idiomas = @idiomas,
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
