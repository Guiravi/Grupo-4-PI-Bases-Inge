using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for IView_BuscarArticulos
/// </summary>
namespace TheCoffeePlace.Views
{
	public interface IView_BuscarArticulos
	{
		String topico { get; set; }
		GridView gridView { get; }
	}
}