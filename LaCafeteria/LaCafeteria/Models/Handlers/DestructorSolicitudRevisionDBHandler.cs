using LaCafeteria.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models.Handlers
{
	public class DestructorSolicitudRevisionDBHandler
	{
		public void DestruirSolicitudRevision(string usernameMiemFK, int idArticuloFK)
		{
			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				string sqlString = @"DELETE FROM NucleoPuedeSerRevisorDeArticulo
									 WHERE  @usernameMiemFK = usernameMiemFK AND
									 @idArticuloFK = idArticuloFK";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@usernameMiemFK", usernameMiemFK);
					sqlCommand.Parameters.AddWithValue("@idArticuloFK", idArticuloFK);

					sqlCommand.ExecuteNonQuery();
				}
			}
		}
	}
}
