using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;

public partial class EscribirArticulo : System.Web.UI.Page, IView_Articulo
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

	protected void Page_Load(object sender, EventArgs e)
	{
	
	}

	public void btnGuardar_Click(object sender, EventArgs e)
	{
		ArticuloController artController = new ArticuloController(this);
		artController.GuardarArticulo();
	}

}

