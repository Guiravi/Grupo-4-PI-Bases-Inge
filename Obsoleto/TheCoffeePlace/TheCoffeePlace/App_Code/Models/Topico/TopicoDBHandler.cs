﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for TopicoDBHandler
/// </summary>
/// 
namespace TheCoffeePlace.Models
{
	public class TopicoDBHandler
	{
		public TopicoDBHandler()
		{

		}

		public List<TopicoModel> ObtenerAllTopicos()
		{
			List<TopicoModel> topicos = new List<TopicoModel>();

			String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;
			String sqlString = "SELECT Topico.nombreTopicoPK FROM Topico";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(sqlString, connection))
				{
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						TopicoModel topicoActual = new TopicoModel((string)reader["nombreTopicoPK"]);
						topicos.Add(topicoActual);
					}
				}
			}
			return topicos;
		}

        public List<TopicoModel> ObtenerTopicosArticulo(int id)
        {
            List<TopicoModel> topicos = new List<TopicoModel>();

            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;
            String sqlString = "SELECT Topico.nombreTopicoPK FROM Topico JOIN TopicosArticulo ON idArticuloFK = @id";

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                using ( SqlCommand command = new SqlCommand(sqlString, connection) )
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    while ( reader.Read() )
                    {
                        TopicoModel topicoActual = new TopicoModel((string) reader["nombreTopicoPK"]);
                        topicos.Add(topicoActual);
                    }
                }
            }
            return topicos;
        }
    }
}