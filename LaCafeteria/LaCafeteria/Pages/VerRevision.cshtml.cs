using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;
namespace LaCafeteria.Pages
{
    public class VerRevisionModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ponderado { get; set; }

        [BindProperty(SupportsGet = true)]
        public string tipoArticulo { get; set; }

        //public ArticuloController articuloController { get; set; }
        private InformacionArticuloController informacionArticuloController;
        private DocumentosArticuloController documentosArticuloController;
        private EditorArticuloController editorArticuloController;

        [BindProperty]
        public ArticuloModel articulo { get; set; }

        public List<Tuple<string, string, double, double, string, string>> revisiones { get; set; }

        public string articuloPDF = "";

        public string rutaCarpeta = "";

        public VerRevisionModel(IHostingEnvironment env) {
            //articuloController = new ArticuloController();
            informacionArticuloController = new InformacionArticuloController();
            documentosArticuloController = new DocumentosArticuloController();

            rutaCarpeta = env.WebRootPath;
        }

        public void OnGet() {
            articulo = informacionArticuloController.GetInformacionArticuloModel(idArticuloPK);
            revisiones = informacionArticuloController.GetRevisiones(idArticuloPK);
            double meritos = 0.0;
            double puntaje = 0.0;
            if (tipoArticulo == "Largo")
            {
                documentosArticuloController.CargarArticuloPDF(idArticuloPK, rutaCarpeta);
                articuloPDF = Convert.ToString(idArticuloPK) + ".pdf";
            }
            for (int i = 0; i < revisiones.Count; i++)
            {
                meritos += revisiones[i].Item3;
                puntaje += revisiones[i].Item4;
            }

            ponderado = Convert.ToString(puntaje / meritos);
        }

        public IActionResult OnPostRechazar() {
            string estadoArticulo = EstadoArticulo.Rechazado;
            editorArticuloController.ActualizarEstadoArticulo(idArticuloPK, estadoArticulo);

            Notificaciones.Set(this, "Aceptar", "El artículo ha sido rechazado", Notificaciones.TipoNotificacion.Exito);
            return Redirect("/VerRevision/" + idArticuloPK + "/" + tipoArticulo);
        }
        public IActionResult OnPostAceptarConModificaciones() {
            string estadoArticulo = EstadoArticulo.EnCorrecciones;
            editorArticuloController.ActualizarEstadoArticulo(idArticuloPK, estadoArticulo);

            Notificaciones.Set(this, "Aceptar", "El artículo ha sido aceptado con correcciones", Notificaciones.TipoNotificacion.Exito);
            return Redirect("/VerRevision/" + idArticuloPK + "/" + tipoArticulo);
        }
        public IActionResult OnPostAceptar() {
            string estadoArticulo = EstadoArticulo.Publicado;
            editorArticuloController.ActualizarEstadoArticulo(idArticuloPK, estadoArticulo);

            Notificaciones.Set(this, "Aceptar", "El artículo ha sido aceptado", Notificaciones.TipoNotificacion.Exito);
            return Redirect("/VerRevision/" + idArticuloPK + "/" + tipoArticulo);
        }
    }
}