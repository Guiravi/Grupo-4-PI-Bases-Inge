using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Pages
{
	public class VerMiembrosModel : PageModel
	{
		public List<MiembroModel> miembros { get; set; }
        //public MiembroController miembroController;
        private BuscadorMiembrosController buscadorMiembrosController;

		public VerMiembrosModel()
		{
            //miembroController = new MiembroController();
            buscadorMiembrosController = new BuscadorMiembrosController();
        }

        public void OnGet()
        {
			miembros = buscadorMiembrosController.GetListaMiembrosModel();
        }
    }
}