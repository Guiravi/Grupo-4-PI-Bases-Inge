using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using LaCafeteria.Utilidades;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{	

	public class LoginModel : PageModel
	{	
		[Required(ErrorMessage = "Debe ingresar su nombre de usuario")]
		[BindProperty]
		public string usernamePK { set; get; }

		[BindProperty(SupportsGet = true)]
		public string cerrarSesion { set; get; }

        private BuscadorMiembrosController buscadorMiembrosController;
		public InformacionMiembroController informacionMiembroController;

		public LoginModel()
		{
            buscadorMiembrosController = new BuscadorMiembrosController();
			informacionMiembroController = new InformacionMiembroController();
		}

		public IActionResult OnGet()
        {
			CreadorNotificacionController creadorNotificacionController = new CreadorNotificacionController();
			
			// TODO: Eliminar codigo de prueba
			creadorNotificacionController.CrearNotificacion( new Notificacion("BadBunny", "Su articulo YHLQMDLG ha sido aceptado", "/Index"));
			creadorNotificacionController.CrearNotificacion(new Notificacion("BadBunny", "Usted ha sido promovido a Miembro de nucleo!", "#"));
			creadorNotificacionController.CrearNotificacion(new Notificacion("BadBunny", "Su articulo Bichiyal ha recibido 1 like", "#"));
			creadorNotificacionController.CrearNotificacion(new Notificacion("BadBunny", "Se solicita su colaboracion para revisar el articulo Oda al mono motorizado", "#"));

			if (cerrarSesion != null)
			{
				Response.Cookies.Delete("usernamePK");
				AvisosInmediatos.Set(this, "cerrarSesion", "Se ha cerrado la sesión", AvisosInmediatos.TipoAviso.Exito);
				return Redirect("/Login");
			}

			return Page();
		}

		public IActionResult OnPost()
		{
			if(EsValido())
			{
				MiembroModel miembro = buscadorMiembrosController.GetMiembro(usernamePK);
				Response.Cookies.Append("usernamePK", miembro.usernamePK);
				Response.Cookies.Append("nombreRolFK", miembro.nombreRolFK);
				List<Notificacion> listaNotificaciones = informacionMiembroController.GetNotificaciones(usernamePK);
				HttpContext.Session.SetComplexData("listaNotificaciones", listaNotificaciones);
				HttpContext.Session.SetInt32("cantidadNotificacionesNuevas", GetCantidadNotificacionesNuevas(listaNotificaciones));
				AvisosInmediatos.Set(this, "sesionIniciada", "Sesión iniciada", AvisosInmediatos.TipoAviso.Exito);
				return Redirect("/Index");
			}

			return Page();
		}

		public bool EsValido()
		{
			bool esValido = true;

			if(buscadorMiembrosController.GetMiembro(usernamePK) == null)
			{
				esValido = false;
				AvisosInmediatos.Set(this, "usernameNoExiste", "Ingrese con un nombre de usuario valido", AvisosInmediatos.TipoAviso.Error);
			}

			return esValido && ModelState.IsValid;
		}

		private int GetCantidadNotificacionesNuevas(List<Notificacion> listaNotificaciones)
		{
			int cantidadNuevas = 0;
			
			if(listaNotificaciones != null)
			{
				foreach (Notificacion notificacion in listaNotificaciones)
				{
					if(notificacion.estado.Equals(Notificacion.Nueva))
					{
						++cantidadNuevas;
					}
				}
			}
			
			return cantidadNuevas;
		}
    }
}