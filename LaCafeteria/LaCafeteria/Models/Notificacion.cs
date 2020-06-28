using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models
{
	public class Notificacion
	{
		public static string Nueva = "Nueva";
		public static string Leida = "Leída";

		public int notificacionAID { get; set; }
		public string usernameFK { get; set; }
		public string fechaCreacion { get; set; }
		public string mensaje { get; set; }
		public string estado { get; set; }
		public string url { get; set; }

		public Notificacion()
		{
		}

		public Notificacion(string usernameFK, string mensaje, string url)
		{
			notificacionAID = -1;
			this.usernameFK = usernameFK;
			this.fechaCreacion = DateTime.Now.ToString("yyyy/MM/dd");
			this.mensaje = mensaje;
			this.estado = Nueva;
			this.url = url;
		}
	}
}
