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
        public DegradarMiembroModel() {

            miembroController = new EditorMiembroController();
            buscadorMiembrosController = new BuscadorMiembrosController();
            creadorNotificacionController = new CreadorNotificacionController();
        }

        public void OnGet() {
            if ( nombreRolFK != "nulo" )
            {
                Degradar();
            }
            miembros = buscadorMiembrosController.GetListaMiembrosDegradarModel();
        }

        public void Degradar() {
            if ( nombreRolFK != "Periférico" && nombreRolFK != "Activo" && nombreRolFK != "Núcleo" )
            {
                AvisosInmediatos.Set(this, "rangoInvalido", "El rango de este miembro no califica para degradar", AvisosInmediatos.TipoAviso.Error);
            } else
            {
                if ( nombreRolFK == "Periférico" )
                {
                    AvisosInmediatos.Set(this, "rangoPeriferico", "El rango de este miembro no se puede degradar más", AvisosInmediatos.TipoAviso.Error);
                } else
                {
                    miembroController.DegradarMiembro(usernamePK, nombreRolFK);
                    AvisosInmediatos.Set(this, "exitoDegradar", "El rango de este miembro fue degradado exitosamente", AvisosInmediatos.TipoAviso.Exito);
                    string mensaje = "Su rango ha sido degradado";
                    Notificacion notificacion = new Notificacion(usernamePK, mensaje, null);
                    creadorNotificacionController.CrearNotificacion(notificacion);
                }
            }



        }



    }

}