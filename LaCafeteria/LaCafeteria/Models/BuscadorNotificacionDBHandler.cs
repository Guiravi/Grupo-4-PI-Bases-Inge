using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models
{
	public class BuscadorNotificacionDBHandler
	{
		public Notificacion GetNotificacion(int notificacionAID)
		{
			Notificacion notificacion = null;

			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = @"SELECT notificacionAID, usernameFK, fechaCreacion, mensaje, estado, url
								     FROM Notificacion WHERE @notificacionAID = notificacionAID";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@notificacionAID", notificacionAID);
					SqlDataReader dataReader = sqlCommand.ExecuteReader();
					if (dataReader.Read())
					{
						notificacion = new Notificacion()
						{
							notificacionAID = (int)dataReader["notificacionAID"],
							usernameFK = (string)dataReader["usenameFK"],
							fechaCreacion = (string)dataReader["fechaCreacion"].ToString().Remove(dataReader["fechaCreacion"].ToString().Length - 12, 12),
							mensaje = (string)dataReader["mensaje"],
							estado = (string)dataReader["estado"],
							url = (!DBNull.Value.Equals(dataReader["url"])) ? (string)dataReader["url"] : null

						};
					}
				}
			}

			return notificacion;
		}
	}
}
