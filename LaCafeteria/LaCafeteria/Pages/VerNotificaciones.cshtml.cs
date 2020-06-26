using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class VerNotificacionesModel : PageModel
    {
		private List<Notificacion> listaNotificaciones;
		private InformacionMiembroController informacionMiembroController;
		private BuscadorNotificacionController buscadorNotificacionController;
		private EditorNotificacionController editorNotificacionController;
		private int cantidadSinLeer = 0;

		public void OnGet()
        {
			informacionMiembroController = new InformacionMiembroController();
			listaNotificaciones = informacionMiembroController.GetNotificaciones(Request.Cookies["usernamePK"]);
			// Contar notificaciones sin leer
        }

		public ActionResult OnPost(int notificacionID)
		{
			// Obtener la notificacion
			Notificacion notificacion = buscadorNotificacionController.GetNotificacion(notificacionID);

			if(notificacion.estado.Equals(Notificacion.Nueva))
			{
				notificacion.estado = Notificacion.Leida;
				// Actualizar notificacion
				editorNotificacionController.ActualizarNotificacion(notificacion);
			}

			if(notificacion.url != null)
			{
				// Si tiene link redirect
				return Redirect(notificacion.url);
			}

			// TODO: Un redirect para que llame al get(?)
			return Page();		
		}
    }
}