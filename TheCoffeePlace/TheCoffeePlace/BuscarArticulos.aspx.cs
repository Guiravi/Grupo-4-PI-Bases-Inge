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

	public String topico
	{
		get { return txtTopico.Text; }
		set { txtTopico.Text = value; }
	}

	public GridView gridView
	{
		get { return gvArticulos; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{

	}

	protected void btnBuscar_Click(object sender, EventArgs e)
	{
		ArticuloController articuloController = new ArticuloController();
		articuloController.BuscarArticuloPorTopico(this);
	}
}