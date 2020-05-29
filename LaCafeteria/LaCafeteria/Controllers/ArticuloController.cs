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
        public ArticuloFSHandler articuloFSHandler;

        public ArticuloController()
        {
            articuloDBHandler = new ArticuloDBHandler();
            articuloFSHandler = new ArticuloFSHandler();
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

				articulos = articulos.Distinct(new ItemEqualityComparer()).ToList();

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


		/*
        public List<ArticuloModel> GetArticulosPaginados(int indiceActual, SolicitudBusquedaModel solicitud, int tamanoPag = 8)
        {
            var data = BuscarArticulo(solicitud);
            return data.OrderBy(d => d.fechaPublicacion).Skip((indiceActual - 1) * tamanoPag).Take(tamanoPag).ToList();
        }
        */

        public void CargarArticuloPDF(int idArticulo , string rutaCarpeta)
        {
                    

            if (!articuloFSHandler.YaEstaEnCarpetaPDF(idArticulo , rutaCarpeta))
            {
                if (!articuloFSHandler.YaEstaEnCarpetaDOCX(idArticulo , rutaCarpeta))
                {
                    byte[] contenido = articuloDBHandler.DescargarArticuloDocx(idArticulo);
                    articuloFSHandler.GuardarArticuloDOCX(idArticulo, contenido, rutaCarpeta);
                    
                }

                articuloFSHandler.ConvertirDocxPDF(Convert.ToString(idArticulo), rutaCarpeta);
            }

        }

        public ArticuloModel GetArticuloModelResumen(int id) {
            ArticuloModel articulo = articuloDBHandler.GetInfoPaginaResumen(id);
            return articulo;
        }

    }

	class ItemEqualityComparer : IEqualityComparer<ArticuloModel>
	{
		public bool Equals(ArticuloModel x, ArticuloModel y)
		{
			// Two items are equal if their keys are equal.
			return x.idArticuloPK == y.idArticuloPK;
		}

		public int GetHashCode(ArticuloModel obj)
		{
			return obj.idArticuloPK.GetHashCode();
		}
	}
}
