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

        [BindProperty(SupportsGet = true)]
        public int paginaAnterior { get; set; }

        [BindProperty(SupportsGet = true)]
        public string username { get; set; }

        [BindProperty]
        public ArticuloModel articulo { get; set; }

        private InformacionArticuloController informacionArticuloController;
        private CalificadorDeArticuloController calificadorDeArticuloController;
        private EditorArticuloController editorArticuloController;

        public List<string> topicos { get; set; }

        public string autores { get; set; }

        public int? calificacion;

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
                TempData["calificacion"] = calificacion;              
            }
           
            TempData["idArticuloPK"] = idArticuloPK;
        }

        public IActionResult OnPostGustar()
        {            
            idArticuloPK = (int)TempData["idArticuloPK"];
            int? calificacionVieja = (int?)TempData["calificacion"];
            if (calificacionVieja == 1)
            {
                AvisosInmediatos.Set(this, "meGusta", "Se ha eliminado su calificación \"Me gusta\"", AvisosInmediatos.TipoAviso.Exito);
                calificacion = null;
            }
            else
            {
                AvisosInmediatos.Set(this, "meGusta", "Su calificación \"Me gusta\" ha sido guardada", AvisosInmediatos.TipoAviso.Exito);
                calificacion = 1;
                TempData["calificacion"] = 1;
            }           
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, 1);
            SetInformacionArticulo();
            TempData["visto"] = 2;
            TempData["idArticuloPK"] = idArticuloPK;           

            return Page();
        }

        public IActionResult OnPostIgual()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            int? calificacionVieja = (int?)TempData["calificacion"];
            if (calificacionVieja == 0)
            {
                AvisosInmediatos.Set(this, "nulo", "Se ha eliminado su calificación \"Nulo\"", AvisosInmediatos.TipoAviso.Exito);
                calificacion = null;
            }
            else
            {
                AvisosInmediatos.Set(this, "nulo", "Su calificación \"Nulo\" ha sido guardada", AvisosInmediatos.TipoAviso.Exito);
                calificacion = 0;
                TempData["calificacion"] = 0;
            }          
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, 0);
            SetInformacionArticulo();
            TempData["visto"] = 2;
            TempData["idArticuloPK"] = idArticuloPK;         

            return Page();
        }

        public IActionResult OnPostDisgustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            int? calificacionVieja = (int?)TempData["calificacion"];
            if (calificacionVieja == -1)
            {
                AvisosInmediatos.Set(this, "noMeGusta", "Se ha eliminado su calificación \"No me gusta\"", AvisosInmediatos.TipoAviso.Exito);
                calificacion = null;
            }
            else
            {
                AvisosInmediatos.Set(this, "noMeGusta", "Su calificación \"No me gusta\" ha sido guardada", AvisosInmediatos.TipoAviso.Exito);
                calificacion = -1;
                TempData["calificacion"] = -1;
            }
            calificadorDeArticuloController.CalificarArticulo(Request.Cookies["usernamePK"], idArticuloPK, -1);
            SetInformacionArticulo();
            TempData["visto"] = 2;
            TempData["idArticuloPK"] = idArticuloPK;
            

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
                AvisosInmediatos.Set(this, "init_session_error", "Por favor inicie sesión para poder ver el artículo", AvisosInmediatos.TipoAviso.Error);
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