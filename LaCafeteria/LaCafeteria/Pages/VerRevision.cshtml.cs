using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class VerRevisionModel : PageModel
    {
        [BindProperty (SupportsGet = true)]
        public int idArticuloPK { get; set; }

        [BindProperty (SupportsGet = true)]
        public string tipoArticulo { get; set; }

        public ArticuloController articuloController { get; set; }
        public ArticuloModel articulo { get; set; }

        public List<Tuple<string, string, double, string>> revisiones { get; set; }

        public string articuloPDF = "";

        public string rutaCarpeta = "";

        public VerRevisionModel(IHostingEnvironment env) {
            articuloController = new ArticuloController();
            rutaCarpeta = env.WebRootPath;
        }

        public void OnGet()
        {
            articulo = articuloController.GetArticuloModelResumen(idArticuloPK);
            revisiones = articuloController.GetRevisiones(idArticuloPK);

            if (tipoArticulo == "Largo")
            {
            articuloController.CargarArticuloPDF(idArticuloPK, rutaCarpeta);
            articuloPDF = Convert.ToString(idArticuloPK) + ".pdf";
            }
        }
    }
}