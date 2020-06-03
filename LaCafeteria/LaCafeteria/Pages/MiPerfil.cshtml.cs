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

		public ArticuloController articuloController;

		public MiembroController miembroController;

		public TopicoController topicoController;

		public MiPerfilModel()
		{
			topicoController = new TopicoController();
			articuloController = new ArticuloController();
			miembroController = new MiembroController();
		}

		public void OnGet()
		{
			// Cargar perfil
			miembro = miembroController.GetMiembro(Request.Cookies["usernamePK"]);
			miembro.fechaNacimiento = Convertidor.CambiarFormatoFechaDMA(miembro.fechaNacimiento);

			// Cargar articulos
			string usernamePK = Request.Cookies["usernamePK"];
			misArticulos = articuloController.GetMisArticulos(usernamePK);
			cantResultados = misArticulos.Count;
		}

        public IActionResult OnPostModificar()
        {
            return Redirect("/ModificarPerfil");
        }
	}
}