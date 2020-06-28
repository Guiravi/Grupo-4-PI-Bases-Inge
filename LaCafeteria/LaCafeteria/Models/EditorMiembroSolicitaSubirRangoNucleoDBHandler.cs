using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models
{
    public class EditorMiembroSolicitaSubirRangoNucleoDBHandler
    {


        public void VotarPromover(string usernamePK, string usernameMiembroFK)
        {
            int solicitudes = 0;
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"UPDATE dbo.[MiembroSolicitaSubirRangoNucleo] SET estado =  'Aceptado' WHERE usernameMiembroFK = @usernamePK AND usernameNucleoFK = @usernameNucleoFK AND estado IS NULL";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
                    sqlCommand.Parameters.AddWithValue("@usernameNucleoFK", usernameMiembroFK);
                    sqlCommand.ExecuteNonQuery();
                }
            }


        }
        public void VotarRechazar(string usernamePK, string usernameMiembroFK)
        {
            int solicitudes = 0;
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"UPDATE dbo.[MiembroSolicitaSubirRangoNucleo] SET estado =  'Rechazado' WHERE usernameMiembroFK = @usernamePK AND usernameNucleoFK = @usernameNucleoFK AND estado IS NULL" ;

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
                    sqlCommand.Parameters.AddWithValue("@usernameNucleoFK", usernameMiembroFK);
                    sqlCommand.ExecuteNonQuery();
                }
            }


        }

        public void BorrarSolicitudes(string usernamePK)
        {
            int solicitudes = 0;
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"DELETE FROM dbo.[MiembroSolicitaSubirRangoNucleo]  WHERE usernameMiembroFK = @usernamePK  ";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
                    sqlCommand.ExecuteNonQuery();
                }
            }


        }

        public void SolicitarSubirRango(string usernamePK, List<MiembroModel> miembros)
        {
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {      

                string sqlString = @"INSERT INTO dbo.[MiembroSolicitaSubirRangoNucleo](usernameNucleoFK, usernameMiembroFK, estado)
											 

									VALUES(@usernameNucleoFK,@usernamePK , null)";

                sqlConnection.Open();
                foreach (var miembro in miembros) {
                    using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
                        sqlCommand.Parameters.AddWithValue("@usernameNucleoFK", miembro.usernamePK);
                        
                       

                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}	

