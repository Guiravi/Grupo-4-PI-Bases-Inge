using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;
using Microsoft.AspNetCore.Http;

namespace LaCafeteria.Pages
{
    public class VerNotificacionesModel : PageModel
    {
		private List<Notificacion> listaNotificaciones;
		private InformacionMiembroController informacionMiembroController;
		private BuscadorNotificacionController buscadorNotificacionController;
		private EditorNotificacionController editorNotificacionController;
		private int cantidadSinLeer = 0;


		public VerNotificacionesModel()
		{
			informacionMiembroController = new InformacionMiembroController();
			buscadorNotificacionController = new BuscadorNotificacionController();
			editorNotificacionController = new EditorNotificacionController();
		}

		public void OnGet()
        {	
			listaNotificaciones = informacionMiembroController.GetNotificaciones(Request.Cookies["usernamePK"]);
			HttpContext.Session.SetComplexData("listaNotificaciones", listaNotificaciones);
			// Contar notificaciones sin leer
			// TODO: Establecer en variable de sesion la cantidad de notificaciones sin leer.
        }

		public ActionResult OnPost(int notificacionAID)
		{
			// Obtener la notificacion
			Notificacion notificacion = buscadorNotificacionController.GetNotificacion(notificacionAID);

			if(notificacion.estado.Equals(Notificacion.Nueva))
			{
				notificacion.estado = Notificacion.Leida;
				// Actualizar notificacion
				editorNotificacionController.ActualizarNotificacion(notificacion);
				// TODO: Establecer en variable de sesion la cantidad de notificaciones sin leer.
				listaNotificaciones = informacionMiembroController.GetNotificaciones(Request.Cookies["usernamePK"]);
				HttpContext.Session.SetComplexData("listaNotificaciones", listaNotificaciones);
				HttpContext.Session.SetInt32("cantidadNotificacionesNuevas", GetCantidadNotificacionesNuevas(listaNotificaciones));
			}

			if (notificacion.url != null)
			{
				// Si tiene link redirect
				return Redirect(notificacion.url);
			}

			return Redirect("/VerNotificaciones");		
		}

		private int GetCantidadNotificacionesNuevas(List<Notificacion> listaNotificaciones)
		{
			int cantidadNuevas = 0;

			if (listaNotificaciones != null)
			{
				foreach (Notificacion notificacion in listaNotificaciones)
				{
					if (notificacion.estado.Equals(Notificacion.Nueva))
					{
						++cantidadNuevas;
					}
				}
			}

			return cantidadNuevas;
		}
	}
}