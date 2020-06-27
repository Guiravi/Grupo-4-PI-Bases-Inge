using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using System.Web;
using TheCoffeePlace.Models;
using TheCoffeePlace.Utilities;
using System.IO;
/// <summary>
/// Summary description for ArticuloController
/// </summary>
///
namespace TheCoffeePlace.Controllers
{
    public class ArticuloController
    {

		public void GuardarArticulo(IView_EscribirArticulo view)
		{
			ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
			AutorDBHandler autdbHandler = new AutorDBHandler();
			String nombreAutor = view.autor;
			String fechaPublicacion = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
			ArticuloModel articulo = new ArticuloModel(view.titulo, view.resumen, view.tipo, view.contenido, fechaPublicacion, nombreAutor, view.username);

			List<TopicoModel> topicosArticulo = new List<TopicoModel>();
			foreach (ListItem item in view.checkBoxList.Items)
			{
				if (item.Selected)
				{
					topicosArticulo.Add(new TopicoModel(item.Value));
				}
			}

			if(topicosArticulo.Count ==0)
			{
				Utilidades.SetErrorMsg((Page)view, "Debe elegir al menos un topico", "~/EscribirArticulo.aspx");
			}
			artdbHandler.SaveArticulo(articulo, topicosArticulo);
		}

        public void ActualizarArticulo(IView_EscribirArticulo view,int idArticuloPK,int tipo)
        {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            AutorDBHandler autdbHandler = new AutorDBHandler();
            String nombreAutor = view.autor;
            String fechaPublicacion = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();

            ArticuloModel articulo = new ArticuloModel(idArticuloPK,view.titulo, view.resumen, tipo, view.contenido, fechaPublicacion, nombreAutor, view.username);
            List<TopicoModel> topicosArticulo = new List<TopicoModel>();
            foreach (ListItem item in view.checkBoxList.Items)
            {
                if (item.Selected)
                {
                    topicosArticulo.Add(new TopicoModel(item.Value));
                }
            }
            artdbHandler.UpdateArticulo(articulo, topicosArticulo);
        }

        public void ActualizarArticulo(IView_SubirArticulo view, int idArticuloPK)
        {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            AutorDBHandler autdbHandler = new AutorDBHandler();


            String nombreCompletoAutor = autdbHandler.GetFullName(view.username);

            string contenidoString = System.Convert.ToBase64String(view.contenido, 0, view.contenido.Length);

            String fechaPublicacion = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
            ArticuloModel articulo = new ArticuloModel(idArticuloPK,view.titulo, view.resumen, view.tipo, contenidoString, fechaPublicacion, nombreCompletoAutor, view.username);

			List<TopicoModel> topicosArticulo = new List<TopicoModel>();
			foreach (ListItem item in view.checkBoxList.Items)
			{
				if (item.Selected)
				{
					topicosArticulo.Add(new TopicoModel(item.Value));
				}
			}
            artdbHandler.UpdateArticulo(articulo, topicosArticulo);
        }
        public void GuardarArticulo(IView_SubirArticulo view)
        {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            AutorDBHandler autdbHandler = new AutorDBHandler();


            String nombreCompletoAutor = autdbHandler.GetFullName(view.username);

            string contenidoString = System.Convert.ToBase64String(view.contenido, 0, view.contenido.Length);

            String fechaPublicacion = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
            ArticuloModel articulo = new ArticuloModel(view.titulo, view.resumen, view.tipo, contenidoString, fechaPublicacion, nombreCompletoAutor, view.username);

            List<TopicoModel> topicosArticulo = new List<TopicoModel>();
            foreach (ListItem item in view.checkBoxList.Items)
            {
                if (item.Selected)
                {
                    topicosArticulo.Add(new TopicoModel(item.Value));
                }
            }
            artdbHandler.SaveArticulo(articulo, topicosArticulo);
        }
        public void BuscarArticuloPorTopico(IView_BuscarArticulos view)
		{
            int tipos = 0;

            if (view.chkbBCortoChecked)
                tipos = 1;

            if (view.chkbBLargoChecked)
                tipos = 2;

            if (view.chkbBCortoChecked && view.chkbBLargoChecked)
                tipos = 0;

			ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
			List<ArticuloModel> articulos = artdbHandler.GetArticulosPorTopico(view.contenidoBusqueda, tipos);
			view.gridView.DataSource = articulos;
			view.gridView.DataBind();
		}

        public void BuscarArticuloPorAutor(IView_BuscarArticulos view)
        {
            int tipos = 0;

            if (view.chkbBCortoChecked)
                tipos = 1;

            if (view.chkbBLargoChecked)
                tipos = 2;

            if (view.chkbBCortoChecked && view.chkbBLargoChecked)
                tipos = 0;

            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            List<ArticuloModel> articulos = artdbHandler.GetArticulosPorAutor(view.contenidoBusqueda, tipos);
            view.gridView.DataSource = articulos;
            view.gridView.DataBind();
        }
        public void BuscarArticuloPorTitulo(IView_BuscarArticulos view)
        {
            int tipos = 0;

            if (view.chkbBCortoChecked)
                tipos = 1;

            if (view.chkbBLargoChecked)
                tipos = 2;

            if (view.chkbBCortoChecked && view.chkbBLargoChecked)
                tipos = 0;

            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            List<ArticuloModel> articulos = artdbHandler.GetArticulosPorTitulo(view.contenidoBusqueda, tipos);
            view.gridView.DataSource = articulos;
            view.gridView.DataBind();
        }



        public void BuscarTodosArticulos (IView_BuscarArticulos view)
        {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            List<ArticuloModel> articulos = artdbHandler.GetTodosArticulos();
            view.gridView.DataSource = articulos;
            view.gridView.DataBind();
        }
        public void BuscarArticuloPorAutorEditar(IViewArticlesByAutorEdit view) {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            List<ArticuloModel> articulos = artdbHandler.GetArticlesByAutorEdit(view.autor);
            view.gridView.DataSource = articulos;
            view.gridView.DataBind();

        }
        public string GetContenidoArticuloCorto(int idArticuloPK) {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            string contenido = artdbHandler.GetContenidoCortoDB(idArticuloPK);
            return contenido;
        }
        public string GetTitulo(int idArticuloPK) {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            string titulo = artdbHandler.GetTituloDB(idArticuloPK);
            return titulo;
        }
        public string GetResumen(int idArticuloPK) {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            string titulo = artdbHandler.GetResumenDB(idArticuloPK);
            return titulo;
        }
        public string GetAutores(int idArticuloPK)
        {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            string autores = artdbHandler.GetAutoresDB(idArticuloPK);
            return autores;
        }
        public void ObtenerPaginaResumen(IView_VerResumen view) {
            ArticuloDBHandler artDBHandler = new ArticuloDBHandler();
            ArticuloModel articulo = artDBHandler.GetInfoPaginaResumen(view.idArticuloPK);

            view.tipo = articulo.tipo;
            view.titulo = articulo.titulo;
            view.autor = articulo.nombreAutor;
            view.resumen = articulo.resumen;
        }

        public void MostrarArticulo(IView_VerResumen view)
        {
            /* Intentar abrir el pdf */
            String nombre_Art =  Convert.ToString(view.idArticuloPK) ;
            if (File.Exists(HttpContext.Current.Server.MapPath("~/ArticulosPDF/" + nombre_Art + ".pdf"))){
                Utilidades.VerPDF((Page)view, "~/ArticulosPDF/" + nombre_Art + ".pdf");
            }
            else
            {
                ArticuloDBHandler articuloDBHandler = new ArticuloDBHandler();
                if (view.tipo == 0)
                {
                    string contenido = articuloDBHandler.DescargarArticuloHtml(view.idArticuloPK);
                    view.setArticuloCorto(contenido);
                }
                else
                {
                    byte[] contenido = articuloDBHandler.DescargarArticuloDocx(view.idArticuloPK);
                    File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/ArticulosDocx/" + nombre_Art + ".docx"), contenido);
                    Utilidades.ConvertToPDF("~/ArticulosDocx/" + nombre_Art + ".docx");
                    Utilidades.VerPDF((Page)view, "~/ArticulosPDF/" + nombre_Art + ".pdf");
                }
            }
        }

        /*
        public void DescargarArticulo(IView_SubirArticulo view)
        {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();

            byte[] articulo = artdbHandler.DescargarArticulo(idArticuloPK);

            
            //view.Clear();
            ((Page)view).Response.ContentType = "Application/docx";
            ((Page)view).Response.AppendHeader("Content-Disposition", "attachment; filename=articulo.docx");

            ((Page)view).Response.OutputStream.Write(articulo, 0, articulo.Length);
            
            ((Page)view).Response.End();
            //((Page)view).Response.Redirect("~/EditarArticuloLargo.aspx?idArticuloPK=" + ((Page)view).Request.QueryString["idArticuloPK"] + "&tipo=" + ((Page)view).Request.QueryString["tipo"] + "&usernameFK=" + ((Page)view).Request.QueryString["usernameFK"] + "&descarga=" + 0,true);
        }
		*/
        

    }
}