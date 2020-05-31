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
using LaCafeteria.Utilidades;
using System.ComponentModel.DataAnnotations;

namespace LaCafeteria.Pages
{
	public class EscribirArticuloModel : PageModel
	{
		public List<TopicoModel> listaTopicos { set; get; }

		public List<MiembroModel> listaMiembros { set; get; }

		[BindProperty]
		public ArticuloModel articulo { set; get; }

		[BindProperty]
		public List<string> listaTopicosArticulo { get; set; }

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
			if(EsValido())
			{	
				articulo.tipo = "corto";
				// TODO: articuloController.SubirArticulo(articulo, listaUsernameAutores)
				Notificaciones.Set(this, "articuloGuardado", "Su articulo se guardó", Notificaciones.TipoNotificacion.Exito);
			}

		}

		private bool EsValido()
		{
			bool esValido = true;

			if(listaTopicosArticulo.Count == 0)
			{
				Notificaciones.Set(this, "listaTopicosArticulo", "Debe seleccionar al menos un tópico para su artículo", Notificaciones.TipoNotificacion.Error);
				esValido = false;
			}

			if (listaMiembrosAutores.Count == 0)
			{
				Notificaciones.Set(this, "listaMiembrosAutores", "Debe seleccionar al menos un autor para su artículo", Notificaciones.TipoNotificacion.Error);
				esValido = false;
			}

			return esValido && ModelState.IsValid;
		}
	}
}