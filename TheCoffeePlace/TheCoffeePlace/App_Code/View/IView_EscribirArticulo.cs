using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for 
/// </summary>
namespace TheCoffeePlace.Views
{
	public interface IView_EscribirArticulo
	{
		String titulo { get; set; }
		String resumen { get; set; }
		String contenido { get; set; }
		int tipo { get; }
		String username { get; }

		void btnGuardar_Click(object sender, EventArgs e);
	}
}