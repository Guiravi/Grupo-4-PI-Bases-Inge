using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MiPerfil : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCrearArticulo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CrearArticulo.aspx");
    }

    protected void btnEnviarMensaje_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/EnviarEmail.aspx");
    }
}