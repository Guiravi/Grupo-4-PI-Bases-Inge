using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Articulo
/// </summary>
///
namespace TheCoffeePlace.Models
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

        public ArticuloModel(int idArticulo, String titulo, String resumen, int tipo, String contenido, String fechaPublicacion, String nombreAutor, String usernameFK)
		{
            this.idArticuloPK = idArticulo;
			this.titulo = titulo;
			this.resumen = resumen;
			this.tipo = tipo;
			this.contenido = contenido;
			this.fechaPublicacion = fechaPublicacion;
			this.nombreAutor = nombreAutor;
			this.usernameFK = usernameFK;
		}

        public ArticuloModel(String titulo, String resumen, int tipo, String contenido, String fechaPublicacion, String nombreAutor, String usernameFK)
        {
			this.titulo = titulo;
            this.resumen = resumen;
            this.tipo = tipo;
            this.contenido = contenido;
            this.fechaPublicacion = fechaPublicacion;
            this.nombreAutor = nombreAutor;
            this.usernameFK = usernameFK;
        }
    }
}