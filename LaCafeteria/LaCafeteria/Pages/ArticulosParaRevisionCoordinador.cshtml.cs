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
    public class ArticulosParaRevisionCoordinadorModel : PageModel
    {
        private BuscadorArticuloController buscadorArticuloController { get; set; }
        public InformacionArticuloController informacionArticuloController { get; set; }

        public List<ArticuloModel> artList { get; set; }

        public Dictionary<ArticuloModel, List<CategoriaTopicoModel>> dictTopicos { get; set; }

        public Dictionary<ArticuloModel, string> dictAutores { get; set; } 

        public ArticulosParaRevisionCoordinadorModel() {

            buscadorArticuloController = new BuscadorArticuloController();
            informacionArticuloController = new InformacionArticuloController();

            artList = buscadorArticuloController.GetArticulosPorEstado(EstadoArticulo.RequiereRevision);

            dictTopicos = new Dictionary<ArticuloModel, List<CategoriaTopicoModel>>();
            dictAutores = new Dictionary<ArticuloModel, string>();

            for (int i = 0; i< artList.Count(); ++i )
            {
                dictTopicos.Add(artList[i], informacionArticuloController.GetCategoriaTopicosArticulo(artList[i].articuloAID));
                dictAutores.Add(artList[i], informacionArticuloController.GetAutoresDeArticuloString(artList[i].articuloAID));
            }
        }

        public IActionResult OnGet()
        {
			string usernameFK = Request.Cookies["usernamePK"];
			string nombreRolFK = Request.Cookies["nombreRolFK"];
			if (usernameFK == null || !(nombreRolFK.Equals("Coordinador") || nombreRolFK.Equals("Núcleo")))
			{	
				//TODO: Desplegar notificacion de error
				return Redirect("Index");
			}

			return Page();
        }
    }
}