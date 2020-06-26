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

		public MiembroController miembroController;
		public InformacionMiembroController informacionMiembroController;

		public LoginModel()
		{
			miembroController = new MiembroController();
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
				Notificaciones.Set(this, "cerrarSesion", "Se ha cerrado la sesión", Notificaciones.TipoNotificacion.Exito);
				return Redirect("/Login");
			}

			return Page();
		}

		public IActionResult OnPost()
		{
			if(EsValido())
			{
				Response.Cookies.Append("usernamePK", usernamePK);
				HttpContext.Session.SetComplexData("listaNotificaciones", informacionMiembroController.GetNotificaciones(usernamePK));
				Notificaciones.Set(this, "sesionIniciada", "Sesión iniciada", Notificaciones.TipoNotificacion.Exito);
				return Redirect("/Index");
			}

			return Page();
		}

		public bool EsValido()
		{
			bool esValido = true;

			if(!miembroController.ExisteMiembro(usernamePK))
			{
				esValido = false;
				Notificaciones.Set(this, "usernameNoExiste", "Ingrese con un nombre de usuario valido", Notificaciones.TipoNotificacion.Error);
			}

			return esValido && ModelState.IsValid;
		}
    }
}