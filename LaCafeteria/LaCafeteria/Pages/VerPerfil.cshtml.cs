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

        private BuscadorMiembrosController buscadorMiembrosController;
        private BuscadorArticuloController buscadorArticuloController;
        public InformacionArticuloController informacionArticuloController;

		public MiembroModel miembro;

		public VerPerfilModel()
		{

            buscadorMiembrosController = new BuscadorMiembrosController();
            buscadorArticuloController = new BuscadorArticuloController();
            informacionArticuloController = new InformacionArticuloController();

        }

        public void OnGet()
        {
			if(ModelState.IsValid)
			{	
				//Cargar perfil del miembro
				miembro = buscadorMiembrosController.GetMiembro(usernamePK);
				if(miembro.fechaNacimiento != null)
				{
					miembro.fechaNacimiento = Convertidor.CambiarFormatoFechaDMA(miembro.fechaNacimiento);
				}

				// Cargar articulos
				articulosResultado = buscadorArticuloController.GetArticulosPorMiembro(usernamePK);
				cantResultados = articulosResultado.Count;
			}
        }
    }
}