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

		[BindProperty]
		public List<string> listaIdiomas { set; get; }
       
        private CreadorMiembrosController creadorMiembrosController;
        private BuscadorMiembrosController buscadorMiembrosController;

		public string rutaCarpeta;

		public CrearPerfilModel(IHostingEnvironment env)
		{
            creadorMiembrosController = new CreadorMiembrosController();
            buscadorMiembrosController = new BuscadorMiembrosController();

			listaIdiomas = new List<string>();
			rutaCarpeta = env.WebRootPath;
		}

		public IActionResult OnPost()
        {
			if(EsValido())
			{
				var filePath = rutaCarpeta + "/images/ImagenesPerfil/" + miembro.usernamePK + "." + imagenDePerfil.ContentType.Split('/')[1];

				using (var stream = System.IO.File.Create(filePath))
				{
					imagenDePerfil.CopyTo(stream);
				}

				miembro.rutaImagenPerfil = "images/ImagenesPerfil/" + miembro.usernamePK + "." + imagenDePerfil.ContentType.Split('/')[1];

				miembro.idiomas = listaIdiomas;
                creadorMiembrosController.CrearMiembro(miembro);
				Response.Cookies.Append("usernamePK", miembro.usernamePK);
				AvisosInmediatos.Set(this, "sesionIniciada", "Sesión iniciada", AvisosInmediatos.TipoAviso.Exito);
				return Redirect("/Index");
			}

			return Page();
        }

		public bool EsValido()
		{
			bool esValido = true;

			if (!ModelState.IsValid)
			{
				esValido = false;
			}
			else
			{
				if (imagenDePerfil != null && esValido)
				{
					if (!imagenDePerfil.ContentType.Equals("image/png") && !imagenDePerfil.ContentType.Equals("image/jpg") && !imagenDePerfil.ContentType.Equals("image/jpeg"))
					{
						esValido = false;
						AvisosInmediatos.Set(this, "formatoInvalido", "Debe subir una imagen en formato .png o .jpg", AvisosInmediatos.TipoAviso.Error);
					}
				}
				if ( buscadorMiembrosController.GetMiembro(miembro.usernamePK) != null && esValido)
				{
					esValido = false;
					AvisosInmediatos.Set(this, "usernamePKInvalido", "Nombre de usuario ya existe. Seleccione otro nombre de usuario", AvisosInmediatos.TipoAviso.Error);
				}
			}

			return esValido;
		}
    }
}