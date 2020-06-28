using LaCafeteria.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models.Handlers
{
	public class AsignadorRevisoresDBHandler
	{
		public void AsignarRevisor(string usernameMiemFK, int idArticuloFK)
		{
			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				string sqlString = @"INSERT INTO NucleoRevisaArticulo(usernameMiemFK, idArticuloFK)
									 VALUES(@usernameFK, @idArticuloFK)";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@usernameFK", usernameMiemFK);
					sqlCommand.Parameters.AddWithValue("@idArticuloFK", idArticuloFK);

					sqlCommand.ExecuteNonQuery();
				}
			}
		}
	}
}
