using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Utilidades;
using LaCafeteria.Controllers;
using LaCafeteria.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;

namespace LaCafeteria.Pages
{
    public class RevisarArticuloModel : PageModel
    {
        private CreadorNotificacionController creadorNotificacionController;
        private BuscadorMiembrosController buscadorMiembrosController;
        private BuscadorArticuloController buscadorArticuloController;
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

        [BindProperty]
        public List<string> topicos { get; set; }

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

        public RevisionModel revisionModel { get; set; }

        public RevisarArticuloModel(IHostingEnvironment env)
        {
            buscadorArticuloController = new BuscadorArticuloController();
            revisorArticulosController = new RevisorArticulosController();
            informacionMiembroController = new InformacionMiembroController();
            informacionArticuloController = new InformacionArticuloController();
            documentosArticuloController = new DocumentosArticuloController();
            creadorNotificacionController = new CreadorNotificacionController();
            buscadorMiembrosController = new BuscadorMiembrosController();

            forma = -1;
            opinion = -1;
            contribucion = -1;
            recomendacion = -1;
            comentario = "";

            rutaCarpeta = env.WebRootPath;
        }

        public void OnGet() {
            articulo = informacionArticuloController.GetInformacionArticuloModel(idArticuloPK);
            autores = informacionArticuloController.GetAutoresDeArticuloString(idArticuloPK);
            topicos = informacionArticuloController.GetCategoriaTopicosArticuloString(idArticuloPK);
            if ( tipoArticulo == "Largo" )
            {
                documentosArticuloController.CargarArticuloPDF(idArticuloPK, rutaCarpeta);
                articuloPDF = Convert.ToString(idArticuloPK) + ".pdf";
            }
            List<RevisionModel> revisionesModel = informacionArticuloController.GetRevisiones(idArticuloPK);
            revisionModel = revisionesModel.Find(x => x.usernameMiemFK == Request.Cookies["usernamePK"]);
        }

        public IActionResult OnPostEnviar()
        {
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

            RevisionModel revision = new RevisionModel();
            revision.opinion = opinion;
            revision.contribucion = contribucion;
            revision.forma = forma;
            revision.estadoRevision = "Finalizada";
            revision.comentarios = comentario;
            revision.recomendacion = recomend_final;
            revision.usernameMiemFK = Request.Cookies["usernamePK"];
            revision.idArticuloFK = idArticuloPK;

            /* Crear nuevo controlador de revisor de artículo */
            revisorArticulosController.ActualizarRevisionArticulo(revision);

            List <ArticuloModel> articulosRevisionFinalizada = buscadorArticuloController.GetArticulosRevisionFinalizada();

            bool revFinalizada = false;

            foreach ( ArticuloModel articuloRev in articulosRevisionFinalizada )
            {
                if ( idArticuloPK == articuloRev.articuloAID )
                {
                    revFinalizada = true;
                }
            }

            if ( revFinalizada )
            {
                string mensaje = "Estimado Coordinador, se ha finalizado las revisiones del articulo " + articulo.titulo + " . Por favor proceder a realizar un veredicto.";
                string url = "/ArticulosParaRevisionCoordinador";

                Notificacion notificacion = new Notificacion(buscadorMiembrosController.GetMiembroCoordinador().usernamePK, mensaje, url);

                creadorNotificacionController.CrearNotificacion(notificacion);
            }

            AvisosInmediatos.Set(this, "revisionExitosa", "Su revisión se ha efectuado exitosamente", AvisosInmediatos.TipoAviso.Exito);
            return Redirect("MisArticulosPorRevisar");
        }
    }
}