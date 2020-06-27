using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;
using System.IO;

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


    private int _idArticuloPK;
    private int _tipo;
    public int idArticuloPK
    {
        get { return _idArticuloPK; }
        set { _idArticuloPK = value; }
    }

    public int tipo
    {
        get { return _tipo; }
        set { _tipo = value; }
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

    public void setArticuloCorto(string contenido)
    {
        artCorto.InnerHtml = contenido;
        artCorto.Visible = true;
        btnDescargar.Visible = true;
    }

    protected void btnVerArticulo_Click(object sender, EventArgs e)
    {
        ArticuloController articuloController = new ArticuloController();
        articuloController.MostrarArticulo(this);
        
    }

    protected void btnDescargar_Click(object sender, EventArgs e)
    {
        Byte[] res = null;
        using (MemoryStream ms = new MemoryStream())
        {
            var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(artCorto.InnerHtml, PdfSharp.PageSize.A4);
            pdf.Save(ms);
            res = ms.ToArray();
        }

        Response.Clear();
        Response.ContentType = "Application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=articulo.pdf");
        Response.OutputStream.Write(res, 0, res.Length);
    }
}