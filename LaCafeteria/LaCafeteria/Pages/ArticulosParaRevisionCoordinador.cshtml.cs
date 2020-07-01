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
        public List<ArticuloModel> listaArticulosRequierenRevision { get; set; }
		public List<ArticuloModel> listaArticulosEnRevision { get; set; }

		public Dictionary<ArticuloModel, List<CategoriaTopicoModel>> dictTopicos { get; set; }

        public Dictionary<ArticuloModel, string> dictAutores { get; set; }
		private BuscadorArticuloController buscadorArticuloController { get; set; }
		public InformacionArticuloController informacionArticuloController { get; set; }

		public ArticulosParaRevisionCoordinadorModel()
		{

            buscadorArticuloController = new BuscadorArticuloController();
            informacionArticuloController = new InformacionArticuloController();

            listaArticulosRequierenRevision = buscadorArticuloController.GetArticulosPorEstado(EstadoArticulo.RequiereRevision);
			listaArticulosEnRevision = buscadorArticuloController.GetArticulosPorEstado(EstadoArticulo.EnRevision);

			dictTopicos = new Dictionary<ArticuloModel, List<CategoriaTopicoModel>>();
            dictAutores = new Dictionary<ArticuloModel, string>();

            for (int i = 0; i< listaArticulosRequierenRevision.Count(); ++i )
            {
                dictTopicos.Add(listaArticulosRequierenRevision[i], informacionArticuloController.GetCategoriaTopicosArticulo(listaArticulosRequierenRevision[i].articuloAID));
                dictAutores.Add(listaArticulosRequierenRevision[i], informacionArticuloController.GetAutoresDeArticuloString(listaArticulosRequierenRevision[i].articuloAID));
            }
        }

        public IActionResult OnGet()
        {
			string usernameFK = Request.Cookies["usernamePK"];
			string nombreRolFK = Request.Cookies["nombreRolFK"];
			if (usernameFK == null || !(nombreRolFK.Equals("Coordinador")))
			{	
				//TODO: Desplegar notificacion de error
				return Redirect("Index");
			}

			return Page();
        }
    }
}