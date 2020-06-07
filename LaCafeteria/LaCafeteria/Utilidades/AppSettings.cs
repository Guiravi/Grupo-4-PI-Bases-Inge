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
			return "Data Source=172.16.202.24;Initial Catalog=BD_Grupo4;Persist Security Info=True;User ID=Grupo4;Password=thecoffeeplace";
		}
	}
}
