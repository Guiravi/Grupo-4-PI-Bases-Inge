using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCoffeePlace.Models;
using TheCoffeePlace.Views;

namespace TheCoffeePlace.Controllers
{
	public class TopicoController
	{
		public TopicoController()
		{

		}

        public void GetTopicosArticulo(IView_VerResumen view)
		{
            TopicoDBHandler topicoHandler = new TopicoDBHandler();
            List<TopicoModel> topicos = topicoHandler.ObtenerTopicosArticulo(view.idArticuloPK);

            string msjTopicos = "";

            for ( int i = 0; i < topicos.Count - 1; ++i )
            {
                msjTopicos = msjTopicos + topicos[i].nombre + ", ";
            }

            msjTopicos = msjTopicos + topicos[topicos.Count - 1].nombre;

            view.topicos = msjTopicos;
        }

		public void SetTopicos(IView_Articulo view)
		{
			TopicoDBHandler topicoDBHandler = new TopicoDBHandler();
			view.checkBoxList.DataSource = topicoDBHandler.ObtenerAllTopicos();
			view.checkBoxList.DataBind();
		}
	}
}