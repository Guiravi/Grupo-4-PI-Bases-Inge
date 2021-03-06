﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models
{
    public class MiembroModel
    {
		[BindProperty]
		[Required(ErrorMessage = "Debe introducir un nombre de usuario")]
		public string usernamePK { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Debe introducir un correo electrónico")]
		[EmailAddressAttribute(ErrorMessage = "Debe introducir un correo electrónico válido")]
		public string email { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Debe introducir su nombre")]
		public string nombre { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Debe introducir su primer apellido")]
		public string apellido1 { get; set; }

		[BindProperty]
		public string apellido2 { get; set; }

		[BindProperty]
		public string fechaNacimiento { get; set; }

		[BindProperty]
        [Required(ErrorMessage = "Debe introducir su país")]
        public string paisFK { get; set; }

		[BindProperty]
		public string estado { get; set; }

		[BindProperty]
		public string ciudad { get; set; }

		public string rutaImagenPerfil { get; set; }

		[BindProperty]
		public List<string> pasatiempos { get; set; }

		[BindProperty]
		public List<string> habilidades { get; set; }

		[BindProperty]
		public List<string> idiomas { get; set; }

		[BindProperty]
		public string informacionLaboral { get; set; }

		public double meritos { get; set; }

		public bool activo { get; set; }

		public string nombreRolFK { get; set; }


		public string GetNombreCompleto()
		{
            return nombre + " " + apellido1 + (String.IsNullOrEmpty(apellido2) ? null : " " + apellido2);
		}
	}

    public class EdicionMiembroModel {
        [BindProperty]
        public string apellido2 { get; set; }

        [BindProperty]
        public string fechaNacimiento { get; set; }

        [BindProperty]
        public string estado { get; set; }

        [BindProperty]
        public string ciudad { get; set; }

        [BindProperty]
        public string informacionLaboral { get; set; }
    }
}

