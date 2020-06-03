using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Utilidades
{
	public static class AppSettings
	{
		public static string GetConnectionString()
		{
			return "Data Source=DESKTOP-3SS2QUU;Initial Catalog=Iter2;Integrated Security=True";
		}
	}
}
