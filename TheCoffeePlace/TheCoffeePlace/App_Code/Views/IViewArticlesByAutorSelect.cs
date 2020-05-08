using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
/// <summary>
/// Esta clase correspone a la vista de ArticlesByAutorSelect
/// </summary>
namespace TheCoffeePlace.Views
{
    public interface IViewArticlesByAutorSelect
    {

        String Autor { get; set; }
        String Title{ get; set; }
    }
}