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
        public string tiposArticulo { get; set; }

        [BindProperty]
        public string tipoBusqueda { get; set; }

        [BindProperty]
        public string textoBusqueda { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            SolicitudBusquedaModel solicitud;
            if (tipoBusqueda == "topicos")
            {
                //string tops = script javascript
                solicitud = new SolicitudBusquedaModel(tipoBusqueda, "tops", tiposArticulo, "");
            }
            else
            {
                solicitud = new SolicitudBusquedaModel(tipoBusqueda, "", tiposArticulo, textoBusqueda);
            }

            return Page();
        }

        
        public void OnGetCargarPagina()
        {
            return;
        }

    }

    public class SolicitudBusquedaModel
    {
        public string tipoBusqueda { get; set; }

        public string topicos { get; set; }

        public string tiposArticulo { get; set; }

        public string textoBusqueda { get; set; }

        public SolicitudBusquedaModel(string tipoBus, string tops, string tiposArt, string textB)
        {
            tipoBusqueda = tipoBus;

            topicos = tops;

            tiposArticulo = tiposArt;

            textoBusqueda = textB;

        }

    }

}