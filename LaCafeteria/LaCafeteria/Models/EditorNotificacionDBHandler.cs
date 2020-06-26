using LaCafeteria.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models
{
	public class EditorNotificacionDBHandler
	{
		public void ActualizarNotificacion(Notificacion notificacionActualizada)
		{
			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				string sqlString = @"UPDATE Notificacion 
									 SET notificacionAID = @notificacionAID, 
									     usernameFK = @usernameFK, 
										 fechaCreacion = @fechaCreacion, 
										 mensaje = @mensaje, 
										 estado = @estado,
										 url = @url
								     WHERE @notificacionAID = notificacionAID";
				SqlCommand command = new SqlCommand(sqlString, connection);
				command.Parameters.AddWithValue("@notificacionAID", notificacionActualizada.notificacionAID);
				command.Parameters.AddWithValue("@usernameFK", notificacionActualizada.usernameFK);
				command.Parameters.AddWithValue("@fechaCreacion", notificacionActualizada.fechaCreacion);
				command.Parameters.AddWithValue("@mensaje", notificacionActualizada.mensaje);
				command.Parameters.AddWithValue("@estado", notificacionActualizada.estado);
				if (notificacionActualizada.url != null)
				{
					command.Parameters.AddWithValue("@url", notificacionActualizada.url);
				}
				else
				{
					command.Parameters.AddWithValue("@url", DBNull.Value);
				}

				command.ExecuteNonQuery();
			}
		}
	}
}
