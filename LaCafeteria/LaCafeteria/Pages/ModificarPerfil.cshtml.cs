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

        public MiembroController miembroController { set; get; }

        public ModificarPerfilModel()
        {
            miembroController = new MiembroController();
        }

        public void OnGet()
        {
            miembro = miembroController.GetMiembro(Request.Cookies["usernamePK"]);
        }
        
        public IActionResult OnPostActualizar()
        {
            miembroController.ActualizarMiembro(Request.Cookies["usernamePK"], miembro);
            return Redirect("/MiPerfil");
        }
    }
}