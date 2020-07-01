using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Controllers;
using LaCafeteria.Models;
using Microsoft.AspNetCore.Hosting;

namespace LaCafeteria.Pages
{
    public class DefinirSolicitudRevisionModel : PageModel
    {
		private InformacionArticuloController informacionArticuloController;
		private DocumentosArticuloController documentosArticuloController;
		private AsignadorRevisoresController asignadorRevisoresController;
		private DestructorSolicitudRevisionController destructorSolicitudRevisionController;

		[BindProperty(SupportsGet = true)]
		public int articuloAID { get; set; }

		public ArticuloModel articulo { get; set; }
		public string rutaCarpeta { get; set; }

		public DefinirSolicitudRevisionModel(IHostingEnvironment env)
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

		public ActionResult OnPostAceptaRevision()
		{
			string usernamePK = Request.Cookies["usernamePK"];
			asignadorRevisoresController = new AsignadorRevisoresController();
			asignadorRevisoresController.AsignarRevisor(usernamePK, articuloAID);
			//TODO: Notificar a usuario en pantalla
			return Redirect("/ArticulosParaRevisionNucleo");
		}

		public ActionResult OnPostRechazaRevision()
		{
			string usernamePK = Request.Cookies["usernamePK"];
			destructorSolicitudRevisionController = new DestructorSolicitudRevisionController();
			destructorSolicitudRevisionController.DestruirSolicitudRevision(usernamePK, articuloAID);
			//TODO: Notificar a usuario en pantalla
			return Redirect("/ArticulosParaRevisionNucleo");
		}
	}
}