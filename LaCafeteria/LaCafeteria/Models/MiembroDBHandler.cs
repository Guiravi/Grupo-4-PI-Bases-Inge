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
								email = (string)dataReader["usernamePK"],
								nombre = (string)dataReader["usernamePK"],
								apellido1 = (string)dataReader["usernamePK"],
								apellido2 = (string)dataReader["usernamePK"],
								fechaNacimiento = (string)dataReader["usernamePK"],
								pais = (string)dataReader["usernamePK"],
								estado = (string)dataReader["usernamePK"],
								ciudad = (string)dataReader["usernamePK"],
								rutaImagenPerfil = (string)dataReader["usernamePK"],
								hobbies = (string)dataReader["usernamePK"],
								habilidades = (string)dataReader["usernamePK"],
								idiomas = (string)dataReader["usernamePK"],
								informacionLaboral = (string)dataReader["usernamePK"],
								meritos = (int)dataReader["usernamePK"],
								activo = (bool)dataReader["usernamePK"],
								nombreRolFK = (string)dataReader["usernamePK"]
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
