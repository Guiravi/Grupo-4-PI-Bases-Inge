using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCoffeePlace.Models;

/// <summary>
/// Summary description for IView_EscribirEmail
/// </summary>
/// 
namespace TheCoffeePlace.Views
{
    public interface IView_EnviarEmail
    {
        List<string> listaAutores { get; set; }
        string destino { get; }
        string asunto { get; }
        string mensaje { get; }
    }
}