using LaCafeteria.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;

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

        public List<DatosGraficoDona> GetMiembrosPorPais()
        {
            List<DatosGraficoDona> lista = new List<DatosGraficoDona>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT paisFK, COUNT(*) AS cantidad
                                        FROM [dbo].[Miembro]
                                        GROUP BY paisFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DatosGraficoDona datos = new DatosGraficoDona((string)dataReader["paisFK"], (int)dataReader["cantidad"]);
                        lista.Add(datos);
                    }
                }
            }

            return lista;
        }

        public List<DatosGraficoBarrasApilado> GetHabilidadesPorPais()
        {
            List<DatosGraficoBarrasApilado> lista = new List<DatosGraficoBarrasApilado>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT MH.habilidad, M.paisFK, COUNT(*) AS cantidad
                                    FROM [dbo].[Miembro] M
                                    JOIN [dbo].[MiembroHabilidad] MH
	                                    ON M.usernamePK = MH.usernameFK
                                    WHERE	MH.habilidad IN (SELECT habilidadPK
						                                    FROM [Catalogo].[Habilidad])
                                    GROUP BY M.paisFK, MH.habilidad
                                    ORDER BY MH.habilidad, M.paisFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DatosGraficoBarrasApilado datos = new DatosGraficoBarrasApilado((string)dataReader["habilidad"], (string)dataReader["paisFK"], (int)dataReader["cantidad"]);
                        lista.Add(datos);
                    }
                }
            }

            return lista;
        }

        public List<DatosGraficoBarrasApilado> GetHabilidadesPorIdioma()
        {
            List<DatosGraficoBarrasApilado> lista = new List<DatosGraficoBarrasApilado>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT  MH.habilidad, MI.idiomaFK, COUNT(*) AS cantidad
                                    FROM [dbo].[Miembro] M
                                    JOIN [dbo].[MiembroHabilidad] MH
	                                    ON M.usernamePK = MH.usernameFK
                                    JOIN [dbo].[MiembroIdioma] MI
	                                    ON M.usernamePK = MI.usernameFK
                                    WHERE	MH.habilidad IN (SELECT habilidadPK
						                                    FROM [Catalogo].[Habilidad])
                                    GROUP BY MI.idiomaFK, MH.habilidad
                                    ORDER BY MH.habilidad, MI.idiomaFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DatosGraficoBarrasApilado datos = new DatosGraficoBarrasApilado((string)dataReader["habilidad"], (string)dataReader["idiomaFK"], (int)dataReader["cantidad"]);
                        lista.Add(datos);
                    }
                }
            }

            return lista;
        }

    }
}
