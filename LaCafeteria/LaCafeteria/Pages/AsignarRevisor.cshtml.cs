using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class AsignarRevisorModel : PageModel
    {
		public List<MiembroModel> listaMiembrosInteresados { set; get; }
		public List<MiembroModel> listaMiembrosParaAsignarRevision { set; get; }
		public List<MiembroModel> listaMiembrosParaSolicitudRevision { set; get; }

		[BindProperty(SupportsGet = true)]
		public int articuloAID { get; set; }

		[BindProperty]
		public List<string> listaSolicitados { set; get; }

		private BuscadorMiembrosController buscadorMiembroController;
		private CreadorSolicitudRevisionController creadorSolicitudRevisionController;

		public AsignarRevisorModel()
		{
			listaMiembrosInteresados = new List<MiembroModel>();
			listaMiembrosParaSolicitudRevision = new List<MiembroModel>();
			listaMiembrosParaAsignarRevision = new List<MiembroModel>();

			buscadorMiembroController = new BuscadorMiembrosController();
			creadorSolicitudRevisionController = new CreadorSolicitudRevisionController();

		}

		public void OnGet()
        {
			listaMiembrosInteresados = buscadorMiembroController.GetListaMiembrosInteresados(articuloAID);

			listaMiembrosParaSolicitudRevision = buscadorMiembroController.GetListaMiembrosParaSolicitudRevision(articuloAID);

			listaMiembrosParaAsignarRevision = buscadorMiembroController.GetlistaMiembrosParaAsignarRevision(articuloAID);
		}

		public IActionResult OnPostCancelarColaboracion()
		{
			return Redirect("/AsignarRevisor/" + articuloAID);
		}

		public IActionResult OnPostSolicitarColaboracion()
		{
			foreach(string usernameMiemFK in listaSolicitados)
			{
				creadorSolicitudRevisionController.CrearSolicitudRevision(usernameMiemFK, articuloAID, CreadorSolicitudRevisionController.Solicitado);
			}
	
			return Redirect("/AsignarRevisor/" + articuloAID);
		}
		
    }
}