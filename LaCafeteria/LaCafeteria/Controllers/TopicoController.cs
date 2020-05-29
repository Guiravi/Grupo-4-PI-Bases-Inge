using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaCafeteria.Models;

namespace LaCafeteria.Controllers
{
	public class TopicoController
	{
        public TopicoDBHandler topicoDBHandler;

        public TopicoController()
        {
            topicoDBHandler = new TopicoDBHandler();
        }

  //      public void GetTopicosArticulo(IView_VerResumen view)
		//{
  //          TopicoDBHandler topicoHandler = new TopicoDBHandler();
  //          List<TopicoModel> topicos = topicoHandler.ObtenerTopicosArticulo(view.idArticuloPK);

  //          string msjTopicos = "";

  //          for ( int i = 0; i < topicos.Count - 1; ++i )
  //          {
  //              msjTopicos = msjTopicos + topicos[i].nombre + ", ";
  //          }

  //          msjTopicos = msjTopicos + topicos[topicos.Count - 1].nombre;

  //          view.topicos = msjTopicos;
  //      }

		public List<TopicoModel> GetListaTopicos()
		{
			return topicoDBHandler.ObtenerAllTopicos();
		}
	}
}