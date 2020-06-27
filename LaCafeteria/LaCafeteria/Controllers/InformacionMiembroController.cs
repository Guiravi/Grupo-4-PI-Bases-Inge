using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;

namespace LaCafeteria.Controllers
{
	public class InformacionMiembroController
	{
		private InformacionMiembroDBHandler informacionMiembroDBHandler;
		
		public InformacionMiembroController()
		{
			informacionMiembroDBHandler = new InformacionMiembroDBHandler();
		}

		public List<Notificacion> GetNotificaciones(string usernamePK)
		{
			List<Notificacion> listaNotificaciones = null;
			listaNotificaciones = informacionMiembroDBHandler.GetNotificaciones(usernamePK);

			return listaNotificaciones;
		}
	}
}
