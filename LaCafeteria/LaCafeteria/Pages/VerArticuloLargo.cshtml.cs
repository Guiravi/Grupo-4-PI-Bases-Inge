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

        public MiembroController miembroController;

        public int calificacion;

        public string articuloPDF = "";

        public string rutaCarpeta = "";

        public VerArticuloLargoModel(IHostingEnvironment env)
        {
            articuloController = new ArticuloController();
            miembroController = new MiembroController();

            rutaCarpeta = env.WebRootPath;
        }

        public void OnGet()
        {
            articuloController.CargarArticuloPDF(idArticuloPK , rutaCarpeta);
            articuloPDF = Convert.ToString(idArticuloPK) + ".pdf";

            calificacion = miembroController.GetCalificacionMiembro("BadBunny", idArticuloPK);
            TempData["idArticuloPK"] = idArticuloPK;
            TempData["rutaPDF"] = articuloPDF;
        }

        public IActionResult OnPostGustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            articuloPDF = (string)TempData["rutaPDF"];
            calificacion = 1;
            miembroController.CalificarArticulo("BadBunny", idArticuloPK, 1);

            return Page();
        }

        public IActionResult OnPostIgual()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            articuloPDF = (string)TempData["rutaPDF"];
            calificacion = 0;
            miembroController.CalificarArticulo("BadBunny", idArticuloPK, 0);

            return Page();
        }

        public IActionResult OnPostDisgustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            articuloPDF = (string)TempData["rutaPDF"];
            calificacion = -1;
            miembroController.CalificarArticulo("BadBunny", idArticuloPK, -1);

            return Page();
        }
    }
}