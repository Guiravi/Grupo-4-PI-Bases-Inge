using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Utilidades;
using System.Web;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using LaCafeteria.Controllers;
using System.ComponentModel.DataAnnotations;

namespace LaCafeteria.Pages
{
	public class EscribirArticuloModel : PageModel
	{
		public List<TopicoModel> listaTopicos { set; get; }

		public List<MiembroModel> listaMiembros { set; get; }

		[BindProperty]
		[Required]
		public ArticuloModel articulo { set; get; }

		[BindProperty]
		[MinLength(1, ErrorMessage = "Hola")]
		[Required(ErrorMessage = "Hola")]
		public List<string> listaTopicosArticulo { set; get; }

		[BindProperty]
		[Required, MinLength(1, ErrorMessage = "At least one item required in work order")]
		public List<string> listaMiembrosAutores { set; get; }

		public TopicoController topicoController;
		public MiembroController miembroController;

		public EscribirArticuloModel()
		{
			topicoController = new TopicoController();
			miembroController = new MiembroController();
			listaTopicos = topicoController.GetListaTopicos();
			listaMiembros = miembroController.GetListaMiembros();
			ModelState.AddModelError("listaMiembrosAutores", "Error 1");
		}

		public void OnGet()
        {
		
        }

		public void OnPost()
		{
			if(ModelState.IsValid)
			{
				articulo.tipo = "corto";
				// TODO: articuloController.SubirArticulo(articulo, listaUsernameAutores)
			}

		}

	}
}