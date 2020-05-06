using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for ImageDBHandle
/// </summary>
/// 
namespace TheCoffeePlace.Models
{
    public class ImagenArticuloDBHandler
    {

        private SqlConnection conn;
        private void Connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ToString();
            conn = new SqlConnection(conString);
        }

        //***************** AGREGAR NUEVA IMAGEN *****************
        public bool AgregarImagen(ImagenArticuloModel aimodel)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("AgregarNuevaImagen", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@rutaImagen", aimodel.rutaImagen);
            cmd.Parameters.AddWithValue("@idArticuloFK", aimodel.idArticuloFK);

            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        //***************** OBTENER IMAGEN *****************
        public DataSet ObtenerImagen(int articleId) 
        {
            Connection();
            SqlCommand cmd = new SqlCommand("ObtenerImagen", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idArticuloFK", articleId);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            conn.Open();
            sda.Fill(ds);
            conn.Close();

            return ds;
        }

        //***************** BORRAR IMAGEN *****************
        public bool BorrarImagen(int imagenId)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("BorrarImagen", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idImagenPK", imagenId);

            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        //***************** OBTENER SIGUIENTE ID IMAGEN *****************
            
        public int ObtenerSiguienteId()
        {
            Connection();
            SqlCommand cmd;

            conn.Open();

            cmd = new SqlCommand("SELECT IDENT_CURRENT ('ImagenArticuloCorto')", conn);

            SqlDataReader identReader = cmd.ExecuteReader();

            int current_id = 0;

            while (identReader.Read())
            {
                current_id = Convert.ToInt32(identReader.GetValue(0));
            }

            identReader.Close();

            conn.Close();

            return current_id;
        }

        public string ObtenerRutaImagen(int idImagen)
        {
            Connection();
            SqlCommand cmd;
            conn.Open();

            cmd = new SqlCommand("SELECT rutaImagen FROM [dbo].[ImagenArticuloCorto] WHERE idImagenPK = @idImagenPK", conn);

            cmd.Parameters.AddWithValue("@idImagenPK", idImagen);

            SqlDataReader identReader = cmd.ExecuteReader();

            string ruta = "";

            while (identReader.Read())
            {
                ruta = Convert.ToString(identReader.GetValue(0));
            }

            identReader.Close();

            conn.Close();

            return ruta;
        }
    }
}

