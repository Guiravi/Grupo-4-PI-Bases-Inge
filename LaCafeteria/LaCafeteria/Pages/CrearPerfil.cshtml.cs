using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using LaCafeteria.Utilidades;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class CrearPerfilModel : PageModel
    {
		[BindProperty]
		public MiembroModel miembro { set; get; }

		[BindProperty]
		[Required(ErrorMessage = "Debe subir una imagen de perfil")]
		public IFormFile imagenDePerfil { set; get; }

		public MiembroController miembroController{ set; get; }

		public CrearPerfilModel()
		{
			miembroController = new MiembroController();
		}

		public IActionResult OnPost()
        {
			if(EsValido())
			{
				miembro.rutaImagenPerfil = "Imagen/sadsadd.jpg";
				miembroController.CrearMiembro(miembro);
				Response.Cookies.Append("usernamePK", miembro.usernamePK);
				Notificaciones.Set(this, "sesionIniciada", "Sesión iniciada", Notificaciones.TipoNotificacion.Exito);
				return Redirect("/Index");
			}

			return Page();
        }

		public bool EsValido()
		{
			bool esValido = false;

			if(imagenDePerfil != null)
			{
				esValido = true;
			}

			return esValido && ModelState.IsValid;
		}
    }
}