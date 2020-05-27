using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;

namespace LaCafeteria.Pages
{
    public class VerResumenModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int idArticulo { get; set; }

        //[BindProperty]
        //public ArticuloModel articulo { get; set; }

        public string articulo_titulo { get; set; }

        public void OnGet() {

            articulo_titulo = "Titulo de artículo";
        }

        public void OnPost() {

        }
    }
}