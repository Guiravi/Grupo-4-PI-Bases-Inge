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

        public void EditarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<string> listaCategoriaTopicoStrings, string rutaCarpeta) {

            List<CategoriaTopicoModel> listaModels = new List<CategoriaTopicoModel>();

            foreach (string catTop in listaCategoriaTopicoStrings)
            {
                string[] separacion = catTop.Split(": ");

                CategoriaTopicoModel modelo = new CategoriaTopicoModel()
                {
                    nombreCategoriaPK = separacion[0],
                    nombreTopicoPK = separacion[1]
                };

                listaModels.Add(modelo);
            }

            editorArticuloDBHandler.EditarArticulo(articulo, usernamePKMiembrosAutores, listaModels);

            if ( articulo.tipo == "Largo" )
            {
                administradorDeArchivosFSHandler.BorrarViejoArchivo(articulo.articuloAID, rutaCarpeta);
            }
        }
    }
}
