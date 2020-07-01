using LaCafeteria.Controllers;
using LaCafeteria.Models;
using LaCafeteria.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Pages
{
    public class ModificarPerfilModel : PageModel
    {
        [BindProperty]
        public MiembroModel miembro { set; get; }
        public EdicionMiembroModel edicionMiembroModel { get; set; }

        private BuscadorMiembrosController buscadorMiembrosController;
        private EditorMiembroController editorArticuloController;
        private InformacionCatalogosController informacionCatalogosController;

        public List<string> listaPasatiempos;
        public string listaPasatiemposCSV;
        public List<string> listaHabilidades;
        public string listaHabilidadesCSV;

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
            listaHabilidades.AddRange(listaHabilidadesCSV.Split(',').ToList());
            listaPasatiempos.AddRange(listaPasatiemposCSV.Split(',').ToList());
            editorArticuloController.ActualizarMiembro(Request.Cookies["usernamePK"], edicionMiembroModel, listaIdiomas, listaHabilidades, listaPasatiempos);
            AvisosInmediatos.Set(this, "Actualizado", "Su perfil se ha actualizado satifactoriamente", AvisosInmediatos.TipoAviso.Exito);
            return Redirect("/MiPerfil");

        }
    }
}