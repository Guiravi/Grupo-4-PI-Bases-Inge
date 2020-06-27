using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class InformacionCatalogosDBHandler
    {
        public List<string> GetIdiomasCatalogo() {

            List<string> idiomas = new List<string>();

            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection sqlConnection = new SqlConnection(connectionString) )
            {

                string sqlString = @"SELECT idiomaPK
									FROM Catalogo.Idioma";

                sqlConnection.Open();
                using ( SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection) )
                {
                    using ( SqlDataReader dataReader = sqlCommand.ExecuteReader() )
                    {
                        while ( dataReader.Read() )
                        {
                            idiomas.Add((string) dataReader["idiomaPK"]);
                        }

                    }
                }

            }
            return idiomas;
        }
    }
}
