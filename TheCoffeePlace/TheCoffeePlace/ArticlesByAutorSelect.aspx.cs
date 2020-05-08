using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;
//Este código interactúa con la vista de ArticlesByAutorSelect
public partial class MemberFaqs : System.Web.UI.Page, IViewArticlesByAutorSelect

{
    protected void Page_Load(object sender, EventArgs e)
    {
        ArticuloController artController = new ArticuloController();
    }
    //Se guardan valores de variables del código html de la vista
    public string Autor
    {

        get { return lblUsername.Text; }
        set { lblUsername.Text = value; }
    }
    public string Titulo {
        
        get { return title.Text; }
        set { title.Text = value; }
    }

    
    //Se llama al método de la clase controlador de la vista del miembro para que se actualizen los FAQs
    protected void BtnAddClick(object sender, EventArgs e) {
        //MemberFaqController memberFaqController = new MemberFaqController(this);
        //memberFaqController.UploadFaqs();
    }
}