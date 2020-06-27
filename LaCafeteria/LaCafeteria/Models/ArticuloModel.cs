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
	
	public static class EstadoArticulo
	{
		public const string EnProgreso = "En Progreso";
		public const string RequiereRevision = "Requiere Revisión";
		public const string EnRevision = "En Revisión";
        public const string Rechazado = "Rechazado";
        public const string EnCorrecciones = "Aceptado con Correcciones";
		public const string Publicado = "Publicado";
	}

	public static class TipoArticulo
	{
		public const string Corto = "Corto";
		public const string Largo = "Largo";
		public const string Link = "Link";
		
	}

	public class ArticuloModel
	{	
		public int articuloAID { get; set; }

		[Required(ErrorMessage ="Debe incluir un título para su artículo")]
		public String titulo { get; set; }

        public String tipo { get; set; }

		[Required(ErrorMessage = "Debe incluir una fecha de publicación para su artículo")]
		public String fechaPublicacion { get; set; }

		[Required(ErrorMessage = "Debe incluir un resumen para su artículo")]
		public String resumen { get; set; }

		[Required(ErrorMessage = "Su artículo debe tener contenido")]
		public String contenido { get; set; }

		public String estado { get; set; }

        public int visitas { get; set; }

        public double? puntajeTotalRev { get; set; }

		public int calificacionTotalMiem { get; set; }

    }
}