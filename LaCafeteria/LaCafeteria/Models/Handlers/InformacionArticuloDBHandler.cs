using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class InformacionArticuloDBHandler
    {
        public List<MiembroModel> GetAutoresDeArticulo(int id) {
            List<MiembroModel> autores = new List<MiembroModel>();
            string connectionString = AppSettings.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string nombreUSP = "USP_GetAutoresDeArticulo";
                SqlCommand command = new SqlCommand(nombreUSP, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idArticuloPK", id);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MiembroModel autor = new MiembroModel()
                    {
                        usernamePK = (string)reader["usernamePK"],
                        nombre = (string)reader["nombre"],
                        apellido1 = (string)reader["apellido1"],
                        apellido2 = (!DBNull.Value.Equals(reader["apellido2"])) ? (string)reader["apellido2"] : null,
                    };
                    autores.Add(autor);
                }

                reader.Close();
            }
            return autores;
        }

        public ArticuloModel GetInformacionArticuloModel(int id) {
            string connectionString = AppSettings.GetConnectionString();

            ArticuloModel articulo = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Articulo WHERE Articulo.articuloAID = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    articulo = new ArticuloModel()
                    {
                        articuloAID = id,
                        titulo = (string)reader["titulo"],
                        tipo = (string)reader["tipo"],
                        fechaPublicacion = reader["fechaPublicacion"].ToString().Remove(reader["fechaPublicacion"].ToString().Length - 12, 12),
                        resumen = (string)reader["resumen"],
                        contenido = (string)reader["contenido"],
                        estado = (string)reader["estado"],
                        visitas = (int)reader["visitas"],
                        puntajeTotalRev = (!DBNull.Value.Equals(reader["puntajeTotalRev"])) ? (double?)reader["puntajeTotalRev"] : null,
                        calificacionTotalMiem = (int)reader["calificacionTotalMiem"]
                    };
                }

                reader.Close();
            }
            return articulo;
        }

        public List<Tuple<string, string, double, string>> GetRevisiones(int id) {
            List<Tuple<string, string, double, string>> revisiones = new List<Tuple<string, string, double, string>>();

            String connectionString = AppSettings.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT M.usernamePK, M.nombre, M.apellido1, M.apellido2, NRA.puntaje, NRA.comentarios FROM Miembro M " +
                    "JOIN NucleoRevisaArticulo NRA " +
                    "ON M.usernamePK = NRA.usernameMiemFK " +
                    "JOIN Articulo A " +
                    "ON NRA.idArticuloFK = A.articuloAID " +
                    "WHERE A.articuloAID = @id", connection);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string username = reader["usernamePK"].ToString();
                    string nombreRevisor = reader["nombre"].ToString() + " " + reader["apellido1"].ToString() + " " + reader["apellido2"].ToString();
                    double puntaje = (double)reader["puntaje"];
                    string comentarios = reader["comentarios"].ToString();
                    revisiones.Add(Tuple.Create(username, nombreRevisor, puntaje, comentarios));
                }

                reader.Close();

                return revisiones;
            }
        }

        public List<string> GetRevisoresDeArticulo(int id) {
            List<string> listaRevisores = new List<string>();

            String connectionString = AppSettings.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT nombre, apellido1, apellido2 FROM Miembro M " +
                    "JOIN NucleoRevisaArticulo NRA " +
                    "ON M.usernamePK = NRA.usernameMiemFK " +
                    "JOIN Articulo A " +
                    "ON NRA.idArticuloFK = A.articuloAID " +
                    "WHERE A.articuloAID = @id", connection);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nombreRevisor = reader["nombre"].ToString() + " " + reader["apellido1"].ToString() + " " + reader["apellido2"].ToString();
                    listaRevisores.Add(nombreRevisor);
                }

                reader.Close();

                return listaRevisores;
            }
        }

        public byte[] GetBinario(int idArticulo) {
            String connectionString = AppSettings.GetConnectionString();
            SqlCommand cmd;
            string contenido = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                cmd = new SqlCommand("SELECT contenido " +
                    " FROM [dbo].[Articulo] " +
                    " WHERE articuloAID = @idArticulo;", connection);

                cmd.Parameters.AddWithValue("@idArticulo", idArticulo);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contenido = Convert.ToString(reader.GetValue(0));
                }

                reader.Close();

            }

            return System.Convert.FromBase64String(contenido);
        }

        public List<CategoriaTopicoModel> GetCategoriasTopicosArticulo(int id) {
            List<CategoriaTopicoModel> categoriaTopicos = new List<CategoriaTopicoModel>();

            String connectionString = AppSettings.GetConnectionString();

            String nombreUSP = "USP_GetCategoriasTopicosArticulo";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(nombreUSP, connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idArticuloPK", id);


                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CategoriaTopicoModel categoriaTopico = new CategoriaTopicoModel()
                        {
                            nombreCategoriaPK = (string)reader["nombreCategoriaFK"],
                            nombreTopicoPK = (string)reader["nombreTopicoFK"]
                        };
                        categoriaTopicos.Add(categoriaTopico);
                    }
                }
            }
            return categoriaTopicos;
        }

        public int GetCalificacionMiembro(string username, int idArticulo) {
            int calificacion = 10;

            string connectionString = AppSettings.GetConnectionString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string consulta = "SELECT MCA.calificacion " +
                    "FROM Miembro M " +
                    "JOIN MiembroCalificaArticulo MCA " +
                        "ON M.usernamePK = MCA.usernameMiemFK " +
                    "JOIN Articulo A " +
                        "ON MCA.idArticuloFK = A.articuloAID " +
                    "WHERE MCA.usernameMiemFK = @user " +
                        "AND MCA.idArticuloFK = @id;";

                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand(consulta, sqlConnection))
                {

                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@id", idArticulo);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            calificacion = reader.GetInt32(0);
                        }
                    }
                }
            }

            return calificacion;
        }

        public List<DatosGraficoDona> GetArticulosPorRol()
        {
            List<DatosGraficoDona> lista = new List<DatosGraficoDona>();

            string connectionString = AppSettings.GetConnectionString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {

                string sqlString = @"SELECT M.nombreRolFK, COUNT(DISTINCT MAA.idArticuloFK) AS cantidad
                                    FROM [dbo].[Miembro] M
                                    JOIN [dbo].[MiembroAutorDeArticulo] MAA
	                                    ON M.usernamePK = MAA.usernameMiemFK
                                    JOIN [dbo].[Articulo] A
	                                    ON MAA.idArticuloFK = A.articuloAID
                                    WHERE A.estado = 'Publicado'
                                    GROUP BY M.nombreRolFK";

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DatosGraficoDona datos = new DatosGraficoDona((string)dataReader["nombreRolFK"], (int)dataReader["cantidad"]);
                        lista.Add(datos);
                    }
                }
            }

            return lista;
        }

        public List<DatosTablaCategoriaTopicos> GetDatosTablaCategoriaTopicosPorRol(string rol)
        {
            List<DatosTablaCategoriaTopicos> listaDatos = new List<DatosTablaCategoriaTopicos>();

            if (rol == "") //Todos los roles
            {
                string connectionString = AppSettings.GetConnectionString();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    string sqlString = @"SELECT ATT.nombreCategoriaFK, ATT.nombreTopicoFK, COUNT(DISTINCT MAA.idArticuloFK) AS cantidad, SUM(DISTINCT A.visitas) AS visitas, AVG(DISTINCT A.puntajeTotalRev) AS puntajeProm
                                        FROM [dbo].[Miembro] M
                                        JOIN [dbo].[MiembroAutorDeArticulo] MAA
	                                        ON M.usernamePK = MAA.usernameMiemFK
                                        JOIN [dbo].[Articulo] A
	                                        ON MAA.idArticuloFK = A.articuloAID
                                        JOIN [dbo].[ArticuloTrataTopico] ATT
	                                        ON A.articuloAID = ATT.idArticuloFK
                                        WHERE A.estado = 'Publicado'
                                        GROUP BY ATT.nombreCategoriaFK, ATT.nombreTopicoFK
                                        ORDER BY ATT.nombreCategoriaFK, ATT.nombreTopicoFK";

                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                    {
                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        while (dataReader.Read())
                        {
                            DatosTablaCategoriaTopicos datos = new DatosTablaCategoriaTopicos("", 
                                (string)dataReader["nombreCategoriaFK"], 
                                (string)dataReader["nombreTopicoFK"], 
                                (int)dataReader["cantidad"], 
                                (int)dataReader["visitas"], 
                                (double)dataReader["puntajeProm"]);
                            listaDatos.Add(datos);
                        }
                    }
                }
            }
            else
            {
                string connectionString = AppSettings.GetConnectionString();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    string sqlString = @"SELECT M.nombreRolFK, ATT.nombreCategoriaFK, ATT.nombreTopicoFK, COUNT(DISTINCT MAA.idArticuloFK) AS cantidad, SUM(DISTINCT A.visitas) AS visitas, AVG(DISTINCT A.puntajeTotalRev) AS puntajeProm
                                        FROM [dbo].[Miembro] M
                                        JOIN [dbo].[MiembroAutorDeArticulo] MAA
	                                        ON M.usernamePK = MAA.usernameMiemFK
                                        JOIN [dbo].[Articulo] A
	                                        ON MAA.idArticuloFK = A.articuloAID
                                        JOIN [dbo].[ArticuloTrataTopico] ATT
	                                        ON A.articuloAID = ATT.idArticuloFK
                                        WHERE A.estado = 'Publicado'
		                                        AND M.nombreRolFK = @rol
                                        GROUP BY M.nombreRolFK, ATT.nombreCategoriaFK, ATT.nombreTopicoFK
                                        ORDER BY M.nombreRolFK, ATT.nombreCategoriaFK, ATT.nombreTopicoFK";

                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@rol", rol);
                        SqlDataReader dataReader = sqlCommand.ExecuteReader();
                        while (dataReader.Read())
                        {
                            DatosTablaCategoriaTopicos datos = new DatosTablaCategoriaTopicos((string)dataReader["nombreRolFK"], 
                                (string)dataReader["nombreCategoriaFK"],
                                (string)dataReader["nombreTopicoFK"],
                                (int)dataReader["cantidad"],
                                (int)dataReader["visitas"],
                                (double)dataReader["puntajeProm"]);
                            listaDatos.Add(datos);
                        }
                    }
                }
            }


            return listaDatos;
        }
    }
}
