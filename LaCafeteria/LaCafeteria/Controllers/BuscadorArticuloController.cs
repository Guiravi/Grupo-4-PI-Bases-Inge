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
                string[] topicosSep = solicitud.topicos.Split(",");
                for ( int i = 0; i < topicosSep.Length; i++ )
                {
                    articulos.AddRange(buscadorArticuloDBHandler.GetArticulosPorTopico(topicosSep[i], solicitud.tiposArticulo));
                }

                articulos = articulos.Distinct(new ItemEqualityComparer()).ToList();

            } else if ( solicitud.tipoBusqueda == "titulos" )
            {
                articulos = buscadorArticuloDBHandler.GetArticulosPorTitulo(solicitud.textoBusqueda, solicitud.tiposArticulo);
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


        public List<ArticuloModel> GetArticulosRevisionFinalizada() {
            return buscadorArticuloDBHandler.GetArticulosRevisionFinalizada();
        }

        public List<ArticuloModel> GetTodosArticulos() {
            return buscadorArticuloDBHandler.GetTodosArticulos();
        }

        public List<ArticuloModel> GetArticulosPorMiembro(string username) {
            return buscadorArticuloDBHandler.GetArticulosPorMiembro(username);
        }
    }

    class ItemEqualityComparer : IEqualityComparer<ArticuloModel>
    {
        public bool Equals(ArticuloModel x, ArticuloModel y) {
            // Two items are equal if their keys are equal.
            return x.idArticuloPK == y.idArticuloPK;
        }

        public int GetHashCode(ArticuloModel obj) {
            return obj.idArticuloPK.GetHashCode();
        }
    }
}
