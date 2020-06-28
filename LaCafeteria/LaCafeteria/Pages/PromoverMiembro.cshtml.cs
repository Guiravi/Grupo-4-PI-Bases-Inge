using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;
using LaCafeteria.Models;
namespace LaCafeteria.Pages
{
    public class PromoverMiembroModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string usernamePK { get; set; }
        [BindProperty(SupportsGet = true)]
        public string nombreRolFK { get; set; }
        [BindProperty(SupportsGet = true)]
        public int aceptar { get; set; }
        public string usernameMiembroFK { get; set; }
        public string rolNucleoFK { get; set; }
        public List<MiembroModel> miembros { get; set; }
        public MiembroController miembroController;
        public  EditorMiembroSolicitaSubirRangoNucleoController editorMiembroSolicitaSubirRangoNucleoController;
        public RevisionSolicitudesPreviasMiembroSubirRangoNucleoController revisionSolicitudesPreviasMiembroSubirRangoNucleoController;
        public PromoverMiembroModel()
        {

            editorMiembroSolicitaSubirRangoNucleoController = new EditorMiembroSolicitaSubirRangoNucleoController();
            revisionSolicitudesPreviasMiembroSubirRangoNucleoController = new RevisionSolicitudesPreviasMiembroSubirRangoNucleoController();
            miembroController = new MiembroController();
           
        }

        public void OnGet()
        {
            usernameMiembroFK = Request.Cookies["usernamePK"];
            if (nombreRolFK != "nulo") {
                rolNucleoFK = miembroController.GetRango(usernameMiembroFK);
                if (aceptar == 1)
                {
                    VotarPromover();
                }
                else {
                    VotarRechazar();
                }

            }
            miembros = miembroController.GetListaMiembrosSolicitud(usernameMiembroFK);
        }

        public void VotarPromover()
        {

            if (nombreRolFK != "Periférico" && nombreRolFK != "Activo" )
            {
                Notificaciones.Set(this, "rangoInvalido", "El rango de este miembro no califica para poder pormoverse", Notificaciones.TipoNotificacion.Error);
            }
            else {
                if (nombreRolFK == "Núcleo")
                {
                    Notificaciones.Set(this, "rangoPeriferico", "El rango de este miembro no se puede votar para aumentar", Notificaciones.TipoNotificacion.Error);
                }
                 else
                {
                    editorMiembroSolicitaSubirRangoNucleoController.VotarPromover(usernamePK,usernameMiembroFK);
                    Notificaciones.Set(this, "exitoVotar", "Su voto ha sido emitido", Notificaciones.TipoNotificacion.Exito);
                    revisarVotosAceptacion();
                }
            }



           
        }

        public void VotarRechazar()
        {

            if (nombreRolFK != "Periférico" && nombreRolFK != "Activo")
            {
                Notificaciones.Set(this, "rangoInvalido", "El rango de este miembro no califica para poder pormoverse", Notificaciones.TipoNotificacion.Error);
            }
            else
            {
                if (nombreRolFK == "Núcleo")
                {
                    Notificaciones.Set(this, "rangoPeriferico", "El rango de este miembro no se puede votar para aumentar", Notificaciones.TipoNotificacion.Error);
                }
                else
                {
                    editorMiembroSolicitaSubirRangoNucleoController.VotarRechazar(usernamePK, usernameMiembroFK);
                    Notificaciones.Set(this, "exitoVotar", "Su voto ha sido emitido", Notificaciones.TipoNotificacion.Exito);
                    revisarVotosRechazo();
                }
            }




        }
        public void revisarVotosAceptacion() {
            int votosTotales = revisionSolicitudesPreviasMiembroSubirRangoNucleoController.VerSiSolicitado(usernamePK);
            int votosAceptados = revisionSolicitudesPreviasMiembroSubirRangoNucleoController.VerTodosSolicitadosAceptados(usernamePK);
            double porcentajeAceptacion = ((double)votosAceptados / (double)votosTotales) * (double)100;
            if (porcentajeAceptacion > 50 || (nombreRolFK == "Activo" && rolNucleoFK == "Coordinador")) {
                miembroController.AscenderMiembro(usernamePK, nombreRolFK);
                editorMiembroSolicitaSubirRangoNucleoController.BorrarSolicitudes(usernamePK);
                /*Notid=ficacion Aceptacion*/
            }
        }

        public void revisarVotosRechazo()
        {
          
            if (nombreRolFK == "Activo" && rolNucleoFK == "Coordinador")
            {
                editorMiembroSolicitaSubirRangoNucleoController.BorrarSolicitudes(usernamePK);
                /*Notificacion Rechazo*/
            }
        }
    }

}