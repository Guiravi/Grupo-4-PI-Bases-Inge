using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using Microsoft.AspNetCore.Hosting;

namespace LaCafeteria.Pages
{
    public class VerArticuloLargoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        public ArticuloModel articulo { get; set; }

        public ArticuloController articuloController;

        public string articuloPDF = "";

        public string rutaCarpeta = "";

        public VerArticuloLargoModel(IHostingEnvironment env)
        {
            articuloController = new ArticuloController();

            rutaCarpeta = env.WebRootPath;
        }

        public void OnGet()
        {
            articuloController.GetRutaArticuloPDF(idArticuloPK , rutaCarpeta);
            articuloPDF = Convert.ToString(idArticuloPK) + ".pdf";
        }
    }
}