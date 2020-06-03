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
using Microsoft.AspNetCore.Hosting;

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

		public string rutaCarpeta;

		public CrearPerfilModel(IHostingEnvironment env)
		{
			miembroController = new MiembroController();
			rutaCarpeta = env.WebRootPath;
		}

		public IActionResult OnPost()
        {
			if(EsValido())
			{
				var filePath = rutaCarpeta + "/ImagenesPerfil/" + miembro.usernamePK + "." + imagenDePerfil.ContentType.Split('/')[1];

				using (var stream = System.IO.File.Create(filePath))
				{
					imagenDePerfil.CopyTo(stream);
				}

				miembro.rutaImagenPerfil = "/ImagenesPerfil/" + miembro.usernamePK + ".jpg";
				miembroController.CrearMiembro(miembro);
				Response.Cookies.Append("usernamePK", miembro.usernamePK);
				Notificaciones.Set(this, "sesionIniciada", "Sesión iniciada", Notificaciones.TipoNotificacion.Exito);
				return Redirect("/Index");
			}

			return Page();
        }

		public bool EsValido()
		{
			bool esValido = true;

			if(imagenDePerfil != null)
			{	
				if(!imagenDePerfil.ContentType.Equals("image/png") && !imagenDePerfil.ContentType.Equals("image/jpg") && !imagenDePerfil.ContentType.Equals("image/jpeg"))
				{
					esValido = false;
					Notificaciones.Set(this, "formatoInvalido", "Debe subir una imagen en formato .png o .jpg", Notificaciones.TipoNotificacion.Error);
				}
			}
			if (miembroController.ExisteMiembro(miembro.usernamePK))
			{
				esValido = false;
				Notificaciones.Set(this, "usernamePKInvalido", "Nombre de usuario ya existe. Seleccione otro nombre de usuario", Notificaciones.TipoNotificacion.Error);
			}

			return esValido && ModelState.IsValid;
		}
    }
}