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
        private EnviarEmailController enviarEmailController;
        private BuscadorMiembrosController buscadorMiembrosController;

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
            enviarEmailController = new EnviarEmailController(env);
            buscadorMiembrosController = new BuscadorMiembrosController();

            listaMiembros = buscadorMiembrosController.GetListaMiembrosString();

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
            enviarEmailController.EnviarMail(destino, remitente, asunto, mensaje, filePath);

            AvisosInmediatos.Set(this, "Correo Enviado", "Su correo ha sido enviado exitosamente", AvisosInmediatos.TipoAviso.Exito);
            return Redirect("/EnviarCorreo");
        }
    }
}