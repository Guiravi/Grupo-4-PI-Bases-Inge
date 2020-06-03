using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Controllers;
using LaCafeteria.Models;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Pages
{
    public class VerPerfilModel : PageModel
    {
		public List<ArticuloModel> articulosResultado { set; get; }

		[BindProperty(SupportsGet = true)]
		public string usernamePK { get; set; }

		public int cantResultados { set; get; }
		public MiembroController miembroController;

		public ArticuloController articuloController;

		public TopicoController topicoController;

		public MiembroModel miembro;

		public VerPerfilModel()
		{
			miembroController = new MiembroController();
			articuloController = new ArticuloController();
			topicoController = new TopicoController();
		}

        public void OnGet()
        {
			if(ModelState.IsValid)
			{	
				//Cargar perfil del miembro
				miembro = miembroController.GetMiembro(usernamePK);
				miembro.fechaNacimiento = Convertidor.CambiarFormatoFechaDMA(miembro.fechaNacimiento);

				// Cargar articulos
				articulosResultado = articuloController.GetMisArticulos(usernamePK);
				cantResultados = articulosResultado.Count;
			}
        }
    }
}