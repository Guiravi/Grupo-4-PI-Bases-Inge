using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
	public class AsignadorRevisoresController
	{
		private AsignadorRevisoresDBHandler asignadorRevisoresDBHandler;

		public AsignadorRevisoresController()
		{
			asignadorRevisoresDBHandler = new AsignadorRevisoresDBHandler();
		}

		public void AsignarRevisor(string usernameMiemFK, int idArticuloFK)
		{
			asignadorRevisoresDBHandler.AsignarRevisor(usernameMiemFK, idArticuloFK);
		}
	}
}
