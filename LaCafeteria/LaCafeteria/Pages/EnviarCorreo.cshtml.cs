using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using LaCafeteria.Controllers;

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

        public EnviarCorreoModel(IHostingEnvironment env) {
            correoController = new CorreoController(env);
            listaMiembros = correoController.getListaMiembrosString();
            remitente = Request.Cookies["usernamePK"];
        }

        public void OnGet()
        {

        }
    }
}