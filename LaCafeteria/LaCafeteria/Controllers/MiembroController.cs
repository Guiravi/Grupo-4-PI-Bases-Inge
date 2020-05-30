using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;

namespace LaCafeteria.Controllers
{
	public class MiembroController
	{
		private MiembroDBHandler miembroDBHandler;

		public MiembroController()
		{
			miembroDBHandler = new MiembroDBHandler();
		}

		public List<MiembroModel> GetListaMiembros()
		{
			return miembroDBHandler.GetListaMiembros();
		}

        public int GetCalificacionMiembro(string username, int idArticulo)
        {
            return miembroDBHandler.GetCalificacionMiembro(username, idArticulo);
        }

        public void CalificarArticulo(string username, int idArticulo, int valorCalif)
        {
            miembroDBHandler.CalificarArticulo(username, idArticulo, valorCalif);
        }
	}	
}
