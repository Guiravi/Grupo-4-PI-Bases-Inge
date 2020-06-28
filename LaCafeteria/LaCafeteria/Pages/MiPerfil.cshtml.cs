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
    public class MiPerfilModel : PageModel
    {
		public List<ArticuloModel> misArticulos { set; get; }

		public MiembroModel miembro { set; get; }

		public int cantResultados { set; get; }

        private BuscadorMiembrosController buscadorMiembrosController;
        private BuscadorArticuloController buscadorArticuloController;
        public InformacionArticuloController informacionArticuloController;

		public MiPerfilModel()
		{
            buscadorMiembrosController = new BuscadorMiembrosController();
            buscadorArticuloController = new BuscadorArticuloController();
            informacionArticuloController = new InformacionArticuloController();

        }

		public void OnGet()
		{
			// Cargar perfil
			miembro = buscadorMiembrosController.GetMiembro(Request.Cookies["usernamePK"]);
			miembro.fechaNacimiento = Convertidor.CambiarFormatoFechaDMA(miembro.fechaNacimiento);

			// Cargar articulos
			string usernamePK = Request.Cookies["usernamePK"];
			misArticulos = buscadorArticuloController.GetArticulosPorMiembro(usernamePK);
			cantResultados = misArticulos.Count;
		}

        public IActionResult OnPostModificar()
        {
            return Redirect("/ModificarPerfil");
        }
	}
}