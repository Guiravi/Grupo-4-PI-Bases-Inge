using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class CreadorMiembroDBHandler
    {
        public void CrearMiembro(MiembroModel miembro) {
            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection sqlConnection = new SqlConnection(connectionString) )
            {

                string sqlString = @"INSERT INTO Miembro(usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, pais, estado, ciudad, rutaImagenPerfil, 
											hobbies, habilidades, idiomas, informacionLaboral)

									VALUES(@usernamePK, @email, @nombre, @apellido1, @apellido2, @fechaNacimiento, @pais, @estado, @ciudad, @rutaImagenPerfil, @hobbies, @habilidades, @idiomas, @informacionLaboral)";

                sqlConnection.Open();
                using ( SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection) )
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", miembro.usernamePK);
                    sqlCommand.Parameters.AddWithValue("@email", miembro.email);
                    sqlCommand.Parameters.AddWithValue("@nombre", miembro.nombre);
                    sqlCommand.Parameters.AddWithValue("@apellido1", miembro.apellido1);
                    sqlCommand.Parameters.AddWithValue("@apellido2", miembro.apellido2);
                    sqlCommand.Parameters.AddWithValue("@fechaNacimiento", miembro.fechaNacimiento);
                    if ( miembro.pais != null )
                    {
                        sqlCommand.Parameters.AddWithValue("@pais", miembro.pais);
                    } else
                    {
                        sqlCommand.Parameters.AddWithValue("@pais", DBNull.Value);
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
                    sqlCommand.Parameters.AddWithValue("@rutaImagenPerfil", miembro.rutaImagenPerfil);
                    if ( miembro.hobbies != null )
                    {
                        sqlCommand.Parameters.AddWithValue("@hobbies", miembro.hobbies);
                    } else
                    {
                        sqlCommand.Parameters.AddWithValue("@hobbies", DBNull.Value);
                    }
                    if ( miembro.habilidades != null )
                    {
                        sqlCommand.Parameters.AddWithValue("@habilidades", miembro.habilidades);
                    } else
                    {
                        sqlCommand.Parameters.AddWithValue("@habilidades", DBNull.Value);
                    }
                    if ( miembro.idiomas != null )
                    {
                        sqlCommand.Parameters.AddWithValue("@idiomas", miembro.idiomas);
                    } else
                    {
                        sqlCommand.Parameters.AddWithValue("@idiomas", DBNull.Value);
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
        }
    }
}
