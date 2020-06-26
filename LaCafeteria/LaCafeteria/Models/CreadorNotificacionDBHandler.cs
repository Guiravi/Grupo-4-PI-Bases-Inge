using LaCafeteria.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models
{
	public class CreadorNotificacionDBHandler
	{
		public void CrearNotificacion(Notificacion notificacion)
		{
			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				string sqlString = @"INSERT INTO Notificacion(usernameFK, fechaCreacion, mensaje, estado, url)
									 VALUES(@usernameFK, @fechaCreacion, @mensaje, @estado, @url)";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@usernameFK", notificacion.usernameFK);
					sqlCommand.Parameters.AddWithValue("@fechaCreacion", notificacion.fechaCreacion);
					sqlCommand.Parameters.AddWithValue("@mensaje", notificacion.mensaje);
					sqlCommand.Parameters.AddWithValue("@estado", notificacion.estado);
					if (notificacion.url != null)
					{
						sqlCommand.Parameters.AddWithValue("@url", notificacion.url);
					}
					else
					{
						sqlCommand.Parameters.AddWithValue("@url", DBNull.Value);
					}

					sqlCommand.ExecuteNonQuery();
				}
			}
		}
	}
}
