﻿using LaCafeteria.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models.Handlers
{
	public class InformacionMiembroDBHandler
	{
		public List<Notificacion> GetNotificaciones(string usernamePK)
		{	
			List<Notificacion> listaNotificaciones = new List<Notificacion>();

			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = @"SELECT notificacionAID, usernameFK, fechaCreacion, mensaje, estado, url
								     FROM Notificacion WHERE @usernamePK = usernameFK
									 ORDER BY fechaCreacion, estado DESC";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
					SqlDataReader dataReader = sqlCommand.ExecuteReader();
					while(dataReader.Read())
					{
						Notificacion notificacion = new Notificacion()
						{
							notificacionAID = (int)dataReader["notificacionAID"],
							usernameFK = (string)dataReader["usernameFK"],
							fechaCreacion = (string)dataReader["fechaCreacion"].ToString().Remove(dataReader["fechaCreacion"].ToString().Length - 12, 12),
							mensaje = (string)dataReader["mensaje"],
							estado = (string)dataReader["estado"],
							url = (!DBNull.Value.Equals(dataReader["url"])) ? (string)dataReader["url"] : null

						};

						listaNotificaciones.Add(notificacion);
					}
				}
			}

			return listaNotificaciones;
		}

        public float GetMeritos(string username)
        {
            float merito;
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT meritos
								     FROM Miembro 
                                        WHERE @username = usernamePK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", username);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    dataReader.Read();
                    merito = (float)dataReader["meritos"];
                }
            }
            return merito;
        }
	}
}
