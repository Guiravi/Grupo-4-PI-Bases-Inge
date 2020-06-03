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

        public ArticuloController articuloController { get; set; }

        public TopicoController topicoController { get; set; }

        public MiembroController miembroController;

        public string topicos { get; set; }

        public string autores { get; set; }

        public int calificacion;

        public string contenido;

        public VerResumenModel()
        {
            articuloController = new ArticuloController();
            topicoController = new TopicoController();
            miembroController = new MiembroController();
        }

        public void OnGet() {
            
            articulo = articuloController.GetArticuloModelResumen(idArticuloPK);
            autores = articuloController.GetAutoresDeArticulo(idArticuloPK);
            topicos = topicoController.GetTopicosArticulo(idArticuloPK);
            contenido = articulo.contenido;

            calificacion = miembroController.GetCalificacionMiembro("BadBunny", idArticuloPK);
            TempData["idArticuloPK"] = idArticuloPK;
        }

        public IActionResult OnPostGustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"]; 
            calificacion = 1;          
            miembroController.CalificarArticulo("BadBunny", idArticuloPK, 1);
            articulo = articuloController.GetArticuloModelResumen(idArticuloPK);
            autores = articuloController.GetAutoresDeArticulo(idArticuloPK);
            topicos = topicoController.GetTopicosArticulo(idArticuloPK);
            contenido = articulo.contenido;
            Notificaciones.Set(this, "meGusta", "Su calificación \"Me gusta\" ha sido guardada", Notificaciones.TipoNotificacion.Exito);

            return Page();
        }

        public IActionResult OnPostIgual()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            calificacion = 0;            
            miembroController.CalificarArticulo("BadBunny", idArticuloPK, 0);
            articulo = articuloController.GetArticuloModelResumen(idArticuloPK);
            autores = articuloController.GetAutoresDeArticulo(idArticuloPK);
            topicos = topicoController.GetTopicosArticulo(idArticuloPK);
            contenido = articulo.contenido;
            Notificaciones.Set(this, "nulo", "Su calificación \"Nulo\" ha sido guardada", Notificaciones.TipoNotificacion.Exito);

            return Page();
        }

        public IActionResult OnPostDisgustar()
        {
            idArticuloPK = (int)TempData["idArticuloPK"];
            calificacion = -1;          
            miembroController.CalificarArticulo("BadBunny", idArticuloPK, -1);
            articulo = articuloController.GetArticuloModelResumen(idArticuloPK);
            autores = articuloController.GetAutoresDeArticulo(idArticuloPK);
            topicos = topicoController.GetTopicosArticulo(idArticuloPK);
            contenido = articulo.contenido;
            Notificaciones.Set(this, "noMeGusta", "Su calificación \"No me gusta\" ha sido guardada", Notificaciones.TipoNotificacion.Exito);

            return Page();
        }

        public void OnPostSumar() {
            articuloController.AgregarVisita(idArticuloPK);
        }
    }
}