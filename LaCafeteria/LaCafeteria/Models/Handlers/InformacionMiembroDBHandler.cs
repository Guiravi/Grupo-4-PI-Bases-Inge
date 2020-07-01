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
									 ORDER BY fechaCreacion DESC";

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
							fechaCreacion = (string)Convert.ToDateTime(dataReader["fechaCreacion"]).ToString("yyyy/MM/dd"),
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

        public double GetMeritos(string username)
        {
            double merito;
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


        public List<string> GetIdiomasMiembro(string username)
        {
            List<string> idiomasMiembro = new List<string>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT idiomaFK
									FROM MiembroIdioma
									WHERE @usernameFK =  usernameFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernameFK", username);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            idiomasMiembro.Add((string)dataReader["idiomaFK"]);
                        }

                    }
                }

            }
            return idiomasMiembro;
        }

        public List<string> GetPasatiemposMiembro(string username)
        {
            List<string> pasatiemposMiembro = new List<string>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT pasatiempoFK
									FROM MiembroPasatiempo
									WHERE @usernameFK =  usernameFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernameFK", username);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            pasatiemposMiembro.Add((string)dataReader["pasatiempoFK"]);
                        }

                    }
                }

            }
            return pasatiemposMiembro;
        }

        public List<string> GetHabilidadesMiembro(string username)
        {
            List<string> habilidadesMiembro = new List<string>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT habilidadFK
									FROM MiembroHabilidad
									WHERE @usernameFK =  usernameFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernameFK", username);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            habilidadesMiembro.Add((string)dataReader["habilidadFK"]);
                        }

                    }
                }
            }
            return habilidadesMiembro;
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

                string sqlString = @"SELECT MH.habilidadFK, M.paisFK, COUNT(*) AS cantidad
                                    FROM [dbo].[Miembro] M
                                    JOIN [dbo].[MiembroHabilidad] MH
	                                    ON M.usernamePK = MH.usernameFK
                                    WHERE	MH.habilidadFK IN (SELECT habilidadPK
						                                    FROM [Catalogo].[Habilidad])
                                    GROUP BY M.paisFK, MH.habilidadFK
                                    ORDER BY MH.habilidadFK, M.paisFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DatosGraficoBarrasApilado datos = new DatosGraficoBarrasApilado((string)dataReader["habilidadFK"], (string)dataReader["paisFK"], (int)dataReader["cantidad"]);
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

                string sqlString = @"SELECT  MH.habilidadFK, MI.idiomaFK, COUNT(*) AS cantidad
                                    FROM [dbo].[Miembro] M
                                    JOIN [dbo].[MiembroHabilidad] MH
	                                    ON M.usernamePK = MH.usernameFK
                                    JOIN [dbo].[MiembroIdioma] MI
	                                    ON M.usernamePK = MI.usernameFK
                                    WHERE	MH.habilidadFK IN (SELECT habilidadPK
						                                    FROM [Catalogo].[Habilidad])
                                    GROUP BY MI.idiomaFK, MH.habilidadFK
                                    ORDER BY MH.habilidadFK, MI.idiomaFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DatosGraficoBarrasApilado datos = new DatosGraficoBarrasApilado((string)dataReader["habilidadFK"], (string)dataReader["idiomaFK"], (int)dataReader["cantidad"]);
                        lista.Add(datos);
                    }
                }
            }

            return lista;
        }

        public List<DatosGraficoBarrasApilado> GetPasatiemposPorPais()
        {
            List<DatosGraficoBarrasApilado> lista = new List<DatosGraficoBarrasApilado>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT MP.pasatiempoFK, M.paisFK, COUNT(*) AS cantidad
                                    FROM [dbo].[Miembro] M
                                    JOIN [dbo].[MiembroPasatiempo] MP
	                                    ON M.usernamePK = MP.usernameFK
                                    WHERE	MP.pasatiempoFK IN (SELECT pasatiempoPK
						                                    FROM [Catalogo].[Pasatiempo])
                                    GROUP BY M.paisFK, MP.pasatiempoFK
                                    ORDER BY MP.pasatiempoFK, M.paisFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DatosGraficoBarrasApilado datos = new DatosGraficoBarrasApilado((string)dataReader["pasatiempoFK"], (string)dataReader["paisFK"], (int)dataReader["cantidad"]);
                        lista.Add(datos);
                    }
                }
            }

            return lista;
        }

        public List<DatosGraficoBarrasApilado> GetPastiemposPorIdioma()
        {
            List<DatosGraficoBarrasApilado> lista = new List<DatosGraficoBarrasApilado>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT  MP.pasatiempoFK, MI.idiomaFK, COUNT(*) AS cantidad
                                    FROM [dbo].[Miembro] M
                                    JOIN [dbo].[MiembroPasatiempo] MP
	                                    ON M.usernamePK = MP.usernameFK
                                    JOIN [dbo].[MiembroIdioma] MI
	                                    ON M.usernamePK = MI.usernameFK
                                    WHERE	MP.pasatiempoFK IN (SELECT pasatiempoPK
						                                    FROM [Catalogo].[Pasatiempo])
                                    GROUP BY MI.idiomaFK, MP.pasatiempoFK
                                    ORDER BY MP.pasatiempoFK, MI.idiomaFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DatosGraficoBarrasApilado datos = new DatosGraficoBarrasApilado((string)dataReader["pasatiempoFK"], (string)dataReader["idiomaFK"], (int)dataReader["cantidad"]);
                        lista.Add(datos);
                    }
                }
            }

            return lista;
        }

        public List<string> GetHabilidadesEstandarSinAsignar()
        {
            List<string> lista = new List<string>();
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT H.habilidadPK
                                    FROM [Catalogo].[Habilidad] H
                                    WHERE	H.habilidadPK NOT IN	(SELECT habilidadFK
							                                    FROM [dbo].[MiembroHabilidad])";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string habilidad = (string)dataReader["habilidadPK"];
                        lista.Add(habilidad);
                    }
                }
            }

            return lista;
        }

        public List<string> GetPasatiemposEstandarSinAsignar()
        {
            List<string> lista = new List<string>();
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT P.pasatiempoPK
                                    FROM [Catalogo].[Pasatiempo] P
                                    WHERE	P.pasatiempoPK NOT IN	(SELECT pasatiempoFK
								                                    FROM [dbo].[MiembroPasatiempo])";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string pasatiempo = (string)dataReader["pasatiempoPK"];
                        lista.Add(pasatiempo);
                    }
                }
            }

            return lista;
        }

        public List<DatosGraficoDona> GetMiembrosPorRol()
        {
            List<DatosGraficoDona> lista = new List<DatosGraficoDona>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT nombreRolFK, COUNT(*) AS cantidad
                                    FROM [dbo].[Miembro]
                                    WHERE nombreRolFK != 'Coordinador'
                                    GROUP BY nombreRolFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DatosGraficoDona datos = new DatosGraficoDona((string)dataReader["nombreRolFK"], (int)dataReader["cantidad"]);
                        lista.Add(datos);
                    }
                }
            }

            return lista;
        }

    }
}
