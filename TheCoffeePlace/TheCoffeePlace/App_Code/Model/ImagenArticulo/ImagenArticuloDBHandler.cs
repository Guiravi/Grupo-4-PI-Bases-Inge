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
    public class ImageDBHandler
    {

        private SqlConnection conn;
        private void Connection()
        {
            string conString = ConfigurationManager.ConnectionStrings["Grupo4Conn"].ToString();
            conn = new SqlConnection(conString);
        }

        //***************** AGREGAR NUEVA IMAGEN *****************
        public bool AgregarImagen(ImagenArticulo aimodel)
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
        public List <ImagenArticulo> ObtenerImagen(int articleId) 
        {
            Connection();
            List<ImagenArticulo> imageList = new List<ImagenArticulo>();

            SqlCommand cmd = new SqlCommand("ObtenerImagen", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idArticuloPK", articleId);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            sda.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                imageList.Add(
                    new ImagenArticulo
                    {
                        idImagenPK = Convert.ToInt32(dr["idImagenPK"]),
                        rutaImagen = Convert.ToString(dr["rutaImagen"]),
                        idArticuloFK = Convert.ToInt32(dr["idArticuloFK"])

                    });
            }
            return imageList;
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
    }
}

