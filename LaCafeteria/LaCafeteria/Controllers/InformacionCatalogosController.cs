﻿using System;
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

        public List<string> GetHabilidadesCatalogo()
        {
            return informacionCatalogosDBHandler.GetHabilidadesCatalogo();
        }

        public List<string> GetPasatiemposCatalogo()
        {
            return informacionCatalogosDBHandler.GetPasatiemposCatalogo();
        }

        public List<string> GetPaisesCatalogo()
        {
            return informacionCatalogosDBHandler.GetPaisesCatalogo();
        }

    }
}
