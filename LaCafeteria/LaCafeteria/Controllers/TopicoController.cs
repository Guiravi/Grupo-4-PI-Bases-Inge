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




        public List<TopicoModel> GetListaTopicos()
		{
			return topicoDBHandler.GetListaTopicos();
		}
	}
}