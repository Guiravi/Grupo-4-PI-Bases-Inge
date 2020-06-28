using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
	public class CreadorSolicitudRevisionController
	{
		public const string Interesa = "Interesa";

		public const string Solicitado = "Solicitado";

		private CreadorSolicitudRevisionDBHandler creadorSolicitudRevisionDBHandler;

		public CreadorSolicitudRevisionController()
		{
			creadorSolicitudRevisionDBHandler = new CreadorSolicitudRevisionDBHandler();
		}

		public void CrearSolicitudRevision(string usernameMiemFK, int idArticuloFK, string estado)
		{
			creadorSolicitudRevisionDBHandler.CrearSolicitudRevision(usernameMiemFK, idArticuloFK, estado);
		}
	}
}
