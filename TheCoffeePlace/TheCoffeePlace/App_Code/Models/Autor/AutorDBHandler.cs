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
}