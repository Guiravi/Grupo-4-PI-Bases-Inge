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
		public List<MiembroModel> listaMiembrosParaSolicitudRevision { set; get; }

		private BuscadorMiembrosController buscadorMiembroController;

		public AsignarRevisorModel()
		{
			buscadorMiembroController = new BuscadorMiembrosController();
		}

		public void OnGet()
        {

			//listaMiembrosParaSolicitudRevision = buscadorMiembroController.GetListaMiembrosParaSolicitudRevision();
		}
    }
}