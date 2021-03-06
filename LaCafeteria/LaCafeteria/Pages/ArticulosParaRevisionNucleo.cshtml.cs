﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Controllers;
using LaCafeteria.Models;

namespace LaCafeteria.Pages
{
    public class ArticulosParaRevisionNucleoModel : PageModel
    {
		public List<ArticuloModel> listaArticulosParaRevisonNucleo;
		public List<ArticuloModel> listaArticulosNucleoEsSolicitado;
		public List<ArticuloModel> listaArticulosNucleoLeInteresa;

		private BuscadorArticuloController buscadorArticuloController;
		public InformacionArticuloController informacionArticuloController;

		public ArticulosParaRevisionNucleoModel()
		{
			buscadorArticuloController = new BuscadorArticuloController();
			informacionArticuloController = new InformacionArticuloController();
		}

        public void OnGet()
        {
			string usernamePK = Request.Cookies["usernamePK"];
			listaArticulosParaRevisonNucleo = buscadorArticuloController.GetArticulosParaRevisarNucleo(usernamePK);
			listaArticulosNucleoEsSolicitado = buscadorArticuloController.GetArticulosNucleoEsSolicitado(usernamePK);
			listaArticulosNucleoLeInteresa = buscadorArticuloController.GetArticulosNucleoLeInteresa(usernamePK);
		}
    }
}