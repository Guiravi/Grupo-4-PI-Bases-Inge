using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Utilidades;
using LaCafeteria.Controllers;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;

namespace LaCafeteria.Pages
{
    public class RevisarArticuloModel : PageModel
    {
        private RevisorArticulosController revisorArticulosController;
        private InformacionMiembroController informacionMiembroController;
        private InformacionArticuloController informacionArticuloController;
        private DocumentosArticuloController documentosArticuloController;

        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        [BindProperty(SupportsGet = true)]
        public string tipoArticulo { get; set; }

        [BindProperty]
        public ArticuloModel articulo { get; set; }

        public string articuloPDF = "";

        public string rutaCarpeta = "";

        public string autores = "";

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

        public RevisarArticuloModel(IHostingEnvironment env)
        {
            revisorArticulosController = new RevisorArticulosController();
            informacionMiembroController = new InformacionMiembroController();
            informacionArticuloController = new InformacionArticuloController();
            documentosArticuloController = new DocumentosArticuloController();

            forma = -1;
            opinion = -1;
            contribucion = -1;
            recomendacion = -1;
            comentario = "";

            autores = informacionArticuloController.GetAutoresDeArticuloString(idArticuloPK);

            rutaCarpeta = env.WebRootPath;
        }

        public void OnGet() {
            articulo = informacionArticuloController.GetInformacionArticuloModel(idArticuloPK);
            if ( tipoArticulo == "Largo" )
            {
                documentosArticuloController.CargarArticuloPDF(idArticuloPK, rutaCarpeta);
                articuloPDF = Convert.ToString(idArticuloPK) + ".pdf";
            } 
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

            double merito = informacionMiembroController.GetMeritos(Request.Cookies["usernamePK"]);

            /* Crear nuevo controlador de revisor de artículo */
            revisorArticulosController.ActualizarRevisionArticulo(merito,opinion,contribucion, forma, "Finalizada", 
                comentario,recomend_final,Request.Cookies["usernamePK"],0);


            Notificaciones.Set(this, "revisionExitosa", "Su revisión se ha efectuado exitosamente", Notificaciones.TipoNotificacion.Exito);
            return Redirect("MisArticulosPorRevisar");
        }
    }
}