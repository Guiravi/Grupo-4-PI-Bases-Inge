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

		public void GuardarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<string> nombreTopicoPKTopicos)
		{
			articuloDBHandler.GuardarArticulo(articulo, usernamePKMiembrosAutores, nombreTopicoPKTopicos);
		}



        public void EditarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<string> nombreTopicoPKTopicos, string rutaCarpeta)
        {                 
            articuloDBHandler.EditarArticulo(articulo, usernamePKMiembrosAutores, nombreTopicoPKTopicos);
            if (articulo.tipo == "Largo")
            {
                articuloFSHandler.BorrarViejoArchivo(articulo.idArticuloPK, rutaCarpeta);
            }          
        }







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

        public void CargarArticuloDOCX(int idArticulo, string rutaCarpeta)
        {
            if (!articuloFSHandler.YaEstaEnCarpetaDOCX(idArticulo, rutaCarpeta))
            {
                byte[] contenido = articuloDBHandler.DescargarArticuloDocx(idArticulo);
                articuloFSHandler.GuardarArticuloDOCX(idArticulo, contenido, rutaCarpeta);

            }
        }

        public ArticuloModel GetArticuloModelResumen(int id) {
            ArticuloModel articulo = articuloDBHandler.GetInfoPaginaResumen(id);
            return articulo;
        }


        public void ActualizarEstadoArticulo(int id, string estadoArticulo){
            articuloDBHandler.ActualizarEstadoArticulo(id,estadoArticulo);
        }                                                                           

        public void AgregarVisita(int id) {
            articuloDBHandler.AgregarVisita(id);
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
