using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Pages;
using LaCafeteria.Models;

namespace LaCafeteria.Controllers
{
    public class ArticuloController
    {
        public ArticuloDBHandler articuloDBHandler;

        public ArticuloController()
        {
            articuloDBHandler = new ArticuloDBHandler();
        }

        public List<ArticuloModel> BuscarArticulo(SolicitudBusquedaModel solicitud)
        {
            List<ArticuloModel> articulos = new List<ArticuloModel>();

            if (solicitud.tipoBusqueda == "topicos")
            {
                string[] topicosSep = solicitud.topicos.Split(",");
                for (int i = 0; i < topicosSep.Length; i++)
                {
                    articulos.AddRange(articuloDBHandler.GetArticulosPorTopico(topicosSep[i], solicitud.tiposArticulo));
                }
            }
            else if (solicitud.tipoBusqueda == "titulos")
            {
                articulos = articuloDBHandler.GetArticulosPorTitulo(solicitud.textoBusqueda, solicitud.tiposArticulo);
            }
            else
            {
                articulos = articuloDBHandler.GetArticulosPorAutor(solicitud.textoBusqueda, solicitud.tiposArticulo);
            }

            return articulos;
        }


    }
}
