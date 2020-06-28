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

        public BuscadorMiembrosController()
		{
            buscadorMiembroDBHandler = new BuscadorMiembroDBHandler();
        }

		public MiembroModel GetMiembro(string usernamePK)
		{
			return buscadorMiembroDBHandler.GetMiembro(usernamePK);
		}

		public List<MiembroModel> GetListaMiembrosModel()
		{
            return buscadorMiembroDBHandler.GetListaMiembros();
        }

        public List<string> GetListaMiembrosString()
		{
            List<string> stringMiembros = new List<string>();
            List<MiembroModel> listaMiembros = buscadorMiembroDBHandler.GetListaMiembros();

            foreach ( MiembroModel miembro in listaMiembros )
            {
                stringMiembros.Add(miembro.nombre + " " + miembro.apellido1 + " " + miembro.apellido2 + " (" + miembro.usernamePK + ")");
            }

            return stringMiembros;
        }

        public List<MiembroModel> GetListaMiembrosNucleoModel()
		{
            return buscadorMiembroDBHandler.GetListaNucleos();
        }

		public List<MiembroModel>GetListaMiembrosParaSolicitudRevision(int articuloAID)
		{
			return buscadorMiembroDBHandler.GetListaMiembrosParaSolicitudRevision(articuloAID);
		}

		public List<MiembroModel> GetlistaMiembrosParaAsignarRevision(int articuloAID)
		{
			return buscadorMiembroDBHandler.GetlistaMiembrosParaAsignarRevision(articuloAID);
		}

		public List<MiembroModel> GetListaMiembrosInteresados(int articuloAID)
		{
			return buscadorMiembroDBHandler.GetListaMiembrosInteresados(articuloAID);
		}
	}
}
