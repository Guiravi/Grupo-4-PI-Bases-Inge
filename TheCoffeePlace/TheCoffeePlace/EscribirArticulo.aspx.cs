using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;  
using TheCoffeePlace.Controllers;
using TheCoffeePlace.Utilities;

namespace TheCoffeePlace.Views
{
	public partial class EscribirArticulo : System.Web.UI.Page, IView_EscribirArticulo, IView_ImagenArticulo
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			ImagenArticuloController imArtController = new ImagenArticuloController(this);
			imArtController.ObtenerImagen();

			if (!IsPostBack)
			{
				TopicoController topicoController = new TopicoController();
				topicoController.SetTopicos(this);
				List<string> listaAutores = new List<string>();
				listaAutores.Add("Nombre del autor");
				ViewState["listaAutores"] = listaAutores;
				gvAutor.DataSource = listaAutores;
				gvAutor.DataBind();
			}

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

		public String autor
		{
			get
			{
				string autores = "";
				foreach (GridViewRow row in gvAutor.Rows)
				{
					autores += row.Cells[1].Text;
				}
				autores.TrimEnd(',');
				return autores;
			}
		}

		public CheckBoxList checkBoxList
		{
			get { return chkblTopicos; }
			set { chkblTopicos = value; }
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
			ArticuloController artController = new ArticuloController();
			artController.GuardarArticulo(this);
		}

		protected void btnSubir_Click(object sender, EventArgs e)
		{
			lblErrorImagen.Visible = false;

			if (fileupImagen.HasFile)
			{
				bool tipoArchivoValido = (Utilidades.EsTipoArchivo(fileupImagen.FileName, "jpg") || Utilidades.EsTipoArchivo(fileupImagen.FileName, "png") || Utilidades.EsTipoArchivo(fileupImagen.FileName, "JPG") || Utilidades.EsTipoArchivo(fileupImagen.FileName, "PNG"));
				if (tipoArchivoValido && EsNombreCortoImagen(fileupImagen.FileName))
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

		protected void gvAutor_RowEditing(object sender, GridViewEditEventArgs e)
		{
			gvAutor.EditIndex = e.NewEditIndex;
			gvAutor.DataSource = (List<string>)ViewState["listaAutores"];
			gvAutor.DataBind();
		}


		protected void gvAutor_RowUpdated(object sender, GridViewUpdateEventArgs e)
		{
		}

		protected void gvAutor_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			List<string> listaAutores = (List<string>)ViewState["listaAutores"];
			listaAutores[e.RowIndex] = (gvAutor.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
			gvAutor.DataSource = (List<string>)ViewState["listaAutores"];
			gvAutor.EditIndex = -1;
			gvAutor.DataBind();
		}

		protected void btnAgregarAutor_Click(object sender, EventArgs e)
		{
			List<string> listaAutores = (List<string>)ViewState["listaAutores"];
			listaAutores.Add(txtNuevoAutor.Text);
			gvAutor.DataSource = (List<string>)ViewState["listaAutores"];
			gvAutor.DataBind();
		}

		protected void gvAutor_RowDeleted(object sender, GridViewDeletedEventArgs e)
		{
		}

		protected void gvAutor_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			List<string> listaAutores = (List<string>)ViewState["listaAutores"];
			listaAutores.RemoveAt(e.RowIndex);
			gvAutor.DataSource = (List<string>)ViewState["listaAutores"];
			gvAutor.DataBind();
		}
	}
}

