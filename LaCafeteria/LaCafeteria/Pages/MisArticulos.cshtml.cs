using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Controllers;
using LaCafeteria.Models;

namespace LaCafeteria.Pages
{
    public class MisArticulosModel : PageModel
    {
        public List<ArticuloModel> misArticulos { set; get; }

        public int cantResultados { set; get; }

        public TopicoController topicoController;

        public ArticuloController articuloController;

        public MiembroController miembroController;

        public MisArticulosModel()
        {
            topicoController = new TopicoController();
            articuloController = new ArticuloController();
            miembroController = new MiembroController();
        }

        public void OnGet()
        {
            misArticulos = articuloController.GetMisArticulos("nleate8"); //Cambiar por username de sesión
            cantResultados = misArticulos.Count;
        }
    }
}