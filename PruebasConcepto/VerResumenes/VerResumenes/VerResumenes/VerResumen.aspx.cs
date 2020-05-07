using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VerResumenes
{
    public partial class VerResumen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            labelTituloArticulo.Text = "Este es un título del artículo.";
            labelAutor.Text = "Kevin";
            labelTopicos.Text = "Ciencia";
            labelResumen.Text = "Este es un resumen";
        }

        protected void buttonVerArticulo_Click(object sender, EventArgs e)
        {

        }
    }
}