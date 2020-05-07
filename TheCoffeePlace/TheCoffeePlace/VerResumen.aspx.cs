using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;

public partial class VerResumen : System.Web.UI.Page, IView_VerResumen
{
    protected void Page_Load(object sender, EventArgs e)
    {
        idArticuloPK = Convert.ToInt32(Request.QueryString["idArticuloPK"]);

        ArticuloController artController = new ArticuloController();
        artController.ObtenerPaginaResumen(this);

        TopicoController topController = new TopicoController();
        topController.GetTopicosArticulo(this);

    }

    protected void buttonVerArticulo_Click(object sender, EventArgs e)
    {

    }

    private int _idArticuloPK;
    public int idArticuloPK
    {
        get { return _idArticuloPK; }
        set { _idArticuloPK = value; }
    }

    public string titulo {
        get { return labelTituloArticulo.Text; }
        set { labelTituloArticulo.Text = value; }
    }

    public string autor
    {
        get { return labelAutor.Text; }
        set { labelAutor.Text = value; }
    }

    public string topicos
    {
        get { return labelTopicos.Text; }
        set { labelTopicos.Text = value; }
    }

    public string resumen
    {
        get { return labelResumen.Text; }
        set { labelResumen.Text = value; }
    }
}