﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;
namespace LaCafeteria.Pages
{
    public class VerRevisionModel : PageModel
    {
        [BindProperty (SupportsGet = true)]
        public int idArticuloPK { get; set; }

        [BindProperty (SupportsGet = true)]
        public string tipoArticulo { get; set; }

        public ArticuloController articuloController { get; set; }

        [BindProperty]
        public ArticuloModel articulo { get; set; }

        public List<Tuple<string, string, double, string>> revisiones { get; set; }

        public string articuloPDF = "";

        public string rutaCarpeta = "";

        public VerRevisionModel(IHostingEnvironment env) {
            articuloController = new ArticuloController();
            rutaCarpeta = env.WebRootPath;
            articulo = articuloController.GetArticuloModelResumen(idArticuloPK);
            revisiones = articuloController.GetRevisiones(idArticuloPK);
         
        }

        public void OnGet()
        {
            

            if (tipoArticulo == "Largo")
            {
            articuloController.CargarArticuloPDF(idArticuloPK, rutaCarpeta);
            articuloPDF = Convert.ToString(idArticuloPK) + ".pdf";
            }
            else
            {

            }
        }
        public IActionResult OnPostRechazar()
        {
            return Page();

        }
        public IActionResult OnPostAceptarConModificaciones()
        {
            return Page();
        }
        public IActionResult OnPostAceptar()
        {
            string estadoArticulo = EstadoArticulo.Publicado;
            articuloController.ActualizarEstadoArticulo(idArticuloPK, estadoArticulo);

            Notificaciones.Set(this, "Aceptar", "El artículo ha sido aceptado", Notificaciones.TipoNotificacion.Exito);
            return Redirect("/VerRevision/"+idArticuloPK+"/"+tipoArticulo);
            //return Page();
        }
    }
}