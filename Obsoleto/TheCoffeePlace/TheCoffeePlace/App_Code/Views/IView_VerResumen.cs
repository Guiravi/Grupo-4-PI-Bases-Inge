﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for IView_VerResumen
/// </summary>

namespace TheCoffeePlace.Views
{
    public interface IView_VerResumen
    {
        int idArticuloPK { get; set; }
        int tipo { get; set; }
        string titulo { set; }
        string autor { set; }
        string topicos { set; }
        string resumen { set; }

        void setArticuloCorto(string contenido);
    }
}