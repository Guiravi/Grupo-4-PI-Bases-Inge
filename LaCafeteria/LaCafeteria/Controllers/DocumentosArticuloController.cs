using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class DocumentosArticuloController
    {
        private AdministradorDeArchivosFSHandler administradorDeArchivosFSHandler;
        private InformacionArticuloDBHandler informacionArticuloDBHandler;
        private ConvertidorFSHandler convertidorFSHandler;

        public DocumentosArticuloController() {
            administradorDeArchivosFSHandler = new AdministradorDeArchivosFSHandler();
            informacionArticuloDBHandler = new InformacionArticuloDBHandler();
            convertidorFSHandler = new ConvertidorFSHandler();
        }

        public void CargarArticuloDOCX(int idArticulo, string rutaCarpeta) {
            if ( !administradorDeArchivosFSHandler.YaEstaEnCarpetaDOCX(idArticulo, rutaCarpeta) )
            {
                byte[] contenido = informacionArticuloDBHandler.GetBinario(idArticulo);
                administradorDeArchivosFSHandler.GuardarArticuloDOCX(idArticulo, contenido, rutaCarpeta);

            }
        }


        public void CargarArticuloPDF(int idArticulo, string rutaCarpeta) {

            if ( !administradorDeArchivosFSHandler.YaEstaEnCarpetaPDF(idArticulo, rutaCarpeta) )
            {
                if ( !administradorDeArchivosFSHandler.YaEstaEnCarpetaDOCX(idArticulo, rutaCarpeta) )
                {
                    byte[] contenido = informacionArticuloDBHandler.GetBinario(idArticulo);
                    administradorDeArchivosFSHandler.GuardarArticuloDOCX(idArticulo, contenido, rutaCarpeta);

                }
                convertidorFSHandler.ConvertirDocxPDF(Convert.ToString(idArticulo), rutaCarpeta);
            }
        }
    }
}
