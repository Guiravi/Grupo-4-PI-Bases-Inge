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
        public IFormFile imagenDePerfil { set; get; }

        [BindProperty(SupportsGet = true)]
        public int? editar { set; get; }

        [BindProperty(SupportsGet = true)]
        public string username { set; get; }

        public List<string> listaHabilidadesEstandar { set; get; }
        public List<string> listaPasatiempoEstandar { set; get; }
        public List<string> listaIdiomasEstandar { set; get; }
        public List<string> listaPaises { set; get; }

        [BindProperty]
        public List<string> listaHabilidadesSelec { set; get; }

        [BindProperty]
        public List<string> listaPasatiemposSelec { set; get; }

        [BindProperty]
        public List<string> listaIdiomasSelec { set; get; }

        private CreadorMiembrosController creadorMiembrosController;
        private BuscadorMiembrosController buscadorMiembrosController;
        private InformacionCatalogosController informacionCatalogosController;
        private EditorMiembroController editorMiembroController;

		public string rutaCarpeta;

		public CrearPerfilModel(IHostingEnvironment env)
		{
            creadorMiembrosController = new CreadorMiembrosController();
            buscadorMiembrosController = new BuscadorMiembrosController();
            informacionCatalogosController = new InformacionCatalogosController();
            editorMiembroController = new EditorMiembroController();

            listaHabilidadesEstandar = informacionCatalogosController.GetHabilidadesCatalogo();
            listaPasatiempoEstandar = informacionCatalogosController.GetPasatiemposCatalogo();
            listaIdiomasEstandar = informacionCatalogosController.GetIdiomasCatalogo();
            listaPaises = informacionCatalogosController.GetPaisesCatalogo();

            listaHabilidadesSelec = new List<string>();
            listaPasatiemposSelec = new List<string>();
            listaIdiomasSelec = new List<string>();
			rutaCarpeta = env.WebRootPath;           
		}

        public void OnGet()
        {
            if (editar != null)
            {
                miembro = buscadorMiembrosController.GetMiembro(username);
                miembro.fechaNacimiento = Utilidades.Convertidor.CambiarFormatoFechaAMD(miembro.fechaNacimiento);
                listaHabilidadesSelec = miembro.habilidades;
                listaPasatiemposSelec = miembro.pasatiempos;
                listaIdiomasSelec = miembro.idiomas;
                TempData["username"] = miembro.usernamePK;
            }
        }
 
		public IActionResult OnPostCrear()
        {
			if(EsValido())
			{
                if (imagenDePerfil != null)
                {
                    var filePath = rutaCarpeta + "/images/ImagenesPerfil/" + miembro.usernamePK + "." + imagenDePerfil.ContentType.Split('/')[1];

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        imagenDePerfil.CopyTo(stream);
                    }

                    miembro.rutaImagenPerfil = "images/ImagenesPerfil/" + miembro.usernamePK + "." + imagenDePerfil.ContentType.Split('/')[1];
                }
                else
                {
                    miembro.rutaImagenPerfil = "images/ImagenesPerfil/default_avatar.png";
                }
				

				miembro.idiomas = listaIdiomasSelec;
                miembro.habilidades = listaHabilidadesSelec;
                miembro.pasatiempos = listaPasatiemposSelec;
                creadorMiembrosController.CrearMiembro(miembro);
				Response.Cookies.Append("usernamePK", miembro.usernamePK);
				AvisosInmediatos.Set(this, "sesionIniciada", "Sesión iniciada", AvisosInmediatos.TipoAviso.Exito);
				return Redirect("/Index");
			}

			return Page();
        }

        public IActionResult OnPostEditar()
        {

            if (EsValidoEditar())
            {
                var partialPath = rutaCarpeta + "/images/ImagenesPerfil/" + miembro.usernamePK;
                if (imagenDePerfil != null)
                {
                    var filePath = rutaCarpeta + "/images/ImagenesPerfil/" + miembro.usernamePK + "." + imagenDePerfil.ContentType.Split('/')[1];                    

                    if (System.IO.File.Exists(partialPath + ".png") || System.IO.File.Exists(partialPath + ".jpg") || System.IO.File.Exists(partialPath + ".jpeg"))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        imagenDePerfil.CopyTo(stream);
                    }

                    miembro.rutaImagenPerfil = "images/ImagenesPerfil/" + miembro.usernamePK + "." + imagenDePerfil.ContentType.Split('/')[1];
                }
                else
                {
                    if (System.IO.File.Exists(partialPath + ".png") || System.IO.File.Exists(partialPath + ".jpg") || System.IO.File.Exists(partialPath + ".jpeg"))
                    {
                        //Ya tenía imagen de perfil y no quiso cambiarla
                    }
                    else
                    {
                        miembro.rutaImagenPerfil = "images/ImagenesPerfil/default_avatar.png";
                    }                   
                }


                miembro.idiomas = listaIdiomasSelec;
                miembro.habilidades = listaHabilidadesSelec;
                miembro.pasatiempos = listaPasatiemposSelec;
                editorMiembroController.ActualizarMiembro(miembro);
                AvisosInmediatos.Set(this, "perfilEditado", "Su perfil se ha editado correctamente", AvisosInmediatos.TipoAviso.Exito);
                return Redirect("/MiPerfil");
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
                if (!buscadorMiembrosController.CorreoValido(miembro.email))
                {
                    esValido = false;
                    AvisosInmediatos.Set(this, "emailInvalido", "El correo electrónico ya está asociado a otro usuario. Ingrese otro correo electrónico", AvisosInmediatos.TipoAviso.Error);
                }
			}

			return esValido;
		}

        public bool EsValidoEditar()
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
            }

            return esValido;
        }
    }
}