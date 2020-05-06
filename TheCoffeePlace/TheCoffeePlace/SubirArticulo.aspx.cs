using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;
using TheCoffeePlace.Utilities;



public partial class SubirArticulo : System.Web.UI.Page, IView_SubirArticulo
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

    public byte[] contenido
    {
        get { return fuArticulo.FileBytes; }
    }

    public int tipo
    {
        get { return 1; }
    }

    public String username
    {
        get { return lblUsername.Text; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        if (Utilidades.EsTipoArchivo(fuArticulo.FileName, "docx") || Utilidades.EsTipoArchivo(fuArticulo.FileName, "DOCX"))
        {
            ArticuloController artController = new ArticuloController();
            artController.GuardarArticulo(this);
        }
        else
        {
            //Mensaje de Error
        }
    }

}
