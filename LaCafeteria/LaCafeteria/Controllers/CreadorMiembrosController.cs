using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class CreadorMiembrosController
    {
        private ICreadorMiembroDBHandler creadorMiembroDBHandler;

        public CreadorMiembrosController(ICreadorMiembroDBHandler creadorMiembroDBHandler) {
            this.creadorMiembroDBHandler = creadorMiembroDBHandler;
        }

        public CreadorMiembrosController() {
            creadorMiembroDBHandler = new CreadorMiembroDBHandler();
        }

        public void CrearMiembro(MiembroModel model) {
            if ( model != null )
            {

                creadorMiembroDBHandler.CrearMiembro(model);
            }
        }
    }
}
