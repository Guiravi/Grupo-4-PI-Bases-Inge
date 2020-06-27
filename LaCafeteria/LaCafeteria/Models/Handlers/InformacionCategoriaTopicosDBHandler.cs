using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class InformacionCategoriaTopicosDBHandler
    {
        public List<CategoriaTopicoModel> GetCategoriasYTopicos() {
            List<CategoriaTopicoModel> lista = new List<CategoriaTopicoModel>();

            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection sqlConnection = new SqlConnection(connectionString) )
            {

                string sqlString = @"SELECT nombreCategoriaPK, nombreTopicoPK
									FROM CategoriaTopico
                                    ORDER BY nombreCategoriaPK, nombreTopicoPK";

                sqlConnection.Open();
                using ( SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection) )
                {
                    using ( SqlDataReader dataReader = sqlCommand.ExecuteReader() )
                    {
                        while ( dataReader.Read() )
                        {
                            CategoriaTopicoModel categoriaTopico = new CategoriaTopicoModel() {
                                nombreCategoriaPK = (string) dataReader["nombreCategoriaPK"],
                                nombreTopicoPK = (string) dataReader["nombreTopicoPK"]
                            };

                            lista.Add(categoriaTopico);
                        }

                    }
                }
            }
            return lista;
        }
    }
}
