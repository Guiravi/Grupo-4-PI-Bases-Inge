using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaCafeteria.Pages
{
    public class RevisarArticuloModel : PageModel
    {

        [BindProperty]
        public int opinion { get; set; }

        [BindProperty]
        public int contribucion { get; set; }

        [BindProperty]
        public int forma { get; set; }

        [BindProperty]
        public int recomendacion { get; set; }

        [BindProperty]
        public int comentario { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }
    }
}