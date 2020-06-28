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
                    if (dataReader.HasRows)
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

                string sqlString = @"INSERT INTO Miembro(usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											 informacionLaboral)

									VALUES(@usernamePK, @email, @nombre, @apellido1, @apellido2, @fechaNacimiento, @paisFK, @estado, @ciudad, @rutaImagenPerfil, @hobbies, @habilidades, @idiomas, @informacionLaboral)";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", miembro.usernamePK);
                    sqlCommand.Parameters.AddWithValue("@email", miembro.email);
                    sqlCommand.Parameters.AddWithValue("@nombre", miembro.nombre);
                    sqlCommand.Parameters.AddWithValue("@apellido1", miembro.apellido1);
                    sqlCommand.Parameters.AddWithValue("@apellido2", miembro.apellido2);
                    sqlCommand.Parameters.AddWithValue("@fechaNacimiento", miembro.fechaNacimiento);
                    if (miembro.paisFK != null)
                    {
                        sqlCommand.Parameters.AddWithValue("@paisPK", miembro.paisFK);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@paisPK", DBNull.Value);
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
                    /*
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
					*/
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
        /*
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
        */
        public MiembroModel GetMiembro(string usernamePK)
        {
            MiembroModel miembro = null;

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											 informacionLaboral, meritos, activo, nombreRolFK
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
                                paisFK = (!DBNull.Value.Equals(dataReader["paisPK"])) ? (string)dataReader["paisFK"] : null,
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],
                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
                                meritos = (double)dataReader["meritos"],
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

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
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
                                apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
                                fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                paisFK = (!DBNull.Value.Equals(dataReader["paisFK"])) ? (string)dataReader["paisFK"] : null,
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],

                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (double)dataReader["meritos"] : 0,
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
        public List<MiembroModel> GetListaMiembrosSolicitud(string usernameNucleoFK)
        {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
                                    FROM Miembro
                                    JOIN MiembroSolicitaSubirRangoNucleo
                                    ON Miembro.usernamePK = MiembroSolicitaSubirRangoNucleo.usernameMiembroFK
                                    WHERE MiembroSolicitaSubirRangoNucleo.estado IS NULL AND usernameNucleoFK = @usernameNucleoFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernameNucleoFK", usernameNucleoFK);
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
                                apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
                                fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                paisFK = (!DBNull.Value.Equals(dataReader["paisFK"])) ? (string)dataReader["paisFK"] : null,

                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],

                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (double)dataReader["meritos"] : 0,
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


        public void ModificarMiembro(string usernamePK, string nombreRolFK)
        {
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string consulta = "UPDATE Miembro Set nombreRolFK = @nombreRolFK WHERE usernamePK = @usernamePK;";

                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand(consulta, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@nombreRolFK", nombreRolFK);
                    cmd.Parameters.AddWithValue("@usernamePK", usernamePK);

                    cmd.ExecuteNonQuery();

                }
            }

        }
        public string GetRango(string usernamePK)
        {
            string rango = "";
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT nombreRolFK
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

                            rango = (string)dataReader["nombreRolFK"];


                        }
                    }
                }
            }
            return rango;
        }
        public List<MiembroModel> GetAutoresArticulo(int idArticulo)
        {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil
                                            , informacionLaboral, meritos, activo, nombreRolFK
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
                                paisFK = (!DBNull.Value.Equals(dataReader["paisFK"])) ? (string)dataReader["paisFK"] : null,
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],

                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
                                meritos = (double)dataReader["meritos"],
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

        public List<MiembroModel> GetListaNucleos()
        {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro WHERE nombreRolFK = 'Núcleo'";

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
                                apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
                                fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                paisFK = (!DBNull.Value.Equals(dataReader["paisFK"])) ? (string)dataReader["paisFK"] : null,
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],

                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (double)dataReader["meritos"] : 0,
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






        public List<MiembroModel> GetListaNucleosSolicitud()
        {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro WHERE nombreRolFK = 'Núcleo' OR nombreRolFK = 'Coordinador' ";

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
                                apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
                                fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                paisFK = (!DBNull.Value.Equals(dataReader["paisFK"])) ? (string)dataReader["paisFK"] : null,
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],

                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (double)dataReader["meritos"] : 0,
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

    }
}

