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

        public string GetTopicosArticulo(int id) {
            List<TopicoModel> topicos = topicoDBHandler.ObtenerTopicosArticulo(id);

            string msjTopicos = "";

            for ( int i = 0; i < topicos.Count - 1; ++i ) {
                msjTopicos = msjTopicos + topicos[i].nombre + ", ";
            }

            msjTopicos = msjTopicos + topicos[topicos.Count - 1].nombre;

            return msjTopicos;
        }

        public List<TopicoModel> GetListaTopicos()
		{
			return topicoDBHandler.GetListaTopicos();
		}
	}
}