using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaCafeteria.Models
{ 
	public class ArticuloModel
	{	
		public int idArticuloPK { get; set; }
		[Required(ErrorMessage ="Debe incluir un titulo para su articulo")]
		public String titulo { get; set; }
        public String tipo { get; set; }
		[Required]
		public String fechaPublicacion { get; set; }
		[Required]
		public String resumen { get; set; }
		[Required]
		public String contenido { get; set; }
		public String estado { get; set; }
        public int visitas { get; set; }
        public double puntajeTotalRev { get; set; }
		public int calificacionTotalMiem { get; set; }
    }
}