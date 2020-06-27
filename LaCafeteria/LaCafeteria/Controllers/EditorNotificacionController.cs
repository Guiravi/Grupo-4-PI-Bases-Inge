using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
	public class EditorNotificacionController
	{
		EditorNotificacionDBHandler editorNotificacionDBHandler;

		public EditorNotificacionController()
		{
			editorNotificacionDBHandler = new EditorNotificacionDBHandler();
		}

		public void ActualizarNotificacion(Notificacion notificacionActualizada)
		{
			editorNotificacionDBHandler.ActualizarNotificacion(notificacionActualizada);
		}
	}
}
