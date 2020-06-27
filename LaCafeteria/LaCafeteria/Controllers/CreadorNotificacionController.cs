﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
	public class CreadorNotificacionController
	{
		private CreadorNotificacionDBHandler creadorNotificacionDBHandler;

		public CreadorNotificacionController()
		{
			creadorNotificacionDBHandler = new CreadorNotificacionDBHandler();
		}

		public void CrearNotificacion(Notificacion notificacion)
		{
			creadorNotificacionDBHandler.CrearNotificacion(notificacion);
		}
	}
}