using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaCafeteria.Models
{ 
	public class ArticuloModel
	{
		public int idArticuloPK { get; set; }
		public String titulo { get; set; }
        public String tipo { get; set; }
        public String fechaPublicacion { get; set; }
		public String resumen { get; set; }
		public String contenido { get; set; }
		public String estado { get; set; }
        public int visitas { get; set; }
        public double puntajeTotalRev { get; set; }
		public int calificacionTotalMiem { get; set; }
    }
}