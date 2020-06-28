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

		public AsignarRevisorModel()
		{
			listaMiembrosParaSolicitudRevision = new List<MiembroModel>();
			buscadorMiembroController = new BuscadorMiembrosController();

			listaMiembrosInteresados = buscadorMiembroController.GetListaMiembrosInteresados(articuloAID);

			listaMiembrosParaSolicitudRevision = buscadorMiembroController.GetListaMiembrosParaSolicitudRevision(articuloAID);

			listaMiembrosParaAsignarRevision = buscadorMiembroController.GetlistaMiembrosParaAsignarRevision(articuloAID);
		}

		public void OnGet()
        {	
			
		}

		public void OnPostCancelarColaboracion()
		{

		}

		public void OnPostSolicitarColaboracion()
		{
			// Crear solicitud
		}
		
    }
}