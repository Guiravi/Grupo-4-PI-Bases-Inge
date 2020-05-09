using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EnviarEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void TextBoxDestino_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBoxAsunto_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBoxMensaje_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ResetButton_Click(object sender, EventArgs e)
    {
        TextBoxMensaje.Text = string.Empty;
    }

    protected void SendButton_Click(object sender, EventArgs e)
    {

    }
}