using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;
//Este código interactúa con la vista del Miembro
public partial class UserFaqs : System.Web.UI.Page, IViewUserFaqs

{
    //Se llama al método de la clase controlador de la vista del usuaruio para que el usuario pueda ver los FAQs
    protected void Page_Load(object sender, EventArgs e)
    {
        UserFaqController userFaqController = new UserFaqController(this);
        userFaqController.SeeFaqs();
    }
    //Se guardan valores de variables del código html de la vista

    //Este label va a ser manipulado por la clase controlador de la vista del usuario
    public Label Label
    {
        get { return lblFAQs; }
        set { lblFAQs = value; }
    }


}