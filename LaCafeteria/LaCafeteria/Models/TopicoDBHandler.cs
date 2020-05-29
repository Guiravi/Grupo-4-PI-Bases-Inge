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
        public List<TopicoModel> ObtenerAllTopicos() {
            List<TopicoModel> topicos = new List<TopicoModel>();

            String connectionString = AppSettings.getConnectionString();
            String sqlString = "SELECT Topico.nombreTopicoPK FROM Topico";
            using ( SqlConnection connection = new SqlConnection(connectionString) ) {
                using ( SqlCommand command = new SqlCommand(sqlString, connection) ) {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while ( reader.Read() ) {
                        TopicoModel topicoActual = new TopicoModel() {
                            nombre = (string) reader["nombreTopicoPK"]
                        };

                        topicos.Add(topicoActual);
                    }

                    reader.Close();
                }
            }
            return topicos;
        }

        public List<TopicoModel> ObtenerTopicosArticulo(int id) {
            List<TopicoModel> topicos = new List<TopicoModel>();

            String connectionString = AppSettings.getConnectionString();

            String sqlString = "SELECT ArticuloTrataTopico.nombreTopicoFK FROM ArticuloTrataTopico WHERE ArticuloTrataTopico.idArticuloFK = @id";

            using ( SqlConnection connection = new SqlConnection(connectionString) ) {
                using ( SqlCommand command = new SqlCommand(sqlString, connection) ) {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    while ( reader.Read() ) {
                        TopicoModel topicoActual = new TopicoModel() {
                            nombre = (string) reader["nombreTopicoFK"]
                        };
                        topicos.Add(topicoActual);
                    }
                }
            }
            return topicos;
        }
    }

}