using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class ArticulosRevisadosModel : PageModel
    {
        public ArticuloController articuloController { get; set; }
        public TopicoController topicoController { get; set; }

        public List<ArticuloModel> artList { get; set; }

        public Dictionary<ArticuloModel, string> dictTopicos { get; set; }

        public Dictionary<ArticuloModel, string> dictAutores { get; set; }

        public Dictionary<ArticuloModel, string> dictRevisores { get; set; }

        public ArticulosRevisadosModel() {
            articuloController = new ArticuloController();
            topicoController = new TopicoController();

            artList = articuloController.GetArticulosRevisionFinalizada();

            dictTopicos = new Dictionary<ArticuloModel, string>();
            dictAutores = new Dictionary<ArticuloModel, string>();
            dictRevisores = new Dictionary<ArticuloModel, string>();

            for ( int i = 0; i < artList.Count(); ++i )
            {
                dictTopicos.Add(artList[i], topicoController.GetTopicosArticulo(artList[i].idArticuloPK));
                dictAutores.Add(artList[i], articuloController.GetAutoresDeArticulo(artList[i].idArticuloPK));
                dictRevisores.Add(artList[i], articuloController.GetRevisoresDeArticulo(artList[i].idArticuloPK));
            }
        }

        public void OnGet()
        {

        }
    }
}