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

		public int notificacionID { get; set; }
		public string usernameFK { get; set; }
		public string fechaCreacion { get; set; }
		public string mensaje { get; set; }
		public string estado { get; set; }
		public string url { get; set; }

		public Notificacion(string usernameFK, string mensaje, string url)
		{
			notificacionID = -1;
			this.usernameFK = usernameFK;
			this.fechaCreacion = DateTime.Now.ToShortDateString();
			this.mensaje = mensaje;
			this.estado = Nueva;
			this.url = url;
		}
	}
}
