using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Utilidades;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class ModificarPerfilModel : PageModel
    {
        [BindProperty]
        public MiembroModel miembro { set; get; }

        private BuscadorMiembrosController buscadorMiembrosController;
        private EditorMiembroController editorArticuloController;
        private InformacionCatalogosController informacionCatalogosController;

        public List<string> listaIdiomasElegidos { set; get; }

        [BindProperty]
        public List<string> listaIdiomas { set; get; }

        public List<string> listaIdiomasDisponibles { set; get; }
        public ModificarPerfilModel()
        {
            buscadorMiembrosController = new BuscadorMiembrosController();
            editorArticuloController = new EditorMiembroController();
            informacionCatalogosController = new InformacionCatalogosController();

            listaIdiomasElegidos = new List<string>();
            listaIdiomasDisponibles = informacionCatalogosController.GetIdiomasCatalogo();
        }

        public void OnGet()
        {
            miembro = buscadorMiembrosController.GetMiembro(Request.Cookies["usernamePK"]);
            listaIdiomasElegidos = miembro.idiomas;
        }
        
        public IActionResult OnPostActualizar()
        {
            miembro.idiomas = listaIdiomas;
            editorArticuloController.ActualizarMiembro(Request.Cookies["usernamePK"], miembro);
            Notificaciones.Set(this, "Actualizado", "Su perfil se ha actualizado satifactoriamente", Notificaciones.TipoNotificacion.Exito);
            return Redirect("/MiPerfil");

        }
    }
}