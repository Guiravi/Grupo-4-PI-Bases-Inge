using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ArticuloDBHandler
/// </summary>
namespace TheCoffeePlace.Models
{
    public class ArticuloDBHandler
    {
        public ArticuloDBHandler()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void SaveArticulo(ArticuloModel articulo)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;
            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                String sqlString = "INSERT INTO Articulo(titulo, resumen, tipo, contenido, fechaPublicacion, nombreAutor, usernameFK)";
                sqlString += "VALUES(@titulo, @resumen, @tipo, @contenido, @fechaPublicacion, @nombreAutor, @usernameFK)";

                using ( SqlCommand command = new SqlCommand(sqlString, connection) )
                {
                    command.Parameters.AddWithValue("@titulo", articulo.titulo);
                    command.Parameters.AddWithValue("@resumen", articulo.resumen);
                    command.Parameters.AddWithValue("@tipo", articulo.tipo);
                    command.Parameters.AddWithValue("@contenido", articulo.contenido);
                    command.Parameters.AddWithValue("@fechaPublicacion", articulo.fechaPublicacion);
                    command.Parameters.AddWithValue("@nombreAutor", articulo.nombreAutor);
                    command.Parameters.AddWithValue("@usernameFK", articulo.usernameFK);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public int ObtenerSiguienteId()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;
            SqlCommand cmd;
            int current_id;

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {

                connection.Open();

                cmd = new SqlCommand("SELECT IDENT_CURRENT ('Articulo')", connection);

                SqlDataReader identReader = cmd.ExecuteReader();

                current_id = 0;

                while ( identReader.Read() )
                {
                    current_id = Convert.ToInt32(identReader.GetValue(0));
                }

                identReader.Close();

            }

            return current_id;
        }

        public byte[] DescargarArticulo(int idArticulo)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;
            SqlCommand cmd;
            string contenido = "";

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {

                connection.Open();

                cmd = new SqlCommand("SELECT contenido FROM [dbo].[Articulo] WHERE idArticuloPK = @idArticulo", connection);

                cmd.Parameters.AddWithValue("@idArticulo", idArticulo);

                SqlDataReader identReader = cmd.ExecuteReader();

                while ( identReader.Read() )
                {
                    contenido = Convert.ToString(identReader.GetValue(0));
                }

                identReader.Close();

            }

            return System.Convert.FromBase64String(contenido);
        }

        public List<ArticuloModel> GetArticulosPorTopico(String topico)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT  Articulo.idArticuloPK, Articulo.titulo, Articulo.resumen, Articulo.tipo, Articulo.contenido, Articulo.fechaPublicacion, Articulo.nombreAutor, Articulo.usernameFK " +
                " FROM  Articulo JOIN TopicosArticulo ON " +
                        " Articulo.idArticuloPK = TopicosArticulo.idArticuloFK JOIN Topico ON " +
                        " TopicosArticulo.nombreTopicoFK = @topico", connection);

                cmd.Parameters.AddWithValue("@topico", topico);

                SqlDataReader identReader = cmd.ExecuteReader();

                List<ArticuloModel> art = new List<ArticuloModel>();
                while ( identReader.Read() )
                {
                    ArticuloModel am = new ArticuloModel((int) identReader.GetValue(0),
                        (String) identReader.GetValue(1), (String) identReader.GetValue(2),
                        (int) identReader.GetValue(3), (String) identReader.GetValue(4),
                        identReader.GetValue(5).ToString().Remove(identReader.GetValue(5).ToString().Length - 12, 12),
                        (String) identReader.GetValue(6), (String) identReader.GetValue(7));
                    art.Add(am);
                }

                identReader.Close();

                return art;
            }
        }

        public ArticuloModel GetInfoPaginaResumen(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;

            ArticuloModel articulo = null;

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Articulo WHERE Articulo.idArticuloPK = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader identReader = cmd.ExecuteReader();

                while ( identReader.Read() )
                {
                    articulo = new ArticuloModel((string) identReader["titulo"], (string) identReader["resumen"], (int) identReader["tipo"],
                        (string) identReader["contenido"], (string) identReader["fechaPublicacion"], (string) identReader["nombreAutor"],
                        (string) identReader["usernameFK"]);
                }

                identReader.Close();
            }
            return articulo;
        }
    }
}