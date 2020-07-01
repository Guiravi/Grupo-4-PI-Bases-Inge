using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class CalificadorDeArticuloController
    {
        private ICalificadorDeArticulosDBHandler calificadorDeArticulosDBHandler;

        public CalificadorDeArticuloController(ICalificadorDeArticulosDBHandler calificadorDeArticulosDBHandler) {
            this.calificadorDeArticulosDBHandler = calificadorDeArticulosDBHandler;
        }

        public CalificadorDeArticuloController() {
            calificadorDeArticulosDBHandler = new CalificadorDeArticulosDBHandler();
        }

        public void CalificarArticulo(string username, int idArticulo, int valorCalif) {
            if ( valorCalif >= -1 && valorCalif <= 1 )
            {
                calificadorDeArticulosDBHandler.CalificarArticulo(username, idArticulo, valorCalif);
            }
        }
    }
}
