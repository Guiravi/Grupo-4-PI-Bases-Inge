using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class BuscadorMiembroDBHandler
    {
        public List<MiembroModel> GetListaMiembrosSolicitud(string usernameNucleoFK)
        {   
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
                                    FROM Miembro
                                    JOIN MiembroSolicitaSubirRangoNucleo
                                    ON Miembro.usernamePK = MiembroSolicitaSubirRangoNucleo.usernameMiembroFK
                                    WHERE MiembroSolicitaSubirRangoNucleo.estado IS NULL AND usernameNucleoFK = @usernameNucleoFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernameNucleoFK", usernameNucleoFK);
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
                                apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
                                fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                paisFK = (!DBNull.Value.Equals(dataReader["paisFK"])) ? (string)dataReader["paisFK"] : null,

                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],

                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (double)dataReader["meritos"] : 0,
                                activo = (bool)dataReader["activo"],
                                nombreRolFK = (string)dataReader["nombreRolFK"]
                            };

                            listaMiembros.Add(miembroAutor);
                        }
                    }
                }
            }

            return listaMiembros;
        }

        public string GetRango(string usernamePK)
        {
            string rango = "";
            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT nombreRolFK
									FROM Miembro
									WHERE @usernamePK =  usernamePK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {

                            rango = (string)dataReader["nombreRolFK"];


                        }
                    }
                }
            }
            return rango;
        }
        private InformacionMiembroDBHandler informacionMiembroDBHandler;

        public BuscadorMiembroDBHandler() {
            informacionMiembroDBHandler = new InformacionMiembroDBHandler();
        }

        public List<MiembroModel> GetListaMiembros()
        {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
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
                                usernamePK = (string) dataReader["usernamePK"],
                                email = (string) dataReader["email"],
                                nombre = (string) dataReader["nombre"],
                                apellido1 = (string) dataReader["apellido1"],
                                apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string) dataReader["apellido2"] : null,
                                fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string) dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                paisFK = (string) dataReader["paisFK"],
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string) dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string) dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string) dataReader["rutaImagenPerfil"],
                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string) dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (double) dataReader["meritos"] : 0,
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


        public List<MiembroModel> GetListaMiembrosDegradar()
        {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro
                                    WHERE nombreRolFK = 'Núcleo' OR nombreRolFK = 'Activo'";

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
                                apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
                                fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                paisFK = (string)dataReader["paisFK"],
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],
                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (double)dataReader["meritos"] : 0,
                                activo = (bool)dataReader["activo"],
                                nombreRolFK = (string)dataReader["nombreRolFK"]
                            };

                            listaMiembros.Add(miembroAutor);
                        }
                    }
                }
            }

            return listaMiembros;
        }


        public List<MiembroModel> GetListaNucleosSolicitud()
        {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro WHERE nombreRolFK = 'Núcleo' OR nombreRolFK = 'Coordinador' ";

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
                                apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
                                fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                paisFK = (!DBNull.Value.Equals(dataReader["paisFK"])) ? (string)dataReader["paisFK"] : null,
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],

                                informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
                                meritos = (!DBNull.Value.Equals(dataReader["meritos"])) ? (double)dataReader["meritos"] : 0,
                                activo = (bool)dataReader["activo"],
                                nombreRolFK = (string)dataReader["nombreRolFK"]
                            };

                            listaMiembros.Add(miembroAutor);
                        }
                    }
                }
            }

            return listaMiembros;
        }
        public List<MiembroModel> GetListaNucleos()
        {
            List<MiembroModel> listaMiembros = new List<MiembroModel>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, pais, estado, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro WHERE nombreRolFK = 'Núcleo'";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            MiembroModel miembroAutor = new MiembroModel()
                            {
                                usernamePK = (string) dataReader["usernamePK"],
                                email = (string) dataReader["email"],
                                nombre = (string) dataReader["nombre"],
                                apellido1 = (string) dataReader["apellido1"],
                                apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
                                fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string) dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                paisFK = (string) dataReader["paisFK"],
                                estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string) dataReader["estado"] : null,
                                ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string) dataReader["ciudad"] : null,
                                rutaImagenPerfil = (string) dataReader["rutaImagenPerfil"],
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

		public List<MiembroModel> GetListaMiembrosParaSolicitudRevision(int articuloAID)
		{
			List<MiembroModel> listaMiembrosParaSolictudRevision = new List<MiembroModel>();


			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, informacionLaboral, 
											meritos, activo, nombreRolFK
									FROM Miembro
									WHERE nombreRolFK = 'Núcleo' AND
									NOT EXISTS
									(SELECT 1 FROM NucleoRevisaArticulo WHERE usernamePK = usernameMiemFK AND @articuloAID = idArticuloFK) AND
									NOT EXISTS
									(SELECT 1 FROM NucleoPuedeSerRevisorDeArticulo WHERE usernamePK = usernameMiemFK AND @articuloAID = idArticuloFK)";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@articuloAID", articuloAID);
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
								apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
								fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
								paisFK = (string)dataReader["paisFK"],
								estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
								ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
								rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],
								informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
								meritos = (double) dataReader["meritos"],
								activo = (bool)dataReader["activo"],
								nombreRolFK = (string)dataReader["nombreRolFK"]
							};

							listaMiembrosParaSolictudRevision.Add(miembroAutor);
						}
					}
				}
			}

			return listaMiembrosParaSolictudRevision;
		}

		public List<MiembroModel> GetlistaMiembrosParaAsignarRevision(int articuloAID)
		{
			List<MiembroModel> listaMiembrosParaAsignarRevision = new List<MiembroModel>();


			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, informacionLaboral, 
											meritos, activo, nombreRolFK
									FROM Miembro
									WHERE nombreRolFK = 'Núcleo' AND 
									NOT EXISTS
									(SELECT 1 FROM NucleoRevisaArticulo WHERE usernamePK = usernameMiemFK AND @articuloAID = idArticuloFK)";

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@articuloAID", articuloAID);
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
								apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
								fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
								paisFK = (string)dataReader["paisFK"],
								estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
								ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
								rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],
								informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
								meritos = (double)dataReader["meritos"],
								activo = (bool)dataReader["activo"],
								nombreRolFK = (string)dataReader["nombreRolFK"]
							};

							listaMiembrosParaAsignarRevision.Add(miembroAutor);
						}
					}
				}
			}

			return listaMiembrosParaAsignarRevision;
		}

		public List<MiembroModel> GetListaMiembrosInteresados(int articuloAID)
		{
			List<MiembroModel> listaMiembrosInteresados = new List<MiembroModel>();

			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro WHERE nombreRolFK = 'Núcleo' AND
												 EXISTS (SELECT 1 FROM NucleoPuedeSerRevisorDeArticulo
													WHERE usernamePK = usernameMiemFK AND
													idArticuloFK = @articuloAID AND
													estado = 'Interesa')";
								

				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{	
					sqlCommand.Parameters.AddWithValue("@articuloAID", articuloAID);
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
								apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
								fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
								paisFK = (string)dataReader["paisFK"],
								estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
								ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
								rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],
								informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
								meritos = (double)dataReader["meritos"],
								activo = (bool)dataReader["activo"],
								nombreRolFK = (string)dataReader["nombreRolFK"]
							};

							listaMiembrosInteresados.Add(miembroAutor);
						}
					}
				}
			}

			return listaMiembrosInteresados;
		}

		public MiembroModel GetMiembro(string usernamePK) {
            MiembroModel miembro = null;

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string nombreUSP = "USP_GetMiembro";
            
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(nombreUSP, sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@usernamePK", usernamePK);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                miembro = new MiembroModel()
                                {
                                    usernamePK = (string) dataReader["usernamePK"],
                                    email = (string) dataReader["email"],
                                    nombre = (string) dataReader["nombre"],
                                    apellido1 = (string) dataReader["apellido1"],
                                    apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
                                    fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string) dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                    paisFK = (string) dataReader["paisFK"],
                                    estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string) dataReader["estado"] : null,
                                    ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string) dataReader["ciudad"] : null,
                                    rutaImagenPerfil = (string) dataReader["rutaImagenPerfil"],
                                    informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string) dataReader["informacionLaboral"] : null,
                                    meritos = (double) dataReader["meritos"],
                                    activo = (bool) dataReader["activo"],
                                    nombreRolFK = (string) dataReader["nombreRolFK"]
                                };

                            }
                            miembro.idiomas = informacionMiembroDBHandler.GetIdiomasMiembro(miembro.usernamePK);
                            miembro.pasatiempos = informacionMiembroDBHandler.GetPasatiemposMiembro(miembro.usernamePK);
                            miembro.habilidades = informacionMiembroDBHandler.GetHabilidadesMiembro(miembro.usernamePK);
                        }
                    }
                }
            }

            return miembro;
        }

        public MiembroModel GetMiembroCoordinador() {
            MiembroModel miembro = null;

            string connectionString = AppSettings.GetConnectionString();
            using ( SqlConnection sqlConnection = new SqlConnection(connectionString) )
            {

                string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro
									WHERE nombreRolFK =  'Coordinador'";

                sqlConnection.Open();
                using ( SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection) )
                {
                    using ( SqlDataReader dataReader = sqlCommand.ExecuteReader() )
                    {
                        if ( dataReader.HasRows )
                        {
                            while ( dataReader.Read() )
                            {
                                miembro = new MiembroModel()
                                {
                                    usernamePK = (string) dataReader["usernamePK"],
                                    email = (string) dataReader["email"],
                                    nombre = (string) dataReader["nombre"],
                                    apellido1 = (string) dataReader["apellido1"],
                                    apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string) dataReader["apellido2"] : null,
                                    fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string) dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
                                    paisFK = (string) dataReader["paisFK"],
                                    estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string) dataReader["estado"] : null,
                                    ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string) dataReader["ciudad"] : null,
                                    rutaImagenPerfil = (string) dataReader["rutaImagenPerfil"],
                                    informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string) dataReader["informacionLaboral"] : null,
                                    meritos = (double) dataReader["meritos"],
                                    activo = (bool) dataReader["activo"],
                                    nombreRolFK = (string) dataReader["nombreRolFK"]
                                };

                            }
                            miembro.idiomas = informacionMiembroDBHandler.GetIdiomasMiembro(miembro.usernamePK);
                            miembro.pasatiempos = informacionMiembroDBHandler.GetPasatiemposMiembro(miembro.usernamePK);
                            miembro.habilidades = informacionMiembroDBHandler.GetHabilidadesMiembro(miembro.usernamePK);
                        }
                    }
                }
            }
            return miembro;
        }

		public List<MiembroModel> GetListaMiembrosRevisores(int articuloAID)
		{
			List<MiembroModel> listaMiembrosRevisores = new List<MiembroModel>();

			string connectionString = AppSettings.GetConnectionString();
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{

				string sqlString = @"SELECT usernamePK, email, nombre, apellido1, apellido2, fechaNacimiento, paisFK, estado, ciudad, rutaImagenPerfil, 
											informacionLaboral, meritos, activo, nombreRolFK
									FROM Miembro 
									WHERE nombreRolFK = 'Núcleo' AND
								    EXISTS(SELECT 1 FROM NucleoRevisaArticulo
													WHERE usernamePK = usernameMiemFK AND
													idArticuloFK = @articuloAID)";


				sqlConnection.Open();
				using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
				{
					sqlCommand.Parameters.AddWithValue("@articuloAID", articuloAID);
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
								apellido2 = (!DBNull.Value.Equals(dataReader["apellido2"])) ? (string)dataReader["apellido2"] : null,
								fechaNacimiento = (!DBNull.Value.Equals(dataReader["fechaNacimiento"])) ? (string)dataReader["fechaNacimiento"].ToString().Remove(dataReader["fechaNacimiento"].ToString().Length - 12, 12) : null,
								paisFK = (string)dataReader["paisFK"],
								estado = (!DBNull.Value.Equals(dataReader["estado"])) ? (string)dataReader["estado"] : null,
								ciudad = (!DBNull.Value.Equals(dataReader["ciudad"])) ? (string)dataReader["ciudad"] : null,
								rutaImagenPerfil = (string)dataReader["rutaImagenPerfil"],
								informacionLaboral = (!DBNull.Value.Equals(dataReader["informacionLaboral"])) ? (string)dataReader["informacionLaboral"] : null,
								meritos = (double)dataReader["meritos"],
								activo = (bool)dataReader["activo"],
								nombreRolFK = (string)dataReader["nombreRolFK"]
							};

							listaMiembrosRevisores.Add(miembroAutor);
						}
					}
				}
			}

			return listaMiembrosRevisores;
		}

	}
}
