using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class CalificadorDeArticulosDBHandler : ICalificadorDeArticulosDBHandler
    {
        public void CalificarArticulo(string username, int idArticulo, int valorCalif) {
            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection sqlConnection = new SqlConnection(connectionString) )
            {
                string consulta = "INSERT INTO MiembroCalificaArticulo (usernameMiemFK, idArticuloFK, calificacion) " +
                    " VALUES (@user, @id, @calif);";

                sqlConnection.Open();
                using ( SqlCommand cmd = new SqlCommand(consulta, sqlConnection) )
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
