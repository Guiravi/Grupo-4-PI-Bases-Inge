using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;
using TheCoffeePlace.Utilities;



public partial class SubirArticulo : System.Web.UI.Page, IView_SubirArticulo
{
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

    public byte[] contenido
    {
        get { return fuArticulo.FileBytes; }
    }

    public int tipo
    {
        get { return 1; }
    }

    public String username
    {
        get { return lblUsername.Text; }
    }

	public String autor
	{
		get { return lblUsername.Text; }
	}
	public CheckBoxList checkBoxList
	{
		get { return chkblTopicos; }
		set { chkblTopicos = value; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
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


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblErrorArticulo.Visible = false;

        if (fuArticulo.HasFile)
        {
            if (Utilidades.EsTipoArchivo(fuArticulo.FileName, "docx") || Utilidades.EsTipoArchivo(fuArticulo.FileName, "DOCX"))
            {            
               ArticuloController artController = new ArticuloController();
               artController.GuardarArticulo(this);     
            }
            else
            {
                lblErrorArticulo.Text = "Error. Favor seleccione solo archivos de tipo .docx";
                lblErrorArticulo.Visible = true;
            }

        }
        else
        {
            lblErrorArticulo.Text = "Error. Favor escoger un documento";
            lblErrorArticulo.Visible = true;
        }
        
    }

    protected void gvAutor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAutor.PageIndex = e.NewPageIndex;
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
