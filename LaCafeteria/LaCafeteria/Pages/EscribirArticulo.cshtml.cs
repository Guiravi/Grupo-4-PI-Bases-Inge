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

		[BindProperty]
		public List<string> listaTopicosArticulo { set; get; }

		public TopicoController topicoController;
		//public ArticuloController articuloController;

		public EscribirArticuloModel()
		{
			topicoController = new TopicoController();
			//aController = new TopicoController();
			listaTopicos = topicoController.GetListaTopicos();
		}

		public void OnGet()
        {
		
        }

		public void OnPost()
		{	
			// TODO: Definir el usernameFK "Manuelito01" en alguna variable global para acceder desde cualquier razor page.
			articulo.usernameFK = "Manuelito01";
			// TODO: Manejar el tipo como string? Referirse a comentario de la profe de la iteracion 1
			articulo.tipo = 0;
			
		}
	}
}