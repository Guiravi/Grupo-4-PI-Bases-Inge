using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;

namespace LaCafeteria.Pages
{
    public class BuscarModel : PageModel
    {
        public List<TopicoModel> Topicos { get; set; }

        [BindProperty]
        public int tiposArticulo { get; set; }

        [BindProperty]
        public string tipoBusqueda { get; set; }

        [BindProperty]
        public string textoBusqueda { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            return Page();
        }





    }

}