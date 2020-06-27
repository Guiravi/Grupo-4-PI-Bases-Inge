﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using Microsoft.AspNetCore.Hosting;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Pages
{
    public class VerArticuloLargoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        public ArticuloModel articulo { get; set; }

        //public ArticuloController articuloController;

        //public MiembroController miembroController;
        private EditorArticuloController editorArticuloController;
        private DocumentosArticuloController documentosArticuloController;
        private InformacionArticuloController informacionArticuloController;
        private CalificadorDeArticuloController calificadorDeArticuloController;

        public int calificacion;

        public string articuloPDF = "";

        public string rutaCarpeta = "";

        public VerArticuloLargoModel(IHostingEnvironment env)
        {
            //articuloController = new ArticuloController();
            //miembroController = new MiembroController();
            editorArticuloController = new EditorArticuloController();
            documentosArticuloController = new DocumentosArticuloController();
            informacionArticuloController = new InformacionArticuloController();
            calificadorDeArticuloController = new CalificadorDeArticuloController();

            rutaCarpeta = env.WebRootPath;
        }

        public IActionResult OnGet()
        {
            if (Request.Cookies["usernamePK"] != null)
            {
                editorArticuloController.AgregarVisita(idArticuloPK);
                documentosArticuloController.CargarArticuloPDF(idArticuloPK , rutaCarpeta);
                articuloPDF = Convert.ToString(idArticuloPK) + ".pdf";

                calificacion = informacionArticuloController.GetCalificacionMiembro(Request.Cookies["usernamePK"], idArticuloPK);
                TempData["idArticuloPK"] = idArticuloPK;
                TempData["rutaPDF"] = articuloPDF;
            }
            else
            {
                Notificaciones.Set(this, "init_session_error", "Por favor inicie sesión para poder ver el artículo", Notificaciones.TipoNotificacion.Error);
                return Redirect("/Login");
            }
            return Page();
}

        public IActionResult OnPostGustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            articuloPDF = (string)TempData["rutaPDF"];
            calificacion = 1;
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, 1);
            Notificaciones.Set(this, "meGusta", "Su calificación \"Me gusta\" ha sido guardada", Notificaciones.TipoNotificacion.Exito);

            return Page();
        }

        public IActionResult OnPostIgual()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            articuloPDF = (string)TempData["rutaPDF"];
            calificacion = 0;
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, 0);
            Notificaciones.Set(this, "nulo", "Su calificación \"Nulo\" ha sido guardada", Notificaciones.TipoNotificacion.Exito);

            return Page();
        }

        public IActionResult OnPostDisgustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            articuloPDF = (string)TempData["rutaPDF"];
            calificacion = -1;
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, -1);
            Notificaciones.Set(this, "noMeGusta", "Su calificación \"No me gusta\" ha sido guardada", Notificaciones.TipoNotificacion.Exito);

            return Page();
        }
    }
}