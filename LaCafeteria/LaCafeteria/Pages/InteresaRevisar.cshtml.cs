using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Controllers;
using LaCafeteria.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaCafeteria.Pages
{
	public class InteresaRevisarModel : PageModel
	{
		private InformacionArticuloController informacionArticuloController;
		private DocumentosArticuloController documentosArticuloController;
		private CreadorSolicitudRevisionController creadorSolicitudRevisionController;

		[BindProperty(SupportsGet = true)]
		public int articuloAID { get; set; }

		public ArticuloModel articulo { get; set; }
		public string rutaCarpeta { get; set; }

		public InteresaRevisarModel(IHostingEnvironment env)
		{
			informacionArticuloController = new InformacionArticuloController();
			rutaCarpeta = env.WebRootPath;
		}

		public void OnGet()
		{
			articulo = informacionArticuloController.GetInformacionArticuloModel(articuloAID);

			if (articulo.tipo == "Largo")
			{
				documentosArticuloController = new DocumentosArticuloController();
				documentosArticuloController.CargarArticuloPDF(articuloAID, rutaCarpeta);
			}
		}

		public ActionResult OnPostMeInteresaRevisar()
		{
			string usernamePK = Request.Cookies["usernamePK"];
			creadorSolicitudRevisionController = new CreadorSolicitudRevisionController();
			creadorSolicitudRevisionController.CrearSolicitudRevision(usernamePK, articuloAID, CreadorSolicitudRevisionController.Interesa);
			//TODO: Notificar a usuario en pantalla
			return Redirect("/ArticulosPorRevisar");
		}
	}
}