using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models
{
    public class RevisionSolicitudesPreviasMiembroSubirRangoNucleoDBHandler
    {
        public int VerSiSolicitado(string usernamePK)
        {

            int solicitudes = 0;
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT DISTINCT * 
									FROM dbo.[MiembroSolicitaSubirRangoNucleo] WHERE usernameMiembroFK = @usernamePK" ;

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            solicitudes = solicitudes + 1;
                            
                        }
                    }
                }
            }

            return solicitudes;
        }

        public int VerTodosSolicitadosAceptados(string usernamePK)
        {

            int solicitudes = 0;
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT DISTINCT * 
									FROM dbo.[MiembroSolicitaSubirRangoNucleo] WHERE usernameMiembroFK = @usernamePK AND estado = 'Aceptado'";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            solicitudes = solicitudes + 1;

                        }
                    }
                }
            }

            return solicitudes;
        }


        public int VerTodosSolicitadosRechazados(string usernamePK)
        {

            int solicitudes = 0;
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT DISTINCT * 
									FROM dbo.[MiembroSolicitaSubirRangoNucleo] WHERE usernameMiembroFK = @usernamePK AND estado = 'Rechazado'";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            solicitudes = solicitudes + 1;

                        }
                    }
                }
            }

            return solicitudes;
        }

    }

}
