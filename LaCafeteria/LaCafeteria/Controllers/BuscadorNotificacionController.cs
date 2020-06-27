using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
	public class BuscadorNotificacionController
	{
		private BuscadorNotificacionDBHandler buscadorNotificacionDBHandler;

		public BuscadorNotificacionController()
		{
			buscadorNotificacionDBHandler = new BuscadorNotificacionDBHandler();
		}

		public Notificacion GetNotificacion(int notificacionAID)
		{
			return buscadorNotificacionDBHandler.GetNotificacion(notificacionAID);
		}
	}
}
