﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using LaCafeteria.Controllers;
using LaCafeteria.Models;

namespace LaCafeteria.Pages
{
    public class MisArticulosModel : PageModel
    {
        public List<ArticuloModel> misArticulos { set; get; }

        public int cantResultados { set; get; }

        private BuscadorArticuloController buscadorArticuloController;
        public InformacionArticuloController informacionArticuloController;

        public MisArticulosModel()
        {
            buscadorArticuloController = new BuscadorArticuloController();
            informacionArticuloController = new InformacionArticuloController();
        }

        public void OnGet()
        {	
			string usernamePK = Request.Cookies["usernamePK"];
			misArticulos = buscadorArticuloController.GetArticulosPorMiembro(usernamePK);
            cantResultados = misArticulos.Count;
        }
    }
}