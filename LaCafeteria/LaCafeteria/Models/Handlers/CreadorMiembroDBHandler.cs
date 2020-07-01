using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class CreadorMiembroDBHandler : ICreadorMiembroDBHandler
    {
        public void CrearMiembro(MiembroModel miembro) {
            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection sqlConnection = new SqlConnection(connectionString) )
            {

                string sqlString = @"INSERT INTO Miembro(usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											 informacionLaboral)

									VALUES(@usernamePK, @email, @nombre, @apellido1, @apellido2, @fechaNacimiento, @pais, @estado, @ciudad, @rutaImagenPerfil, @informacionLaboral)";

                sqlConnection.Open();
                using ( SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection) )
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", miembro.usernamePK);
                    sqlCommand.Parameters.AddWithValue("@email", miembro.email);
                    sqlCommand.Parameters.AddWithValue("@nombre", miembro.nombre);
                    sqlCommand.Parameters.AddWithValue("@apellido1", miembro.apellido1);
                    sqlCommand.Parameters.AddWithValue("@pais", miembro.paisFK);
                    sqlCommand.Parameters.AddWithValue("@rutaImagenPerfil", miembro.rutaImagenPerfil);

                    if ( miembro.fechaNacimiento != null )
                    {
                        sqlCommand.Parameters.AddWithValue("@fechaNacimiento", miembro.fechaNacimiento);
                    } else
                    {
                        sqlCommand.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
                    }
                    if ( miembro.apellido2 != null )
                    {
                        sqlCommand.Parameters.AddWithValue("@apellido2", miembro.apellido2);
                    } else
                    {
                        sqlCommand.Parameters.AddWithValue("@apellido2", DBNull.Value);
                    }
                    if ( miembro.estado != null )
                    {
                        sqlCommand.Parameters.AddWithValue("@estado", miembro.estado);
                    } else
                    {
                        sqlCommand.Parameters.AddWithValue("@estado", DBNull.Value);
                    }

                    if ( miembro.ciudad != null )
                    {
                        sqlCommand.Parameters.AddWithValue("@ciudad", miembro.ciudad);
                    } else
                    {
                        sqlCommand.Parameters.AddWithValue("@ciudad", DBNull.Value);
                    }                   

                    if ( miembro.informacionLaboral != null )
                    {
                        sqlCommand.Parameters.AddWithValue("@informacionLaboral", miembro.informacionLaboral);
                    } else
                    {
                        sqlCommand.Parameters.AddWithValue("@informacionLaboral", DBNull.Value);
                    }

                    sqlCommand.ExecuteNonQuery();                   
                }              
            }

            if (miembro.pasatiempos.Count != 0)
            {
                agregarPasatiempos(miembro.usernamePK, miembro.pasatiempos);
            }

            if (miembro.habilidades.Count != 0)
            {
                agregarHabilidades(miembro.usernamePK, miembro.habilidades);
            }

            if (miembro.idiomas.Count != 0)
            {
                agregarIdiomas(miembro.usernamePK, miembro.idiomas);
            }
        }

        private void agregarPasatiempos(string usernamePK, List<string> pasatiempos) {
            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string sqlStringInsert = "INSERT INTO MiembroPasatiempo VALUES (@pasatiempo, @usernamePK)";
                SqlCommand command = new SqlCommand(sqlStringInsert, connection);

                
                foreach ( string _pasatiempo in pasatiempos )
                {                   
                    command.Parameters.AddWithValue("@pasatiempo", _pasatiempo);
                    command.Parameters.AddWithValue("@usernamePK", usernamePK);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        private void agregarIdiomas(string usernamePK, List<string> idiomas) {
            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string sqlStringInsert = "INSERT INTO MiembroIdioma VALUES (@idioma, @usernamePK)";
                SqlCommand command = new SqlCommand(sqlStringInsert, connection);

                foreach ( string _idioma in idiomas )
                {
                    command.Parameters.AddWithValue("@usernamePK", usernamePK);
                    command.Parameters.AddWithValue("@idioma", _idioma);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        private void agregarHabilidades(string usernamePK, List<string> habilidades) {
            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string sqlStringInsert = "INSERT INTO MiembroHabilidad VALUES (@habilidad, @usernamePK)";
                SqlCommand command = new SqlCommand(sqlStringInsert, connection);

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
