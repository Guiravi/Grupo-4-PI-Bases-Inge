using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;


public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        lblAvisoError.Visible = false;
        CargarImagenesArticulo(1);
    }

    protected void BtnSubir_Click(object sender, EventArgs e)
    {
        lblAvisoError.Visible = false;

        if (fileUpImagen.HasFile && EsImagen(fileUpImagen.FileName) && EsNombreCortoImagen(fileUpImagen.FileName))
        {
            try
            {
                SqlConnection conn = new SqlConnection(GetConnectionString());
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
                
                string nombreArchivo = Convert.ToString(current_id) + fileUpImagen.FileName;

                fileUpImagen.SaveAs(Server.MapPath("~/Imagenes/" + nombreArchivo));

                cmd = new SqlCommand("INSERT INTO ImagenArticuloCorto (nombreImagen, Imagen, IdArticuloFK) VALUES (@nombreImagen, @Imagen, @IdArticuloCorto)", conn);
                cmd.Parameters.AddWithValue("@nombreImagen", nombreArchivo);
                cmd.Parameters.AddWithValue("@Imagen", "Imagenes/"+ nombreArchivo);
                cmd.Parameters.AddWithValue("@IdArticuloCorto", 1);

                cmd.ExecuteNonQuery();

                conn.Close();

                CargarImagenesArticulo(1);

            }
            catch (Exception excp){
                lblAvisoError.Text = excp.Message;
            }

        }
        else
        {
            if (!fileUpImagen.HasFile)
            {
                lblAvisoError.Text = "Error. Favor escoger una imagen";
            }

            lblAvisoError.Visible = true;
        }
    }

    protected void CargarImagenesArticulo(int idArticulo) {

        SqlConnection conn = new SqlConnection(GetConnectionString());
        conn.Open();
        SqlDataAdapter da;
        DataSet ds;

        da = new SqlDataAdapter("SELECT nombreImagen, imagen FROM ImagenArticuloCorto WHERE IdArticuloFK=" + Convert.ToString(idArticulo), conn);

        ds = new DataSet();

        da.Fill(ds);

        gridviewImagenes.DataSource = ds;

        gridviewImagenes.DataBind();

        conn.Close();

    }


    protected bool EsImagen(string nombreArchivo)
    {

        string extension = "";
        int contExt = nombreArchivo.Length - 1;
        for (int i = 0; i < 3; i++)
        {
            extension = nombreArchivo[contExt] + extension;
            contExt -= 1;
        }

        if (extension == "jpg" || extension == "png")
        {
            return true;
        }
        else
        {
            lblAvisoError.Text = "Error. Favor seleccione solo archivos de tipo .jpg o .png";
            return false;
        }

    }

    protected bool EsNombreCortoImagen(string nombreArchivo)
    {
        if (nombreArchivo.Count() < 30)
        {
            return true;
        }
        else
        {
            lblAvisoError.Text = "Error. Favor seleccione un archivo con nombre menor a 30 caracteres o cambie el nombre del archivo actual";
            return false;
        }


    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Sacar datos del archivo a eliminar del gridview
        int index = Convert.ToInt32(e.RowIndex);
        GridViewRow filaEliminar = gridviewImagenes.Rows[index];
        string nombreArchivo = filaEliminar.Cells[0].Text;

        //Eliminarlo de la base de datos
        SqlConnection conn = new SqlConnection(GetConnectionString());
        conn.Open();
        SqlCommand cmd;
        cmd = new SqlCommand("DELETE FROM ImagenArticuloCorto WHERE nombreImagen = @nombreImagen", conn);
        cmd.Parameters.AddWithValue("@nombreImagen", nombreArchivo);
        cmd.ExecuteNonQuery();
        conn.Close();

        //Eliminarlo de la carpeta de imagenes
        string rutaArchivo = Server.MapPath("~/Imagenes/" + nombreArchivo);
        File.Delete(rutaArchivo);

        Response.Redirect(Request.RawUrl);
    }

    static private string GetConnectionString()
    {
        // To avoid storing the connection string in your code,
        // you can retrieve it from a configuration file.
        return "Data Source=DESKTOP-3SS2QUU;Initial Catalog=PruebasConceptoPI;"
            + "Integrated Security=true;";
    }

}


