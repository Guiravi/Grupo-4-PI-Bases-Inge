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

        public List<string> getListaMiembrosString() {
            List<string> stringMiembros = new List<string>();
            List<MiembroModel> listaMiembros = buscadorMiembroDBHandler.GetListaMiembros();

            foreach ( MiembroModel miembro in listaMiembros )
            {
                stringMiembros.Add(miembro.nombre + " " + miembro.apellido1 + " " + miembro.apellido2 + " (" + miembro.usernamePK + ")");
            }

            return stringMiembros;
        }
    }
}
