using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using LaCafeteria.Utilidades;
/// <summary>
/// Summary description for TopicoDBHandler
/// </summary>
/// 
namespace LaCafeteria.Models
{
	public class TopicoDBHandler
	{
		public List<TopicoModel> GetListaTopicos()
		{
			List<TopicoModel> topicos = new List<TopicoModel>();

			String connectionString = AppSettings.GetConnectionString();
			String sqlString = "SELECT Topico.nombrePK FROM Topico";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(sqlString, connection))
				{
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						TopicoModel topicoActual = new TopicoModel()
						{
							nombre = (string)reader["nombrePK"]
						};

                        topicos.Add(topicoActual);
                    }

					reader.Close();
				}
			}
			return topicos;
		}


    }

}