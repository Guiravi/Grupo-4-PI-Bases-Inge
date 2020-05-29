using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class VerResumenModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        [BindProperty]
        public ArticuloModel articulo { get; set; }

        public ArticuloController articuloController { get; set; }

        public TopicoController topicoController { get; set; }

        public string topicos { get; set; }

        public string autores { get; set; }

        public void OnGet() {
            articulo = new ArticuloModel();
            articuloController = new ArticuloController();
            topicoController = new TopicoController();

            
        }

        public void OnPost() {

        }
    }
}