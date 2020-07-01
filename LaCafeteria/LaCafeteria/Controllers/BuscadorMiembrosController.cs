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
        private IBuscadorMiembroDBHandler buscadorMiembroDBHandler;

        public BuscadorMiembrosController()
		{
            buscadorMiembroDBHandler = new BuscadorMiembroDBHandler();
        }

        public BuscadorMiembrosController(IBuscadorMiembroDBHandler buscadorMiembroDBHandler) {
            this.buscadorMiembroDBHandler = buscadorMiembroDBHandler;
        }

        public MiembroModel GetMiembro(string usernamePK)
		{
			return buscadorMiembroDBHandler.GetMiembro(usernamePK);
		}

		public List<MiembroModel> GetListaMiembrosModel()
		{
            return buscadorMiembroDBHandler.GetListaMiembros();
        }

        public List<MiembroModel> GetListaMiembrosDegradarModel()
        {
            return buscadorMiembroDBHandler.GetListaMiembrosDegradar();
        }

        public List<MiembroModel> GetListaNucleosSolicitud()
        {
            return buscadorMiembroDBHandler.GetListaNucleosSolicitud();
        }
        public List<MiembroModel> GetListaMiembrosSolicitud(string usernameMiembroFK)
        {
            return buscadorMiembroDBHandler.GetListaMiembrosSolicitud(usernameMiembroFK);
        }
        public string GetRango(string usernamePK)
        {

            return buscadorMiembroDBHandler.GetRango(usernamePK);

        }
        public List<string> GetListaMiembrosString()
		{
            List<string> stringMiembros = new List<string>();
            List<MiembroModel> listaMiembros = buscadorMiembroDBHandler.GetListaMiembros();

            foreach (MiembroModel miembro in listaMiembros)
            {
                stringMiembros.Add(miembro.nombre + " " + miembro.apellido1 + 
                    (String.IsNullOrEmpty(miembro.apellido2) ? null : " " + miembro.apellido2) + " (" + miembro.usernamePK + ")");
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

        public MiembroModel GetMiembroCoordinador() {
            return buscadorMiembroDBHandler.GetMiembroCoordinador();
        }

		public List<MiembroModel> GetListaMiembrosRevisores(int articuloAID)
		{
			return buscadorMiembroDBHandler.GetListaMiembrosRevisores(articuloAID);
		}
	}
}
