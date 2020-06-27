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

        //public MiembroController miembroController { set; get; }
        private BuscadorMiembrosController buscadorMiembrosController;
        private EditorMiembroController editorArticuloController;

        public List<string> listaIdiomasElegidos { set; get; }

        [BindProperty]
        public List<string> listaIdiomas { set; get; }

        public List<string> listaIdiomasDisponibles { set; get; }
        public ModificarPerfilModel()
        {
            buscadorMiembrosController = new BuscadorMiembrosController();
            editorArticuloController = new EditorMiembroController();

            listaIdiomasElegidos = new List<string>();
            listaIdiomasDisponibles = new List<string>();
        }

        public void OnGet()
        {
            miembro = buscadorMiembrosController.GetMiembro(Request.Cookies["usernamePK"]);
            listaIdiomasDisponibles.Add("Español");
            listaIdiomasDisponibles.Add("Inglés");
            listaIdiomasDisponibles.Add("Árabe");
            listaIdiomasDisponibles.Add("Chino");
            listaIdiomasDisponibles.Add("Ruso");
            string idiomas = miembro.idiomas;
            //string idiomas = "Español,Chino,Inglés";
            if (idiomas != null)
            {
                listaIdiomasElegidos = idiomas.Split(',').ToList();
                while (listaIdiomasElegidos.Count() < listaIdiomasDisponibles.Count())
                {
                    listaIdiomasElegidos.Add("");
                }
            }
            
        }
        
        public IActionResult OnPostActualizar()
        {
            miembro.idiomas = ObtenerIdiomasCSV();
            editorArticuloController.ActualizarMiembro(Request.Cookies["usernamePK"], miembro);
            Notificaciones.Set(this, "Actualizado", "Su perfil se ha actualizado satifactoriamente", Notificaciones.TipoNotificacion.Exito);
            return Redirect("/MiPerfil");

        }

        public string ObtenerIdiomasCSV()
        {
            string idiomas = null;

            if (listaIdiomas.Count > 0)
            {
                idiomas = "";
                foreach (string idioma in listaIdiomas)
                {
                    idiomas += idioma + ",";
                }
                idiomas = idiomas.TrimEnd(',');
            }

            return idiomas;
        }

    }
}