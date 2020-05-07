using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Utilities;

public partial class CrearArticulo : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}


	protected void btnDescargar_Click(object sender, EventArgs e)
	{
		Utilidades.verPDF(this, "~/ArticulosPDF/articulo.pdf");
	}
}