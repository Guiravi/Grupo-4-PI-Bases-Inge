using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class EditorMiembroDBHandler
    {
        public void ActualizarMiembro(MiembroModel miembro) {
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"UPDATE Miembro
                                   SET nombre = @nombre, apellido1 = @apellido1, apellido2 = @apellido2, fechaNacimiento = @fechaNacimiento, paisFK = @paisFK, estado = @estado, ciudad = @ciudad, rutaImagenPerfil = @rutaImagenPerfil, informacionLaboral = @informacionLaboral
                                   WHERE usernamePK = @usernamePK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", miembro.usernamePK);
                    sqlCommand.Parameters.AddWithValue("@nombre", miembro.nombre);
                    sqlCommand.Parameters.AddWithValue("@apellido1", miembro.apellido1);
                    sqlCommand.Parameters.AddWithValue("@paisFK", miembro.paisFK);
                    sqlCommand.Parameters.AddWithValue("@rutaImagenPerfil", miembro.rutaImagenPerfil);

                    if (miembro.fechaNacimiento != null)
                    {
                        sqlCommand.Parameters.AddWithValue("@fechaNacimiento", miembro.fechaNacimiento);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
                    }
                    if (miembro.apellido2 != null)
                    {
                        sqlCommand.Parameters.AddWithValue("@apellido2", miembro.apellido2);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@apellido2", DBNull.Value);
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

                actualizarHabilidades(miembro.usernamePK, miembro.habilidades);
                actualizarPasatiempos(miembro.usernamePK, miembro.pasatiempos);
                actualizarIdiomas(miembro.usernamePK, miembro.idiomas);
            }
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
        public void SetRolMiembro() {
        }

        private void actualizarEdicionMiembroModel(string usernamePK, EdicionMiembroModel edicionMiembroModel) {
            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string sqlString = @"UPDATE Miembro SET apellido2 = @apellido2,
                                                        fechaNacimiento = @fechaNacimiento, 
                                                        estado = @estado
                                                        ciudad = @ciudad
                                                        informacionLaboral = @informacionLaboral
									WHERE usernamePK = @usernamePK ";
                SqlCommand command = new SqlCommand(sqlString, connection);
                command.Parameters.AddWithValue("@usernamePK", usernamePK);
                if ( edicionMiembroModel.apellido2 != null )
                {
                    command.Parameters.AddWithValue("@apellido2", edicionMiembroModel.apellido2);
                } else
                {
                    command.Parameters.AddWithValue("@apellido2", DBNull.Value);
                }
                if ( edicionMiembroModel.fechaNacimiento != null )
                {
                    command.Parameters.AddWithValue("@fechaNacimiento", edicionMiembroModel.fechaNacimiento);
                } else
                {
                    command.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
                }
                if ( edicionMiembroModel.estado != null )
                {
                    command.Parameters.AddWithValue("@estado", edicionMiembroModel.estado);
                } else
                {
                    command.Parameters.AddWithValue("@estado", DBNull.Value);
                }
                if ( edicionMiembroModel.ciudad != null )
                {
                    command.Parameters.AddWithValue("@ciudad", edicionMiembroModel.ciudad);
                } else
                {
                    command.Parameters.AddWithValue("@ciudad", DBNull.Value);
                }
                if ( edicionMiembroModel.informacionLaboral != null )
                {
                    command.Parameters.AddWithValue("@informacionLaboral", edicionMiembroModel.informacionLaboral);
                } else
                {
                    command.Parameters.AddWithValue("@informacionLaboral", DBNull.Value);
                }
                command.ExecuteNonQuery();
            }
        }

        private void actualizarPasatiempos(string usernamePK, List<string> pasatiempos) {
            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string sqlString = @"DELETE FROM MiembroPasatiempo WHERE @usernamePK = usernameFK";
                SqlCommand command = new SqlCommand(sqlString, connection);
                command.Parameters.AddWithValue("@usernamePK", usernamePK);
                command.ExecuteNonQuery();

                string sqlStringInsert = "INSERT INTO MiembroPasatiempo VALUES (@pasatiempo, @usernamePK)";
                command = new SqlCommand(sqlStringInsert, connection);

                foreach ( string _pasatiempo in pasatiempos )
                {
                    command.Parameters.AddWithValue("@usernamePK", usernamePK);
                    command.Parameters.AddWithValue("@pasatiempo", _pasatiempo);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        private void actualizarIdiomas(string usernamePK, List<string> idiomas) {
            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string sqlString = @"DELETE FROM MiembroIdioma WHERE @usernamePK = usernameFK";
                SqlCommand command = new SqlCommand(sqlString, connection);
                command.Parameters.AddWithValue("@usernamePK", usernamePK);
                command.ExecuteNonQuery();

                string sqlStringInsert = "INSERT INTO MiembroIdioma VALUES (@idioma, @usernamePK)";
                command = new SqlCommand(sqlStringInsert, connection);

                foreach ( string _idioma in idiomas )
                {
                    command.Parameters.AddWithValue("@usernamePK", usernamePK);
                    command.Parameters.AddWithValue("@idioma", _idioma);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        private void actualizarHabilidades(string usernamePK, List<string> habilidades) {
            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string sqlString = @"DELETE FROM MiembroHabilidad WHERE @usernamePK = usernameFK";
                SqlCommand command = new SqlCommand(sqlString, connection);
                command.Parameters.AddWithValue("@usernamePK", usernamePK);
                command.ExecuteNonQuery();

                string sqlStringInsert = "INSERT INTO MiembroHabilidad VALUES (@habilidad, @usernamePK)";
                command = new SqlCommand(sqlStringInsert, connection);

                foreach ( string _habilidad in habilidades )
                {
                    command.Parameters.AddWithValue("@usernamePK", usernamePK);
                    command.Parameters.AddWithValue("@habilidad", _habilidad);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }
    }
}
