﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaCafeteria.Models
{ 
	public class ArticuloModel
	{
		public int idArticuloPK { get; set; }
		public String titulo { get; set; }
		public String resumen { get; set; }
		public int tipo { get; set; }
		public String contenido { get; set; }
		public String fechaPublicacion { get; set; }
		public String nombreAutor { get; set; }
		public String usernameFK { get; set; }
    }
}