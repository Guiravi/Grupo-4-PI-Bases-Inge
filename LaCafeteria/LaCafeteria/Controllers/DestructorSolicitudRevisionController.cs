using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
	public class DestructorSolicitudRevisionController
	{
		private DestructorSolicitudRevisionDBHandler destructorSolicitudRevisionDBHandler;

		public DestructorSolicitudRevisionController()
		{
			destructorSolicitudRevisionDBHandler = new DestructorSolicitudRevisionDBHandler();
		}

		public void DestruirSolicitudRevision(string usernameMiemFK, int idArticuloFK)
		{
			destructorSolicitudRevisionDBHandler.DestruirSolicitudRevision(usernameMiemFK, idArticuloFK);
		}
	}
}
