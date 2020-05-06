using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for IView_ImagenArticulo
/// </summary>

namespace TheCoffeePlace.Views
{
    public interface IView_ImagenArticulo
    {
        string nombreArchivoFileUpImagen { get; }
        byte[] getContenidoFileUpImagen();
        void setContenidoGridViewImagenes(DataSet ds);
   
    }
}
