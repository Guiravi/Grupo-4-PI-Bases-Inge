using System;
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

        public List<ArticuloModel> misArticulosRevision { set; get; }

        public List<ArticuloModel> misArticulosProgreso { set; get; }

        public List<ArticuloModel> misArticulosPublicados { set; get; }

        public List<ArticuloModel> misArticulosCorregir { set; get; }

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
            misArticulosRevision = buscadorArticuloController.GetArticulosPorMiembroEstado(usernamePK,EstadoArticulo.EnRevision);
            misArticulosProgreso = buscadorArticuloController.GetArticulosPorMiembroEstado(usernamePK, EstadoArticulo.EnProgreso);
            misArticulosPublicados = buscadorArticuloController.GetArticulosPorMiembroEstado(usernamePK, EstadoArticulo.Publicado);
            misArticulosCorregir = buscadorArticuloController.GetArticulosPorMiembroEstado(usernamePK, EstadoArticulo.EnCorrecciones);
        }
    }
}