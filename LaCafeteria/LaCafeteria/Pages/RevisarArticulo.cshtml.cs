using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Utilidades;
using LaCafeteria.Controllers;
using System;
using System.ComponentModel.DataAnnotations;

namespace LaCafeteria.Pages
{
    public class RevisarArticuloModel : PageModel
    {
        RevisorArticulosController revisorArticulosController;

        [BindProperty]
        public int opinion { get; set; }
        
        [BindProperty]
        public int contribucion { get; set; }
        
        [BindProperty]
        public int forma { get; set; }
        
        [BindProperty]
        public int recomendacion { get; set; }

        [BindProperty]
        public string comentario { get; set; }

        public RevisarArticuloModel()
        {
            forma = -1;
            opinion = -1;
            contribucion = -1;
            recomendacion = -1;
            comentario = "";
        }

        public IActionResult OnPostEnviar()
        {
            if (opinion < 0 || contribucion < 0 || forma < 0 || recomendacion < 0 || comentario == "")
            {
                Notificaciones.Set(this, "revision", "Debe realizar todas las calificaciones", Notificaciones.TipoNotificacion.Error);
                return Page();
            }
            string recomend_final = "Rechazar";
            if (recomendacion == 0)
            {
                recomend_final = "Aceptar";
            }
            else if (recomendacion == 1)
            {
                recomend_final = "Aceptar con Modificaciones";
            }

            /* Crear nuevo controlador de revisor de artículo */
            revisorArticulosController.ActualizarRevisionArticulo(opinion,contribucion, forma, "Finalizada", comentario,recomend_final0,0);


            return Page();
        }
    }
}