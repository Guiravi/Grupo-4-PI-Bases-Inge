using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class CalificadorDeArticuloController
    {
        private CalificadorDeArticulosDBHandler calificadorDeArticulosDBHandler;

        public CalificadorDeArticuloController() {
            calificadorDeArticulosDBHandler = new CalificadorDeArticulosDBHandler();
        }

        public void CalificarArticulo(string username, int idArticulo, int valorCalif) {
            calificadorDeArticulosDBHandler.CalificarArticulo(username, idArticulo, valorCalif);
        }
    }
}
