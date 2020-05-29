using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models
{
	public class MiembroDBHandler
	{
		public List<MiembroModel> GetListaMiembros()
		{

			List<MiembroModel> listaMiembros = new List<MiembroModel>();

			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = @"SELECT usernamePK
										, email
										, nombre
										, apellido1
										, apellido2
										, fechaNacimiento
										, pais
										, estado
										, ciudad
										, rutaImagenPerfil
										, hobbies
										, habilidades
										, idiomas
										, informacionLaboral
										, meritos
										, activo
										, nombreRolFK
								FROM Miembro";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
					{
						while (dataReader.Read())
						{
							MiembroModel miembroAutor = new MiembroModel()
							{
								usernamePK = (string)dataReader["usernamePK"],
								email = (string)dataReader["email"],
								nombre = (string)dataReader["nombre"],
								apellido1 = (string)dataReader["apellido1"],
								apellido2 = (string)dataReader["apellido2"],
								fechaNacimiento = (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12),
								pais = (string)dataReader["pais"],
								estado = (string)dataReader["estado"],
								ciudad = (string)dataReader["ciudad"],
								rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],
								hobbies = (!DBNull.Value.Equals(dataReader["hobbies"])) ? (string)dataReader["hobbies"] : null,
								habilidades = (!DBNull.Value.Equals(dataReader["habilidades"])) ? (string)dataReader["habilidades"] : null,
								idiomas = (!DBNull.Value.Equals(dataReader["idiomas"])) ? (string)dataReader["idiomas"] : null,
								informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
								meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (int)dataReader["meritos"] : -1,
								activo = (!DBNull.Value.Equals(dataReader["activo"])) ? (bool)dataReader["activo"] : true,
								nombreRolFK = (string)dataReader["nombreRolFK"]
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
