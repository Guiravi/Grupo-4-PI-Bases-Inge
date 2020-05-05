using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
		private IView_EscribirArticulo view;

		public ArticuloController(IView_EscribirArticulo view)
		{
			this.view = view;
		}

		public void GuardarArticulo()
		{
			ArticuloDBHandler artdbHandler = new ArticuloDBHandler();
			AutorDBHandler autdbHandler = new AutorDBHandler();
			String nombreCompletoAutor = autdbHandler.GetFullName(view.username);
			String fechaPublicacion = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
			ArticuloModel articulo = new ArticuloModel(view.titulo, view.resumen, view.tipo, view.contenido, fechaPublicacion, nombreCompletoAutor, view.username);
			artdbHandler.SaveArticulo(articulo);
		}
	}
}