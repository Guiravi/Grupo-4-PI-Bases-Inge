using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Pages
{
	public class LoginModel : PageModel
	{	
		[Required(ErrorMessage = "Debe ingresar su nombre de usuario")]
		[BindProperty]
		public string usernamePK { set; get; }

		[BindProperty(SupportsGet = true)]
		public string cerrarSesion { set; get; }

		public IActionResult OnGet()
        {
			if(cerrarSesion != null)
			{
				Response.Cookies.Delete("usernamePK");
				Notificaciones.Set(this, "cerrarSesion", "Se ha cerrado la sesión", Notificaciones.TipoNotificacion.Exito);
				return Redirect("/Login");
			}

			return Page();
		}

		public IActionResult OnPost()
		{
			// TODO: Logica de validacion para el username y password
			if(ModelState.IsValid)
			{
				Response.Cookies.Append("usernamePK", usernamePK);
				return Redirect("/Index");
			}

			return Page();
		}
    }
}