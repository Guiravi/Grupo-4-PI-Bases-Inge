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
        public void ActualizarMiembro(string usernamePK, EdicionMiembroModel edicionMiembroModel, List<string> idiomas, List<string> habilidades, List<string> pasatiempos) {
            actualizarEdicionMiembroModel(usernamePK, edicionMiembroModel);
            actualizarIdiomas(usernamePK, idiomas);
            actualizarHabilidades(usernamePK, habilidades);
            actualizarPasatiempos(usernamePK, pasatiempos);
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

                string sqlStringInsert = "INSERT INTO MiembroPasatiempo VALUES (@usernamePK, @pasatiempo)";
                command = new SqlCommand(sqlStringInsert, connection);

                foreach ( string _pasatiempo in pasatiempos )
                {
                    command.Parameters.AddWithValue("@usernamePK", usernamePK);
                    command.Parameters.AddWithValue("@pasatiempo", _pasatiempo);
                    command.ExecuteNonQuery();
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

                string sqlStringInsert = "INSERT INTO MiembroIdioma VALUES (@usernamePK, @idioma)";
                command = new SqlCommand(sqlStringInsert, connection);

                foreach ( string _idioma in idiomas )
                {
                    command.Parameters.AddWithValue("@usernamePK", usernamePK);
                    command.Parameters.AddWithValue("@idioma", _idioma);
                    command.ExecuteNonQuery();
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

                string sqlStringInsert = "INSERT INTO MiembroHabilidad VALUES (@usernamePK, @habilidad)";
                command = new SqlCommand(sqlStringInsert, connection);

                foreach ( string _habilidad in habilidades )
                {
                    command.Parameters.AddWithValue("@usernamePK", usernamePK);
                    command.Parameters.AddWithValue("@idioma", _habilidad);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
