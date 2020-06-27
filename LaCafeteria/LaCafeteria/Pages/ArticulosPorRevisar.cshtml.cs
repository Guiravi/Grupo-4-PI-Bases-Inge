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
    public class ArticulosPorRevisarModel : PageModel
    {
        private BuscadorArticuloController buscadorArticuloController { get; set; }
        private InformacionArticuloController informacionArticuloController { get; set; }

        public List<ArticuloModel> artList { get; set; }

        public Dictionary<ArticuloModel, string> dictTopicos { get; set; }

        public Dictionary<ArticuloModel, string> dictAutores { get; set; } 

        public ArticulosPorRevisarModel() {

            buscadorArticuloController = new BuscadorArticuloController();
            informacionArticuloController = new InformacionArticuloController();

            artList = buscadorArticuloController.GetArticulosPorEstado(EstadoArticulo.RequiereRevision);

            dictTopicos = new Dictionary<ArticuloModel, string>();
            dictAutores = new Dictionary<ArticuloModel, string>();

            for (int i = 0; i<artList.Count(); ++i )
            {
                dictTopicos.Add(artList[i], informacionArticuloController.GetTopicosArticuloString(artList[i].articuloAID));
                dictAutores.Add(artList[i], informacionArticuloController.GetAutoresDeArticuloString(artList[i].articuloAID));
            }
        }

        public void OnGet()
        {

        }
    }
}