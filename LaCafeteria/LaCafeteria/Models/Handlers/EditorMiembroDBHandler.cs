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
        public void ActualizarMiembro(string usernamePK, MiembroModel miembro) {

            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string sqlString = @"UPDATE Miembro SET idiomas = @idiomas,
                                                        hobbies = @hobbies, 
                                                        habilidades = @habilidades
									WHERE usernamePK = @usernamePK ";
                SqlCommand command = new SqlCommand(sqlString, connection);
                command.Parameters.AddWithValue("@usernamePK", usernamePK);
                if ( miembro.hobbies != null )
                {
                    command.Parameters.AddWithValue("@hobbies", miembro.hobbies);
                } else
                {
                    command.Parameters.AddWithValue("@hobbies", DBNull.Value);
                }
                if ( miembro.habilidades != null )
                {
                    command.Parameters.AddWithValue("@habilidades", miembro.habilidades);
                } else
                {
                    command.Parameters.AddWithValue("@habilidades", DBNull.Value);
                }
                if ( miembro.idiomas != null )
                {
                    command.Parameters.AddWithValue("@idiomas", miembro.idiomas);
                } else
                {
                    command.Parameters.AddWithValue("@idiomas", DBNull.Value);
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
