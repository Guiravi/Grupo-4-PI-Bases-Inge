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

		// TODO: Convertir a lista de MiembroModel
		public List<AutorModel> listaAutores { set; get; }

		[BindProperty]
		public List<string> listaTopicosArticulo { set; get; }

		[BindProperty]
		public List<string> listaUsernameAutores { set; get; }

		public TopicoController topicoController;
		//public ArticuloController articuloController;

		public EscribirArticuloModel()
		{
			topicoController = new TopicoController();
			//aController = new TopicoController();
			listaTopicos = topicoController.GetListaTopicos();

			// TODO: Utilizar MiembroController para conseguir la lista de todos los miembros.
			listaAutores = new List<AutorModel>()
			{
				new AutorModel() { usernamePK = "manuelito01", nombre = "Manuel" , apellido1 = "Ramirez", apellido2 = "Lopez"},
				new AutorModel() { usernamePK = "-badbunny-", nombre = "Benito" , apellido1 = "Martinez", apellido2 = "Ocasio"},
				new AutorModel() { usernamePK = "vivaBoshtro3000", nombre = "Hernan" , apellido1 = "Medford", apellido2 = "Soliz"},
				new AutorModel() { usernamePK = "carlit0x", nombre = "Carlos" , apellido1 = "Alvarado", apellido2 = "Lopez"}
			};
		}

		public void OnGet()
        {
		
        }

		public void OnPost()
		{	
			// TODO: Definir el usernameFK "Manuelito01" en alguna variable global para acceder desde cualquier razor page.
			articulo.usernameFK = "manuelito01";
			// TODO: Manejar el tipo como string? Referirse a comentario de la profe de la iteracion 1
			articulo.tipo = 0;
			
		}
	}
}