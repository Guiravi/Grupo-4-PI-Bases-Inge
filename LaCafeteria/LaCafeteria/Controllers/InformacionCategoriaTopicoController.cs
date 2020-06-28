using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class InformacionCategoriaTopicoController
    {
        private InformacionCategoriaTopicosDBHandler informacionCategoriaTopicosDBHandler;

        public InformacionCategoriaTopicoController() {
            informacionCategoriaTopicosDBHandler = new InformacionCategoriaTopicosDBHandler();
        }

        public List<CategoriaTopicoModel> GetCategoriasYTopicos() {
            return informacionCategoriaTopicosDBHandler.GetCategoriasYTopicos();
        }

        public List<CategoriaTopicoModel> GetCategoriasTopicosNoAsociadosRol(string rol)
        {
            return informacionCategoriaTopicosDBHandler.GetCategoriasTopicosNoAsociadosRol(rol);
        }
    }
}
