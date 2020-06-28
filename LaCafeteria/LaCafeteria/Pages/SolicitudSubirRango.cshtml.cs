using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;
using LaCafeteria.Models;
namespace LaCafeteria.Pages
{
    public class SolicitudSubirRangoModel : PageModel
    {
        string usernamePK;
        string nombreRolFK;
        public MiembroController miembroController;
        public EditorMiembroSolicitaSubirRangoNucleoController miembroSolicitaSubirRangoNucleoEnviadaController;
        public RevisionSolicitudesPreviasMiembroSubirRangoNucleoController revisionSolicitudesPreviasMiembroSubirRangoNucleoController;
        public List<MiembroModel> miembros { get; set; }
        public SolicitudSubirRangoModel()
        {

            miembroController = new MiembroController();
            miembroSolicitaSubirRangoNucleoEnviadaController = new EditorMiembroSolicitaSubirRangoNucleoController();
            revisionSolicitudesPreviasMiembroSubirRangoNucleoController = new RevisionSolicitudesPreviasMiembroSubirRangoNucleoController();
        }
        public void OnGet()
        {

        }

        public IActionResult OnPostSolicitar()
        {
            if (Request.Cookies["usernamePK"] != null)
            {
                miembros = miembroController.GetListaNucleosSolicitud();
                usernamePK = Request.Cookies["usernamePK"];
                nombreRolFK = miembroController.GetRango(usernamePK);
                if (nombreRolFK != "Periférico" && nombreRolFK != "Activo" )
                {
                    Notificaciones.Set(this, "rangoInvalido", "El rango de este miembro no califica para la solicitud", Notificaciones.TipoNotificacion.Error);
                }
                else
                {
                    int puede = revisionSolicitudesPreviasMiembroSubirRangoNucleoController.VerSiSolicitado(usernamePK);

                    if (puede == 0)
                    {
                        miembroSolicitaSubirRangoNucleoEnviadaController.SolicitarSubirRango(usernamePK, miembros);
                        Notificaciones.Set(this, "exitoSolicitud", "La solicitud se envió con éxito", Notificaciones.TipoNotificacion.Exito);
                    }
                    else
                    {
                        Notificaciones.Set(this, "fracasoSolicitud", "Usted ha enviado una solicitud que sigue en valoración", Notificaciones.TipoNotificacion.Error);
                    }
                }
            }
                

            return Page();
        }

    }
}