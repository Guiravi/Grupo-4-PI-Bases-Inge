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

        [BindProperty(SupportsGet = true)]
        public int paginaAnterior { get; set; }

        [BindProperty(SupportsGet = true)]
        public string username { get; set; }

        //public ArticuloController articuloController;

        //public MiembroController miembroController;
        private EditorArticuloController editorArticuloController;
        private DocumentosArticuloController documentosArticuloController;
        private InformacionArticuloController informacionArticuloController;
        private CalificadorDeArticuloController calificadorDeArticuloController;

        public int? calificacion;

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
                TempData["calificacion"] = calificacion;
            }
            else
            {
                AvisosInmediatos.Set(this, "init_session_error", "Por favor inicie sesión para poder ver el artículo", AvisosInmediatos.TipoAviso.Error);
                return Redirect("/Login");
            }
            return Page();
}

        public IActionResult OnPostGustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            articuloPDF = (string)TempData["rutaPDF"];
            int? calificacionVieja = (int?)TempData["calificacion"];
            if (calificacionVieja == 1)
            {
                AvisosInmediatos.Set(this, "meGusta", "Se ha eliminado su calificación \"Me gusta\"", AvisosInmediatos.TipoAviso.Exito);
                calificacion = null;
            }
            else
            {
                AvisosInmediatos.Set(this, "meGusta", "Su calificación \"Me gusta\" ha sido guardada", AvisosInmediatos.TipoAviso.Exito);
                calificacion = 1;
                TempData["calificacion"] = 1;
            }
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, 1);
            TempData["idArticuloPK"] = idArticuloPK;
            TempData["rutaPDF"] = articuloPDF;

            return Page();
        }

        public IActionResult OnPostIgual()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            articuloPDF = (string)TempData["rutaPDF"];
            int? calificacionVieja = (int?)TempData["calificacion"];
            if (calificacionVieja == 0)
            {
                AvisosInmediatos.Set(this, "nulo", "Se ha eliminado su calificación \"Nulo\"", AvisosInmediatos.TipoAviso.Exito);
                calificacion = null;
            }
            else
            {
                AvisosInmediatos.Set(this, "nulo", "Su calificación \"Nulo\" ha sido guardada", AvisosInmediatos.TipoAviso.Exito);
                calificacion = 0;
                TempData["calificacion"] = 0;
            }
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, 0);
            TempData["idArticuloPK"] = idArticuloPK;
            TempData["rutaPDF"] = articuloPDF;

            return Page();
        }

        public IActionResult OnPostDisgustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            articuloPDF = (string)TempData["rutaPDF"];
            int? calificacionVieja = (int?)TempData["calificacion"];
            if (calificacionVieja == -1)
            {
                AvisosInmediatos.Set(this, "noMeGusta", "Se ha eliminado su calificación \"No me gusta\"", AvisosInmediatos.TipoAviso.Exito);
                calificacion = null;
            }
            else
            {
                AvisosInmediatos.Set(this, "noMeGusta", "Su calificación \"No me gusta\" ha sido guardada", AvisosInmediatos.TipoAviso.Exito);
                calificacion = -1;
                TempData["calificacion"] = -1;
            }

            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, -1);
            TempData["idArticuloPK"] = idArticuloPK;
            TempData["rutaPDF"] = articuloPDF;

            return Page();
        }
    }
}