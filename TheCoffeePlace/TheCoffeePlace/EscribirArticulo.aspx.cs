using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;
using TheCoffeePlace.Utilities;

public partial class EscribirArticulo : System.Web.UI.Page, IView_EscribirArticulo, IView_ImagenArticulo
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ImagenArticuloController imArtController = new ImagenArticuloController(this);
        imArtController.ObtenerImagen();
    }

    public string titulo
	{	
		get { return txtTituloArticulo.Text; }
		set { txtTituloArticulo.Text = value; }
	}

	public string resumen
	{
		get { return txtResumen.Text; }
		set { txtResumen.Text = value; }
	}

	public string contenido
	{
		get { return ftxtEditor.Text; }
		set { ftxtEditor.Text = value; }
	}

    public int tipo
	{
		get { return 0; }
	}

	public String username
	{
		get { return lblUsername.Text; }
	}

    public string nombreArchivoFileUpImagen
    {
        get { return fileupImagen.FileName; }
    }

    public byte[] getContenidoFileUpImagen()
    {
        return fileupImagen.FileBytes;
    }

    public void setContenidoGridViewImagenes(DataSet ds)
    {
        gridviewImagenes.DataSource = ds;
        gridviewImagenes.DataBind();
    }

	protected void btnGuardar_Click(object sender, EventArgs e)
	{
		// Se crea el ArticuloController(this)
		ArticuloController artController = new ArticuloController();
		artController.GuardarArticulo(this);
	}

    protected void btnSubir_Click(object sender, EventArgs e)
    {
        lblErrorImagen.Visible = false;

        if (fileupImagen.HasFile)
        {
            bool tipoArchivoValido = (Utilidades.EsTipoArchivo(fileupImagen.FileName, "jpg") || Utilidades.EsTipoArchivo(fileupImagen.FileName, "png") || Utilidades.EsTipoArchivo(fileupImagen.FileName, "JPG") || Utilidades.EsTipoArchivo(fileupImagen.FileName, "PNG"));
            if ( tipoArchivoValido && EsNombreCortoImagen(fileupImagen.FileName))
            {
                ImagenArticuloController imArtController = new ImagenArticuloController(this);
                imArtController.GuardarImagen();
                imArtController.ObtenerImagen();
            }
        }
        else
        {
            lblErrorImagen.Text = "Error. Favor escoger una imagen";
            lblErrorImagen.Visible = true;

        }  
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        GridViewRow filaEliminar = gridviewImagenes.Rows[index];
        int idImagen = Convert.ToInt32(filaEliminar.Cells[0].Text);

        ImagenArticuloController imArtController = new ImagenArticuloController(this);
        imArtController.BorrarImagen(idImagen);
        imArtController.ObtenerImagen();
    }

    private bool EsImagen(string nombreArchivo)
    {

        string extension = "";
        int contExt = nombreArchivo.Length - 1;
        for (int i = 0; i < 3; i++)
        {
            extension = nombreArchivo[contExt] + extension;
            contExt -= 1;
        }

        if (extension == "jpg" || extension == "png" || extension == "JPG" || extension == "PNG")
        {
            return true;
        }
        else
        {
            lblErrorImagen.Text = "Error. Favor seleccione solo archivos de tipo .jpg o .png";
            lblErrorImagen.Visible = true;
            return false;
        }

    }

    private bool EsNombreCortoImagen(string nombreArchivo)
    {
        if (nombreArchivo.Length < 30)
        {
            return true;
        }
        else
        {
            lblErrorImagen.Text = "Error. Favor seleccione un archivo con nombre menor a 30 caracteres o cambie el nombre del archivo que quizo ingresar";
            lblErrorImagen.Visible = true;
            return false;
        }


    }

}

