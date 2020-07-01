using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Pages;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class BuscadorArticuloController
    {
        private BuscadorArticuloDBHandler buscadorArticuloDBHandler;

        public BuscadorArticuloController() {
            buscadorArticuloDBHandler = new BuscadorArticuloDBHandler();
        }

        public List<ArticuloModel> BuscarArticulo(SolicitudBusquedaModel solicitud) {
            List<ArticuloModel> articulos = new List<ArticuloModel>();

            if ( solicitud.tipoBusqueda == "topicos" )
            {

                foreach (string catTop in solicitud.topicos)
                {
                    string[] separacion = catTop.Split(": ");

                    CategoriaTopicoModel modelo = new CategoriaTopicoModel()
                    {
                        nombreCategoriaPK = separacion[0],
                        nombreTopicoPK = separacion[1]
                    };

                    articulos.AddRange(buscadorArticuloDBHandler.GetArticulosPorTopicoYTipo(modelo, solicitud.tiposArticulo));
                }

                articulos = articulos.Distinct(new ItemEqualityComparer()).ToList();

            } else if ( solicitud.tipoBusqueda == "titulos" )
            {
                articulos = buscadorArticuloDBHandler.GetArticulosPorTituloYTipo(solicitud.textoBusqueda, solicitud.tiposArticulo);
            } else
            {
                articulos = buscadorArticuloDBHandler.GetArticulosPorAutorYTipo(solicitud.textoBusqueda, solicitud.tiposArticulo).Distinct(new ItemEqualityComparer()).ToList();
            }

            return articulos;
        }


        public List<ArticuloModel> GetArticulosPorEstado(string estadoArticulo) {
            List<ArticuloModel> artList = buscadorArticuloDBHandler.GetArticulosPorEstado(estadoArticulo);
            return artList;
        }

        public List<ArticuloModel> GetArticulosPorEstadoAsignaMiem(string estadoArticulo, string username)
        {
            List<ArticuloModel> artList = buscadorArticuloDBHandler.GetArticulosPorEstadoAsignaMiem(estadoArticulo, username);
            return artList;
        }


        public List<ArticuloModel> GetArticulosRevisionFinalizada() {
            return buscadorArticuloDBHandler.GetArticulosRevisionFinalizada();
        }

        public List<ArticuloModel> GetTodosArticulos() {
            return buscadorArticuloDBHandler.GetTodosArticulos();
        }

        public List<ArticuloModel> GetArticulosPorMiembro(string username) {
            return buscadorArticuloDBHandler.GetArticulosPorMiembro(username);
        }

		public List<ArticuloModel> GetArticulosParaRevisarNucleo(string usernamePK)
		{
			return buscadorArticuloDBHandler.GetArticulosParaRevisarNucleo(usernamePK);
		}

		public List<ArticuloModel> GetArticulosNucleoEsSolicitado(string usernamePK)
		{
			return buscadorArticuloDBHandler.GetArticulosNucleoEsSolicitado(usernamePK);
		}

		public List<ArticuloModel> GetArticulosNucleoLeInteresa(string usernamePK)
		{
			return buscadorArticuloDBHandler.GetArticulosNucleoLeInteresa(usernamePK);
		}
	}

	class ItemEqualityComparer : IEqualityComparer<ArticuloModel>
    {
        public bool Equals(ArticuloModel x, ArticuloModel y) {
            // Two items are equal if their keys are equal.
            return x.articuloAID == y.articuloAID;
        }

        public int GetHashCode(ArticuloModel obj) {
            return obj.articuloAID.GetHashCode();
        }
    }
}
