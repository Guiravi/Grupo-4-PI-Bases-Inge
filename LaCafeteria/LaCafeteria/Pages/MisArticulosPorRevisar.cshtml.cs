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
    public class MisArticulosPorRevisarModel : PageModel
    {
        private BuscadorArticuloController buscadorArticuloController { get; set; }
        public InformacionArticuloController informacionArticuloController { get; set; }

        public List<ArticuloModel> artList { get; set; }

        public Dictionary<ArticuloModel, List<CategoriaTopicoModel>> dictTopicos { get; set; }

        public Dictionary<ArticuloModel, string> dictAutores { get; set; } 

        public MisArticulosPorRevisarModel() {

            buscadorArticuloController = new BuscadorArticuloController();
            informacionArticuloController = new InformacionArticuloController();
        }

        public void OnGet()
        {

			artList = buscadorArticuloController.GetArticulosPorEstadoAsignaMiem("En Revisión", Request.Cookies["usernamePK"]);

			dictTopicos = new Dictionary<ArticuloModel, List<CategoriaTopicoModel>>();
			dictAutores = new Dictionary<ArticuloModel, string>();

			for (int i = 0; i < artList.Count(); ++i)
			{
				dictTopicos.Add(artList[i], informacionArticuloController.GetCategoriaTopicosArticulo(artList[i].articuloAID));
				dictAutores.Add(artList[i], informacionArticuloController.GetAutoresDeArticuloString(artList[i].articuloAID));
			}
		}
    }
}