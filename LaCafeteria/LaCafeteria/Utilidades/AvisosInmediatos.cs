using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LaCafeteria.Utilidades
{
	public static class AvisosInmediatos
	{
		public enum TipoAviso
		{
			Exito = 0,
			Error = 1
		}

		public static void Set(PageModel page , string nombre, string mensaje, TipoAviso tipo)
		{
			

			if (tipo == TipoAviso.Exito)
			{
				page.TempData[nombre] = "<div class=\"alert alert-success\"role=\"alert\">" +
										mensaje +
										"</div>";
			}
			else
			{
				page.TempData[nombre] = "<div class=\"alert alert-danger\"role=\"alert\">" +
                                        "<span class=\"glyphicon glyphicon-exclamation-sign\" aria-hidden=\"true\"></span>" +
                                        "<span class=\"sr-only\">Error:</span>" + " " +
                                        mensaje +
                                        "</div>";
			}

		}

		public static bool Existe(PageModel page, string nombre)
		{
			return page.TempData[nombre] != null;
		}

		public static string Get(PageModel page, string nombre)
		{
			return (string) page.TempData[nombre];
		}
	}
}
