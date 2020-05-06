using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;
//Este código interactúa con la vista del Miembro
public partial class MemberFaqs : System.Web.UI.Page, IViewMemberFaqs

{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //Se guardan valores de variables del código html de la vista
    public string Category {
        
        get { return category.Text; }
        set { category.Text = value; }
    }
    public string Question
    {

        get { return question.Text; }
        set { question.Text = value; }
    }
    public string Answer
    {

        get { return answer.Text; }
        set { answer.Text = value; }
    }
    //Este label va a ser manipulado por la clase controlador de la vista del miembro
    public Label Label {
        get { return lblFAQsHTML; }
        set { lblFAQsHTML = value; }
    }
    //Se llama al método de la clase controlador de la vista del miembro para que se actualizen los FAQs
    protected void BtnAddClick(object sender, EventArgs e) {
        MemberFaqController memberFaqController = new MemberFaqController(this);
        memberFaqController.UploadFaqs();
    }
}