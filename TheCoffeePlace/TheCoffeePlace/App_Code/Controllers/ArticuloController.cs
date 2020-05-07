using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using TheCoffeePlace.Views;
using TheCoffeePlace.Models;
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
			String nombreCompletoAutor = autdbHandler.GetFullName(view.username);
			String fechaPublicacion = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
			ArticuloModel articulo = new ArticuloModel(view.titulo, view.resumen, view.tipo, view.contenido, fechaPublicacion, nombreCompletoAutor, view.username);
			artdbHandler.SaveArticulo(articulo);
		}


        public void GuardarArticulo(IView_SubirArticulo view)
        {
            ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
            AutorDBHandler autdbHandler = new AutorDBHandler();
            

            String nombreCompletoAutor = autdbHandler.GetFullName(view.username);

            string contenidoString = System.Convert.ToBase64String(view.contenido, 0, view.contenido.Length);

            String fechaPublicacion = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
            ArticuloModel articulo = new ArticuloModel(view.titulo, view.resumen, view.tipo, contenidoString, fechaPublicacion, nombreCompletoAutor, view.username);
            artdbHandler.SaveArticulo(articulo);
        }
		
		public void BuscarArticuloPorTopico(IView_BuscarArticulos view)
		{
			ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
			List<ArticuloModel> articulos = artdbHandler.GetArticulosPorTopico(view.topico);
			view.gridView.DataSource = articulos;
			view.gridView.DataBind();
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