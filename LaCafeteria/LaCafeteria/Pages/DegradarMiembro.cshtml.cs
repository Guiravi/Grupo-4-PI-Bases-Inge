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
    public class DegradarMiembroModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string usernamePK { get; set; }
        [BindProperty(SupportsGet = true)]
        public string nombreRolFK { get; set; }

        public List<MiembroModel> miembros { get; set; }
        public EditorMiembroController miembroController;
        public BuscadorMiembrosController buscadorMiembrosController;
        public CreadorNotificacionController creadorNotificacionController;
        public DegradarMiembroModel()
        {
            
            miembroController = new EditorMiembroController();
            buscadorMiembrosController = new BuscadorMiembrosController();
            creadorNotificacionController = new CreadorNotificacionController();
        }

        public void OnGet()
        {
            if (nombreRolFK != "nulo") {
                Degradar();
            }
            miembros = buscadorMiembrosController.GetListaMiembrosDegradarModel();
        }

        public void Degradar()
        {
            if (nombreRolFK != "Periférico" && nombreRolFK != "Activo" && nombreRolFK != "Núcleo")
            {
                Notificaciones.Set(this, "rangoInvalido", "El rango de este miembro no califica para degradar", Notificaciones.TipoNotificacion.Error);
            }
            else {
                if (nombreRolFK == "Periférico")
                {
                    Notificaciones.Set(this, "rangoPeriferico", "El rango de este miembro no se puede degradar más", Notificaciones.TipoNotificacion.Error);
                }
                 else
                 {
                    miembroController.DegradarMiembro(usernamePK,nombreRolFK);
                    Notificaciones.Set(this, "exitoDegradar", "El rango de este miembro fue degradado exitosamente", Notificaciones.TipoNotificacion.Exito);
                    string mensaje = "Su rango ha sido degradado";
                    Notificacion notificacion = new Notificacion(usernamePK, mensaje, null);
                    creadorNotificacionController.CrearNotificacion(notificacion);
                 }
            }



            /*
           switch (error)
           {
               case 0:
                   Notificaciones.Set(this, "exitoAgregar", "Se ha agregado la pregunta y la respuesta con éxito en su respectiva categoría", Notificaciones.TipoNotificacion.Exito);
                   return Redirect("/CategoriasPregFrec");
                   //TempData["Message"] = "Se ha agregado la pregunta y la respuesta con éxito en su respectiva categoría";
                   break;
               case 1:
                   Notificaciones.Set(this, "camposIncompletos", "Por favor complete todos los campos antes de solicitar una funcionalidad", Notificaciones.TipoNotificacion.Error);
                   //TempData["Message"] = "Por favor complete todos los campos antes de solicitar una funcionalidad";
                   return Page();
                   break;
               case 2:
                   Notificaciones.Set(this, "camposRepetidos", "Por favor ingrese otra pregunta y respuesta, ya que su pregunta está repetida para la categoría seleccionada", Notificaciones.TipoNotificacion.Error);
                   return Page();
                   //TempData["Message"] = "Por favor ingrese otra pregunta y respuesta, ya que su pregunta está repetida para la categoría seleccionada";
                   break;

           }
           */
        }



    }

}