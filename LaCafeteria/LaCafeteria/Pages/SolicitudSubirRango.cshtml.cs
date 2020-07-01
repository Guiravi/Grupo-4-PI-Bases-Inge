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
        public BuscadorMiembrosController buscadorMiembrosController;
        public EditorMiembroSolicitaSubirRangoNucleoController miembroSolicitaSubirRangoNucleoEnviadaController;
        public RevisionSolicitudesPreviasMiembroSubirRangoNucleoController revisionSolicitudesPreviasMiembroSubirRangoNucleoController;
        public CreadorNotificacionController creadorNotificacionController;
        public List<MiembroModel> miembros { get; set; }
        public SolicitudSubirRangoModel()
        {

            buscadorMiembrosController = new BuscadorMiembrosController();
            miembroSolicitaSubirRangoNucleoEnviadaController = new EditorMiembroSolicitaSubirRangoNucleoController();
            revisionSolicitudesPreviasMiembroSubirRangoNucleoController = new RevisionSolicitudesPreviasMiembroSubirRangoNucleoController();
            creadorNotificacionController = new CreadorNotificacionController();
        }
        public void OnGet()
        {

        }

        public IActionResult OnPostSolicitar()
        {
            if (Request.Cookies["usernamePK"] != null)
            {
                miembros = buscadorMiembrosController.GetListaNucleosSolicitud();
                usernamePK = Request.Cookies["usernamePK"];
                nombreRolFK = buscadorMiembrosController.GetRango(usernamePK);
                if (nombreRolFK != "Periférico" && nombreRolFK != "Activo" )
                {
                    AvisosInmediatos.Set(this, "rangoInvalido", "El rango de este miembro no califica para la solicitud", AvisosInmediatos.TipoAviso.Error);
                }
                else
                {
                    int puede = revisionSolicitudesPreviasMiembroSubirRangoNucleoController.VerSiSolicitado(usernamePK);

                    if (puede == 0)
                    {
                        miembroSolicitaSubirRangoNucleoEnviadaController.SolicitarSubirRango(usernamePK, miembros);
                        AvisosInmediatos.Set(this, "exitoSolicitud", "La solicitud se envió con éxito", AvisosInmediatos.TipoAviso.Exito);
                        foreach (var miembro in miembros) {
                            string mensaje = "Hay que revisar la solicitud para subir de rango del miembro "+ usernamePK;
                            Notificacion notificacion = new Notificacion(miembro.usernamePK, mensaje, null);
                            creadorNotificacionController.CrearNotificacion(notificacion);
                        }
                    }
                    else
                    {
                        AvisosInmediatos.Set(this, "fracasoSolicitud", "Usted ha enviado una solicitud que sigue en valoración", AvisosInmediatos.TipoAviso.Error);
                    }
                }
            }
                

            return Page();
        }

    }
}