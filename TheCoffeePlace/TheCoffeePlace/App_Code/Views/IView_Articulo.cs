using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for 
/// </summary>
namespace TheCoffeePlace.Views
{
	public interface IView_Articulo
	{
		String titulo { get; set; }
		String resumen { get; set; }	
		int tipo { get; }
		String username { get; }

	}

    public interface IView_ImagenArticulo
    {
        string nombreArchivoFileUpImagen { get; }
        byte[] getContenidoFileUpImagen();
        void setContenidoGridViewImagenes(DataSet ds);

    }

    public interface IView_EscribirArticulo : IView_Articulo
    {
        String contenido { get; set; }

    }

    public interface IView_SubirArticulo : IView_Articulo
    {
        byte[] contenido { get; }

    }
}