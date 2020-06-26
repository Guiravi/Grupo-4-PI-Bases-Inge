using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class EditorArticuloController
    {
        private EditorArticuloDBHandler editorArticuloDBHandler;
        private AdministradorDeArchivosFSHandler administradorDeArchivosFSHandler;

        public EditorArticuloController() {
            editorArticuloDBHandler = new EditorArticuloDBHandler();
            administradorDeArchivosFSHandler = new AdministradorDeArchivosFSHandler();
        }

        public void ActualizarEstadoArticulo(int id, string estadoArticulo) {
            editorArticuloDBHandler.ActualizarEstadoArticulo(id, estadoArticulo);
        }

        public void AgregarVisita(int id) {
            editorArticuloDBHandler.AgregarVisita(id);
        }

        public void EditarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<string> nombreTopicoPKTopicos, string rutaCarpeta) {
            editorArticuloDBHandler.EditarArticulo(articulo, usernamePKMiembrosAutores, nombreTopicoPKTopicos);
            if ( articulo.tipo == "Largo" )
            {
                administradorDeArchivosFSHandler.BorrarViejoArchivo(articulo.idArticuloPK, rutaCarpeta);
            }
        }
    }
}
