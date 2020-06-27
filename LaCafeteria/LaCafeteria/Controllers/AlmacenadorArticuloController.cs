﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class AlmacenadorArticuloController
    {
        private AlmacenadorArticuloDBHandler almacenadorArticuloDBHandler;

        public AlmacenadorArticuloController() {
            almacenadorArticuloDBHandler = new AlmacenadorArticuloDBHandler();
        }

        public void GuardarArticulo(ArticuloModel articulo, List<string> usernamePKMiembrosAutores, List<CategoriaTopicoModel> categoriaTopicoModels) {
            almacenadorArticuloDBHandler.GuardarArticulo(articulo, usernamePKMiembrosAutores, categoriaTopicoModels);
        }
    }
}
