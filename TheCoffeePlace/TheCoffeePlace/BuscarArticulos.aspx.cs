using System;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BuscarArticulos : System.Web.UI.Page, IView_BuscarArticulos
{

	public String contenidoBusqueda
    {
		get { return txtBusqueda.Text; }
		set { txtBusqueda.Text = value; }
	}

	public GridView gridView
	{
		get { return gvArticulos; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
        ArticuloController articuloController = new ArticuloController();
        articuloController.BuscarTodosArticulos(this);
    }

    public bool chkbBCortoChecked
    {
        get { return chkbTipoCorto.Checked; }
    }

    public bool chkbBLargoChecked
    {
        get { return chkbTipoLargo.Checked; }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
	{
        ArticuloController articuloController = new ArticuloController();

        switch (radbtnltTopico.SelectedValue)
        {
            case "1":
                articuloController.BuscarArticuloPorTopico(this);
                break;
            case "2":
                articuloController.BuscarArticuloPorTitulo(this);
                break;
            case "3":

                break;
            default:
                break;

        }
    }


    protected void btnMostrarTodos_Click(object sender, EventArgs e)
    {
        ArticuloController articuloController = new ArticuloController();
        articuloController.BuscarTodosArticulos(this);
    }
}