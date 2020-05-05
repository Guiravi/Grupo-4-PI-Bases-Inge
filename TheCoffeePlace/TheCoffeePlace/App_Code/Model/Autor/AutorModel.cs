using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AutorModel
/// </summary>
namespace TheCoffeePlace.Models
{
	public class AutorModel
	{
		public int usernamePK { get; set; }
		public String email { get; set; }
		public String nombre { get; set; }
		public String apellido1 { get; set; }
		public String apellido2 { get; set; }
		public String pais { get; set; }
		public String estado { get; set; }
		public String ciudad { get; set; }
		public String imagenPerfil { get; set; }
		public String hobbies { get; set; }
		public String habilidades { get; set; }

		public AutorModel()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
