using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Pages
{
    public class EnviarCorreoModel : PageModel
    {
        public CorreoController correoController { get; set; }
        public List<string> listaMiembros { get; set; }

        public string remitente { get; set; }

        [BindProperty]
        public string destino { get; set; }

        [BindProperty]
        public string asunto { get; set; }

        [BindProperty]
        public string mensaje { get; set; }

        [BindProperty]
        public IFormFile archivoAdjunto { get; set; }

        public IHostingEnvironment env { get; set; }

        public string rutaCarpeta { get; set; }

        public EnviarCorreoModel(IHostingEnvironment env) {
            correoController = new CorreoController(env);
            listaMiembros = correoController.getListaMiembrosString();

            rutaCarpeta = env.WebRootPath;
        }

        public void OnGet() {
        }

        public IActionResult OnPost() {
            remitente = Request.Cookies["usernamePK"];

            string filePath = "";

            if ( archivoAdjunto != null )
            {
                filePath = rutaCarpeta + "/Correos/" + archivoAdjunto.FileName;
                using ( var stream = System.IO.File.Create(filePath) )
                {
                    archivoAdjunto.CopyTo(stream);
                }
            }
            correoController.sendMail(destino, remitente, asunto, mensaje, filePath);

            Notificaciones.Set(this, "Correo Enviado", "Su correo ha sido enviado exitosamente", Notificaciones.TipoNotificacion.Exito);
            return Redirect("/EnviarCorreo");
        }
    }
}