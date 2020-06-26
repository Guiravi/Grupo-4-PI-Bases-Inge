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



                                                                        

        public void AgregarVisita(int id) {
            articuloDBHandler.AgregarVisita(id);
        }







    }


}
