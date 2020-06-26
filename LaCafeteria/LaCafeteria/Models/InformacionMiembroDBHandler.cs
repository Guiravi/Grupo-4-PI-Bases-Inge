using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models
{
	public class InformacionMiembroDBHandler
	{
		public List<Notificacion> GetNotificaciones(string usernamePK)
		{	
			// TODO: Conectar a base de datos para obtener notificaciones reales ordenadas por fecha
			List<Notificacion> listaNotificaciones = null;

			listaNotificaciones = new List<Notificacion>();
			listaNotificaciones.Add(new Notificacion("BadBunny", "Su articulo YHLQMDLG ha sido aceptado", "/MiPerfil"));
			listaNotificaciones.Add(new Notificacion("BadBunny", "Usted ha sido promovido a Miembro de nucleo!", "#"));
			listaNotificaciones.Add(new Notificacion("BadBunny", "Su articulo Bichiyal ha recibido 1 like", "#"));
			listaNotificaciones.Add(new Notificacion("BadBunny", "Se solicita su colaboracion para revisar el articulo Oda al mono motorizado", "#"));

			return listaNotificaciones;
		}
	}
}
