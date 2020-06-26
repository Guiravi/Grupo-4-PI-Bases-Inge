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

        public void CargarArticuloDOCX(int idArticulo, string rutaCarpeta) {
            if ( !administradorDeArchivosFSHandler.YaEstaEnCarpetaDOCX(idArticulo, rutaCarpeta) )
            {
                byte[] contenido = informacionArticuloDBHandler.GetBinario(idArticulo);
                administradorDeArchivosFSHandler.GuardarArticuloDOCX(idArticulo, contenido, rutaCarpeta);

            }
        }
    }
}
