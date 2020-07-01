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

        public List<string> GetHabilidadesCatalogo()
        {

            List<string> habilidades = new List<string>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT habilidadPK
									FROM Catalogo.Habilidad";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            habilidades.Add((string)dataReader["habilidadPK"]);
                        }

                    }
                }

            }
            return habilidades;
        }

        public List<string> GetPasatiemposCatalogo()
        {

            List<string> pasatiempos = new List<string>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT pasatiempoPK
									FROM Catalogo.Pasatiempo";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            pasatiempos.Add((string)dataReader["pasatiempoPK"]);
                        }

                    }
                }

            }
            return pasatiempos;
        }

        public List<string> GetPaisesCatalogo()
        {

            List<string> paises = new List<string>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT paisPK
									FROM Catalogo.Pais";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            paises.Add((string)dataReader["paisPK"]);
                        }

                    }
                }

            }
            return paises;
        }
    }
}
