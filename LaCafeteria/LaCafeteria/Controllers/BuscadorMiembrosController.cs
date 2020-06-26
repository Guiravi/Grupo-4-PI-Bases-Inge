using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class BuscadorMiembrosController
    {
        private BuscadorMiembroDBHandler buscadorMiembroDBHandler;

        public List<MiembroModel> GetListaMiembrosModel() {
            return buscadorMiembroDBHandler.GetListaMiembros();
        }
    }
}
