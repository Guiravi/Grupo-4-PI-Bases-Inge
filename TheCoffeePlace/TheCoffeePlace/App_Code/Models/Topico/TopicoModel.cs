using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TopicoModel
/// </summary>
namespace TheCoffeePlace.Models
{
	public class TopicoModel
	{
		public string nombre { get; set; }

		public TopicoModel(string nombre)
		{
			this.nombre = nombre;
		}
	}
}