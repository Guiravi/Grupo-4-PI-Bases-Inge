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

        public List<CategoriaTopicoModel> GetCategoriasTopicosNoAsociadosRol(string rol)
        {
            List<CategoriaTopicoModel> lista = new List<CategoriaTopicoModel>();

            if (rol == "") //Todos lo roles
            {
                string connectionString = AppSettings.GetConnectionString();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    string sqlString = @"SELECT CT.nombreCategoriaPK, CT.nombreTopicoPK
                                        FROM [dbo].[CategoriaTopico] CT
                                        WHERE CT.nombreCategoriaPK + ' ' + CT.nombreTopicoPK NOT IN (SELECT ATT.nombreCategoriaFK + ' ' + ATT.nombreTopicoFK
															                                        FROM [dbo].[ArticuloTrataTopico] ATT
															                                        JOIN [dbo].[Articulo] A
																                                        ON ATT.idArticuloFK = A.articuloAID
															                                        JOIN [dbo].[MiembroAutorDeArticulo] MAA
																                                        ON A.articuloAID = MAA.idArticuloFK
															                                        JOIN [dbo].[Miembro] M
																                                        ON MAA.usernameMiemFK = M.usernamePK
															                                        WHERE A.estado = 'Publicado')";

                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                    {
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                CategoriaTopicoModel categoriaTopico = new CategoriaTopicoModel()
                                {
                                    nombreCategoriaPK = (string)dataReader["nombreCategoriaPK"],
                                    nombreTopicoPK = (string)dataReader["nombreTopicoPK"]
                                };

                                lista.Add(categoriaTopico);
                            }

                        }
                    }
                }
            }
            else
            {
                string connectionString = AppSettings.GetConnectionString();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    string sqlString = @"SELECT CT.nombreCategoriaPK, CT.nombreTopicoPK
                                        FROM [dbo].[CategoriaTopico] CT
                                        WHERE CT.nombreCategoriaPK + ' ' + CT.nombreTopicoPK NOT IN (SELECT ATT.nombreCategoriaFK + ' ' + ATT.nombreTopicoFK
															                                        FROM [dbo].[ArticuloTrataTopico] ATT
															                                        JOIN [dbo].[Articulo] A
																                                        ON ATT.idArticuloFK = A.articuloAID
															                                        JOIN [dbo].[MiembroAutorDeArticulo] MAA
																                                        ON A.articuloAID = MAA.idArticuloFK
															                                        JOIN [dbo].[Miembro] M
																                                        ON MAA.usernameMiemFK = M.usernamePK
															                                        WHERE A.estado = 'Publicado'
																                                        AND M.nombreRolFK = @rol)";

                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@rol", rol);
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                CategoriaTopicoModel categoriaTopico = new CategoriaTopicoModel()
                                {
                                    nombreCategoriaPK = (string)dataReader["nombreCategoriaPK"],
                                    nombreTopicoPK = (string)dataReader["nombreTopicoPK"]
                                };

                                lista.Add(categoriaTopico);
                            }

                        }

                    }                    
                }
            }

            return lista;
        }
    }
}
