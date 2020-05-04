using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Articulo
/// </summary>
///
namespace TheCoffeePlace.Models
{ 
	public class Articulo
	{
		public int idArticuloPK { get; set; }
		public String titulo { get; set; }
		public String resumen { get; set; }
		public int tipo { get; set; }
		public String contenido { get; set; }
		public String fechaPublicacion { get; set; }
		public String nombreAutor { get; set; }
		public String usernameFK { get; set; }

		public Articulo()
		{
			

		}
	}
}