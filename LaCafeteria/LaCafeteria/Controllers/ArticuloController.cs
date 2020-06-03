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
                articulos = articuloDBHandler.GetArticulosPorAutor(solicitud.textoBusqueda, solicitud.tiposArticulo).Distinct(new ItemEqualityComparer()).ToList();
			}

			return articulos;
		}

        public List<ArticuloModel> GetTodosArticulos()
        {
            return articuloDBHandler.GetTodosArticulos();
        }

        public List<ArticuloModel> GetMisArticulos(string username)
        {
            return articuloDBHandler.GetMisArticulos(username);
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

        public String GetAutoresDeArticulo (int id) {
            String autores = "";
            List<MiembroModel> listaAutores = articuloDBHandler.GetAutoresDeArticulo(id);

            for (int i = 0; i<listaAutores.Count-1; ++i )
            {
                autores = autores + listaAutores[i].nombre +" "+ listaAutores[i].apellido1 +" "+ listaAutores[i].apellido2 + ", ";
            }
            autores = autores + listaAutores[listaAutores.Count - 1].nombre + " " +listaAutores[listaAutores.Count - 1].apellido1 + " " + listaAutores[listaAutores.Count - 1].apellido2;

            return autores;
        }

        public void AgregarVisita(int id) {
            articuloDBHandler.AgregarVisita(id);
        }

        public List<ArticuloModel> GetArticulosPorEstado(string estadoArticulo) {
            List<ArticuloModel> artList = articuloDBHandler.GetArticulosPorEstado(estadoArticulo);
            return artList;
        }

        public List<ArticuloModel> GetArticulosRevisionFinalizada() {
            return articuloDBHandler.GetArticulosRevisionFinalizada();
        }

        public string GetRevisoresDeArticulo(int id) {
            string revisores = "";
            List<string> listaRevisores = articuloDBHandler.GetRevisoresDeArticulo(id);

            for ( int i = 0; i < listaRevisores.Count() - 1; i++ )
            {
                revisores = revisores + listaRevisores[i] + ", "; 
            }
            revisores = revisores + listaRevisores[listaRevisores.Count() - 1];
            return revisores;
        }

        public List<Tuple<string, string, double, string>> GetRevisiones (int id) {
            return articuloDBHandler.GetRevisiones(id);
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
