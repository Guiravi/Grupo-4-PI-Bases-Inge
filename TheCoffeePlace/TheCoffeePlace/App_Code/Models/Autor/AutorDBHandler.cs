using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AutorDBHandler
/// </summary>
namespace TheCoffeePlace.Models {
    public class AutorDBHandler
    {
        private SqlConnection conn;
        private void Connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ToString();
            conn = new SqlConnection(conString);
        }

        public AutorDBHandler()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public String GetFullName(String usernamePK)
        {
            Connection();
            // TODO: Cambiar nombre del stored procedure
            SqlCommand cmd = new SqlCommand("ObtenerNombreCompletoAutor", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@usernamePK", usernamePK);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            sda.Fill(dt);
            conn.Close();

            return Convert.ToString(dt.Rows[0][0]);
        }

        public List<AutorModel> getListaAutores()
        {
            List<AutorModel> listaAutores = new List<AutorModel>();

            String connectionString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ConnectionString;
            conn = new SqlConnection(connectionString);
            String sqlString = "SELECT Autor.usernamePK, Autor.email, Autor.nombre, Autor.apellido1, Autor.apellido2 FROM Autor ORDER BY Autor.nombre ASC";

            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            SqlDataReader identReader = cmd.ExecuteReader();

            while(identReader.Read())
            {
                AutorModel autorModel = new AutorModel((string) identReader["usernamePK"], (string) identReader["email"],
                    (string) identReader["nombre"], (string) identReader["apellido1"], (string) identReader["apellido2"]);
                listaAutores.Add(autorModel);
            }

            identReader.Close();
            conn.Close();

            return listaAutores;
        }
    }
}