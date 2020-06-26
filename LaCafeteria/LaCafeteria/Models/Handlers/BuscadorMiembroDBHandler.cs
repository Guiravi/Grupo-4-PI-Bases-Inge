using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class BuscadorMiembroDBHandler
    {
        public List<MiembroModel> GetListaMiembros() {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection sqlConnection = new SqlConnection(connectionString) )
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, pais, estado, ciudad, rutaImagenPerfil, 
											hobbies, habilidades, idiomas, informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro";

                sqlConnection.Open();
                using ( SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection) )
                {
                    using ( SqlDataReader dataReader = sqlCommand.ExecuteReader() )
                    {
                        while ( dataReader.Read() )
                        {
                            MiembroModel miembroAutor = new MiembroModel()
                            {
                                usernamePK = (string) dataReader["usernamePK"],
                                email = (string) dataReader["email"],
                                nombre = (string) dataReader["nombre"],
                                apellido1 = (string) dataReader["apellido1"],
                                apellido2 = (string) dataReader["apellido2"],
                                fechaNacimiento = (string) dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12),
                                pais = (!DBNull.Value.Equals(dataReader["pais"])) ? (string) dataReader["pais"] : null,
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string) dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string) dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string) dataReader["rutaImagenPerfil"],
                                hobbies = (!DBNull.Value.Equals(dataReader["hobbies"])) ? (string) dataReader["hobbies"] : null,
                                habilidades = (!DBNull.Value.Equals(dataReader["habilidades"])) ? (string) dataReader["habilidades"] : null,
                                idiomas = (!DBNull.Value.Equals(dataReader["idiomas"])) ? (string) dataReader["idiomas"] : null,
                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string) dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (int) dataReader["meritos"] : 0,
                                activo = (bool) dataReader["activo"],
                                nombreRolFK = (string) dataReader["nombreRolFK"]
                            };

                            listaMiembros.Add(miembroAutor);
                        }
                    }
                }
            }

            return listaMiembros;
        }

        public List<MiembroModel> GetListaNucleos() {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection sqlConnection = new SqlConnection(connectionString) )
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, pais, estado, ciudad, rutaImagenPerfil, 
											hobbies, habilidades, idiomas, informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro WHERE nombreRolFK = 'Núcleo'";

                sqlConnection.Open();
                using ( SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection) )
                {
                    using ( SqlDataReader dataReader = sqlCommand.ExecuteReader() )
                    {
                        while ( dataReader.Read() )
                        {
                            MiembroModel miembroAutor = new MiembroModel()
                            {
                                usernamePK = (string) dataReader["usernamePK"],
                                email = (string) dataReader["email"],
                                nombre = (string) dataReader["nombre"],
                                apellido1 = (string) dataReader["apellido1"],
                                apellido2 = (string) dataReader["apellido2"],
                                fechaNacimiento = (string) dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12),
                                pais = (!DBNull.Value.Equals(dataReader["pais"])) ? (string) dataReader["pais"] : null,
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string) dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string) dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string) dataReader["rutaImagenPerfil"],
                                hobbies = (!DBNull.Value.Equals(dataReader["hobbies"])) ? (string) dataReader["hobbies"] : null,
                                habilidades = (!DBNull.Value.Equals(dataReader["habilidades"])) ? (string) dataReader["habilidades"] : null,
                                idiomas = (!DBNull.Value.Equals(dataReader["idiomas"])) ? (string) dataReader["idiomas"] : null,
                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string) dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (int) dataReader["meritos"] : 0,
                                activo = (bool) dataReader["activo"],
                                nombreRolFK = (string) dataReader["nombreRolFK"]
                            };

                            listaMiembros.Add(miembroAutor);
                        }
                    }
                }
            }

            return listaMiembros;
        }
    }
}
