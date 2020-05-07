using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCoffeePlace.Views;
using TheCoffeePlace.Models;
/// <summary>
/// Summary description for TopicoController
/// </summary>
namespace TheCoffeePlace.Controllers
{
	public class TopicoController
	{
		public TopicoController()
		{		
			//
			// TODO: Add constructor logic here
			//
		}

		public void SetTopicos(IView_Articulo view)
		{
			TopicoDBHandler topicoDBHandler = new TopicoDBHandler();
			view.checkBoxList.DataSource = topicoDBHandler.ObtenerAllTopicos();
			view.checkBoxList.DataBind();
		}
	}
}