using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Pages
{
    public class VerResumenModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        [BindProperty(SupportsGet =true)]
        public string tipo { get; set; }

        [BindProperty]
        public ArticuloModel articulo { get; set; }

        private InformacionArticuloController informacionArticuloController;
        private CalificadorDeArticuloController calificadorDeArticuloController;
        private EditorArticuloController editorArticuloController;

        public List<string> topicos { get; set; }

        public string autores { get; set; }

        public int calificacion;

        public string contenido;

        public VerResumenModel()
        {
            informacionArticuloController = new InformacionArticuloController();
            calificadorDeArticuloController = new CalificadorDeArticuloController();
            editorArticuloController = new EditorArticuloController();
        }

        public void OnGet() {

            SetInformacionArticulo();

            if (Request.Cookies["usernamePK"] != null)
            {
                calificacion = informacionArticuloController.GetCalificacionMiembro(Request.Cookies["usernamePK"], idArticuloPK);
            }
           
            TempData["idArticuloPK"] = idArticuloPK;
        }

        public IActionResult OnPostGustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"]; 
            calificacion = 1;
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, 1);
            SetInformacionArticulo();
            TempData["visto"] = 2;
            Notificaciones.Set(this, "meGusta", "Su calificación \"Me gusta\" ha sido guardada", Notificaciones.TipoNotificacion.Exito);

            return Page();
        }

        public IActionResult OnPostIgual()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            calificacion = 0;
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, 0);
            SetInformacionArticulo();
            TempData["visto"] = 2;
            Notificaciones.Set(this, "nulo", "Su calificación \"Nulo\" ha sido guardada", Notificaciones.TipoNotificacion.Exito);

            return Page();
        }

        public IActionResult OnPostDisgustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            calificacion = -1;
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, -1);
            SetInformacionArticulo();
            TempData["visto"] = 2;
            Notificaciones.Set(this, "noMeGusta", "Su calificación \"No me gusta\" ha sido guardada", Notificaciones.TipoNotificacion.Exito);

            return Page();
        }

        public IActionResult OnPostSumar() {
            if (Request.Cookies["usernamePK"] != null)
            {
                idArticuloPK = (int)TempData["idArticuloPK"];
                editorArticuloController.AgregarVisita(idArticuloPK);
                SetInformacionArticulo();
                calificacion = informacionArticuloController.GetCalificacionMiembro(Request.Cookies["usernamePK"], idArticuloPK);
                TempData["visto"] = 1;
                TempData["idArticuloPK"] = idArticuloPK;
            }
            else
            {
                Notificaciones.Set(this, "init_session_error", "Por favor inicie sesión para poder ver el artículo", Notificaciones.TipoNotificacion.Error);
                return Redirect("/Login");
            }
            return Page();
        }

        private void SetInformacionArticulo() {
            articulo = informacionArticuloController.GetInformacionArticuloModel(idArticuloPK);
            autores = informacionArticuloController.GetAutoresDeArticuloString(idArticuloPK);
            topicos = informacionArticuloController.GetCategoriaTopicosArticuloString(idArticuloPK);
            contenido = articulo.contenido;
        }
    }
}