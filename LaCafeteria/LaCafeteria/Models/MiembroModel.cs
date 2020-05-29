using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models
{
    public class MiembroModel
    {
        public string usernamePK { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string fechaNacimiento { get; set; }
        public string pais { get; set; }
        public string estado { get; set; }
        public string ciudad { get; set; }
        public string rutaImagenPerfil { get; set; }
        public string hobbies { get; set; }
        public string habilidades { get; set; }
        public string idiomas { get; set; }
        public string informacionLaboral { get; set; }
        public int meritos { get; set; }
        public bool activo { get; set; }
        public string nombreRolFK { get; set; }

		public string GetNombreCompleto()
		{
			return nombre + " " + apellido1 + " " + apellido2;
		}
	}
}
