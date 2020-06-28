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

		public ArticuloModel articulo;

		[BindProperty(SupportsGet = true)]
		public int articuloAID { get; set; }

		[BindProperty]
		public List<string> listaSolicitados { set; get; }

		private BuscadorMiembrosController buscadorMiembroController;
		private CreadorSolicitudRevisionController creadorSolicitudRevisionController;
		private InformacionArticuloController informacionArticuloController;

		public AsignarRevisorModel()
		{
			listaMiembrosInteresados = new List<MiembroModel>();
			listaMiembrosParaSolicitudRevision = new List<MiembroModel>();
			listaMiembrosParaAsignarRevision = new List<MiembroModel>();

			buscadorMiembroController = new BuscadorMiembrosController();
			creadorSolicitudRevisionController = new CreadorSolicitudRevisionController();
			informacionArticuloController = new InformacionArticuloController();

		}

		public void OnGet()
        {
			articulo = informacionArticuloController.GetInformacionArticuloModel(articuloAID);

			listaMiembrosInteresados = buscadorMiembroController.GetListaMiembrosInteresados(articuloAID);

			listaMiembrosParaSolicitudRevision = buscadorMiembroController.GetListaMiembrosParaSolicitudRevision(articuloAID);

			listaMiembrosParaAsignarRevision = buscadorMiembroController.GetlistaMiembrosParaAsignarRevision(articuloAID);

			if(!EsValidoGet())
			{	
				//TODO: Setear notificacion de error
				Redirect("/ArticulosPorRevisar");
			}
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
		
		private bool EsValidoGet()
		{
			bool valido = false;
			if(articulo != null)
			{
				valido = true;
			}

			return valido;
		}
    }
}