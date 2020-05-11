using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AutorModel
/// </summary>
namespace TheCoffeePlace.Models
{
	public class AutorModel
	{
		public String usernamePK { get; set; }
		public String email { get; set; }
		public String nombre { get; set; }
		public String apellido1 { get; set; }
		public String apellido2 { get; set; }
		public String pais { get; set; }
		public String estado { get; set; }
		public String ciudad { get; set; }
		public String imagenPerfil { get; set; }
		public String hobbies { get; set; }
		public String habilidades { get; set; }

		public AutorModel(string usernamePK, string email, string nombre, string apellido1, string apellido2, string pais, string estado, string ciudad, string imagenPerfil, string hobbies, string habilidades)
		{
            this.usernamePK = usernamePK;
            this.email = email;
            this.nombre = nombre;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.pais = pais;
            this.estado = estado;
            this.ciudad = ciudad;
            this.imagenPerfil = imagenPerfil;
            this.hobbies = hobbies;
            this.habilidades = habilidades;
		}

        public AutorModel(string usernamePK, string email, string nombre, string apellido1, string apellido2)
        {
            this.usernamePK = usernamePK;
            this.email = email;
            this.nombre = nombre;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
        }
	}
}
