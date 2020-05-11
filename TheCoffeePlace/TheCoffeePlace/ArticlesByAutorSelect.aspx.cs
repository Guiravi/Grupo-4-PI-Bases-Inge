using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;
//Este código interactúa con la vista de ArticlesByAutorSelect
public partial class ArticlesByAutorSelect : System.Web.UI.Page, IViewArticlesByAutorEdit

{
    protected void Page_Load(object sender, EventArgs e)
    {
        ArticuloController artController = new ArticuloController();
        artController.BuscarArticuloPorAutorEditar(this);
    }
    //Se guardan valores de variables del código html de la vista
    public string autor
    {

        get { return username.Text; }
        set { username.Text = value; }
    }

    public GridView gridView
    {
        get { return articles; }
    }


    protected void gvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        articles.PageIndex = e.NewPageIndex;

        articles.DataBind();
    }
}