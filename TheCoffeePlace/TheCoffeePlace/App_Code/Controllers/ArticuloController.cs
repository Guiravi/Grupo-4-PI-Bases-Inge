using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using TheCoffeePlace.Views;
using TheCoffeePlace.Models;
using System.Web.UI.WebControls;
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
        public string getContenidoArticuloCorto(int idArticuloPK) {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            string contenido = artdbHandler.GetContenidoCortoDB(idArticuloPK);
            return contenido;
        }
        public void ObtenerPaginaResumen(IView_VerResumen view) {
            ArticuloDBHandler artDBHandler = new ArticuloDBHandler();
            ArticuloModel articulo = artDBHandler.GetInfoPaginaResumen(view.idArticuloPK);

            view.titulo = articulo.titulo;
            view.autor = articulo.nombreAutor;
            view.resumen = articulo.resumen;
        }

        /*
        public void DescargarArticulo(IView_SubirArticulo view)
        {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();

            byte[] articulo = artdbHandler.DescargarArticulo(view.idArticulo);

            
            view.resp.Clear();
            view.resp.ContentType = "Application/docx";
            view.resp.AppendHeader("Content-Disposition", "attachment; filename=articulo.docx");

            view.resp.OutputStream.Write(articulo, 0, articulo.Length);

            view.resp.End();
        }
        */

    }
}