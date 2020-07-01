using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class AlmacenadorArticuloController
    {
        private IAlmacenadorArticuloDBHandler almacenadorArticuloDBHandler;

        public AlmacenadorArticuloController(IAlmacenadorArticuloDBHandler almacenadorArticuloDBHandler) {
            this.almacenadorArticuloDBHandler = almacenadorArticuloDBHandler;
        }

        public AlmacenadorArticuloController() {
            almacenadorArticuloDBHandler = new AlmacenadorArticuloDBHandler();
        }

        public void GuardarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<string> categoriaTopicoStrings) {
            if ( articulo != null && usernamePKMiembrosAutores.Count > 0 && categoriaTopicoStrings.Count > 0 )
            {

                List<CategoriaTopicoModel> listaModels = new List<CategoriaTopicoModel>();

                foreach ( string catTop in categoriaTopicoStrings )
                {
                    string[] separacion = catTop.Split(": ");

                    CategoriaTopicoModel modelo = new CategoriaTopicoModel()
                    {
                        nombreCategoriaPK = separacion[0],
                        nombreTopicoPK = separacion[1]
                    };

                    listaModels.Add(modelo);
                }

                almacenadorArticuloDBHandler.GuardarArticulo(articulo, usernamePKMiembrosAutores, listaModels);
            }
        }
    }
}
