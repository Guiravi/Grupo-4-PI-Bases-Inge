using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IView_VerResumen
/// </summary>

namespace TheCoffeePlace.Views
{
    public interface IView_VerResumen
    {
        int idArticuloPK { get; set; }
        string titulo { set; }
        string autor { set; }
        string topicos { set; }
        string resumen { set; }
    }
}