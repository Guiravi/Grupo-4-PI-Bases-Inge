using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class InformacionCatalogosController
    {
        private InformacionCatalogosDBHandler informacionCatalogosDBHandler;

        public InformacionCatalogosController() {
            informacionCatalogosDBHandler = new InformacionCatalogosDBHandler();
        }

        public List<string> GetIdiomasCatalogo() {
            return informacionCatalogosDBHandler.GetIdiomasCatalogo();
        }
    }
}
