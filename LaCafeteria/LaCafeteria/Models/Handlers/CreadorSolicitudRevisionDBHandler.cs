using LaCafeteria.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models.Handlers
{
	public class CreadorSolicitudRevisionDBHandler
	{
		public void CrearSolicitudRevision(string usernameMiemFK, int idArticuloFK, string estado)
		{
			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				string sqlString = @"INSERT INTO NucleoPuedeSerRevisorDeArticulo(usernameMiemFK, idArticuloFK, estado)
									 VALUES(@usernameMiemFK, @idArticuloFK, @estado)";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@usernameMiemFK", usernameMiemFK);
					sqlCommand.Parameters.AddWithValue("@idArticuloFK", idArticuloFK);
					sqlCommand.Parameters.AddWithValue("@estado", estado);

					sqlCommand.ExecuteNonQuery();
				}
			}
		}
	}
}
