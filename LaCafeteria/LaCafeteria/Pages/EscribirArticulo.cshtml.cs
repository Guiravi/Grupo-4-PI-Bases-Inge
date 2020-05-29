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

namespace LaCafeteria.Pages
{
	public class EscribirArticuloModel : PageModel
	{
		[BindProperty]
		public ArticuloModel articulo { set; get; }

		public List<TopicoModel> listaTopicos { set; get; }

		public List<MiembroModel> listaMiembros { set; get; }

		[BindProperty]
		public List<string> listaTopicosArticulo { set; get; }

		[BindProperty]
		public List<string> listaMiembrosAutores { set; get; }

		public TopicoController topicoController;
		public MiembroController miembroController;

		public EscribirArticuloModel()
		{
			topicoController = new TopicoController();
			miembroController = new MiembroController();
			listaTopicos = topicoController.GetListaTopicos();
			listaMiembros = miembroController.GetListaMiembros();
		}

		public void OnGet()
        {
		
        }

		public void OnPost()
		{
			articulo.tipo = "corto";
			// TODO: articuloController.SubirArticulo(articulo, listaUsernameAutores)
			
		}
	}
}