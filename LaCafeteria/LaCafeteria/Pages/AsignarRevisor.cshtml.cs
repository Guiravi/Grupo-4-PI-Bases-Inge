using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models; 

namespace LaCafeteria.Pages
{
    public class AsignarRevisorModel : PageModel
    {
		public List<MiembroModel> listaMiembrosParaSolicitudRevision { set; get; }

		public AsignarRevisorModel()
		{

		}

		public void OnGet()
        {
			
        }
    }
}