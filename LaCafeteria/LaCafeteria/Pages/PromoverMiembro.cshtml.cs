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
        public EditorMiembroController miembroController;
        public BuscadorMiembrosController buscadorMiembroController;
        public EditorMiembroSolicitaSubirRangoNucleoController editorMiembroSolicitaSubirRangoNucleoController;
        public RevisionSolicitudesPreviasMiembroSubirRangoNucleoController revisionSolicitudesPreviasMiembroSubirRangoNucleoController;
        public CreadorNotificacionController creadorNotificacionController;
        public PromoverMiembroModel() {

            editorMiembroSolicitaSubirRangoNucleoController = new EditorMiembroSolicitaSubirRangoNucleoController();
            revisionSolicitudesPreviasMiembroSubirRangoNucleoController = new RevisionSolicitudesPreviasMiembroSubirRangoNucleoController();
            buscadorMiembroController = new BuscadorMiembrosController();
            creadorNotificacionController = new CreadorNotificacionController();
            miembroController = new EditorMiembroController();
        }

        public void OnGet() {
            usernameMiembroFK = Request.Cookies["usernamePK"];
            if ( nombreRolFK != null )
            {
                rolNucleoFK = buscadorMiembroController.GetRango(usernameMiembroFK);
                if ( aceptar == 1 )
                {
                    VotarPromover();
                } else
                {
                    VotarRechazar();
                }

            }
            miembros = buscadorMiembroController.GetListaMiembrosSolicitud(usernameMiembroFK);
        }

        public void VotarPromover() {

            if ( nombreRolFK != "Periférico" && nombreRolFK != "Activo" )
            {
                AvisosInmediatos.Set(this, "rangoInvalido", "El rango de este miembro no califica para poder pormoverse", AvisosInmediatos.TipoAviso.Error);
            } else
            {
                if ( nombreRolFK == "Núcleo" )
                {
                    AvisosInmediatos.Set(this, "rangoPeriferico", "El rango de este miembro no se puede votar para aumentar", AvisosInmediatos.TipoAviso.Error);
                } else
                {
                    editorMiembroSolicitaSubirRangoNucleoController.VotarPromover(usernamePK, usernameMiembroFK);
                    AvisosInmediatos.Set(this, "exitoVotar", "Su voto ha sido emitido", AvisosInmediatos.TipoAviso.Exito);
                    revisarVotosAceptacion();
                }
            }




        }

        public void VotarRechazar() {

            if ( nombreRolFK != "Periférico" && nombreRolFK != "Activo" )
            {
                AvisosInmediatos.Set(this, "rangoInvalido", "El rango de este miembro no califica para poder pormoverse", AvisosInmediatos.TipoAviso.Error);
            } else
            {
                if ( nombreRolFK == "Núcleo" )
                {
                    AvisosInmediatos.Set(this, "rangoPeriferico", "El rango de este miembro no se puede votar para aumentar", AvisosInmediatos.TipoAviso.Error);
                } else
                {
                    editorMiembroSolicitaSubirRangoNucleoController.VotarRechazar(usernamePK, usernameMiembroFK);
                    AvisosInmediatos.Set(this, "exitoVotar", "Su voto ha sido emitido", AvisosInmediatos.TipoAviso.Exito);
                    revisarVotosRechazo();
                }
            }




        }
        public void revisarVotosAceptacion() {
            int votosTotales = revisionSolicitudesPreviasMiembroSubirRangoNucleoController.VerSiSolicitado(usernamePK);
            int votosAceptados = revisionSolicitudesPreviasMiembroSubirRangoNucleoController.VerTodosSolicitadosAceptados(usernamePK);
            double porcentajeAceptacion = ((double) votosAceptados / (double) votosTotales) * (double) 100;
            if ( porcentajeAceptacion > 50 || (nombreRolFK == "Activo" && rolNucleoFK == "Coordinador") )
            {
                miembroController.AscenderMiembro(usernamePK, nombreRolFK);
                editorMiembroSolicitaSubirRangoNucleoController.BorrarSolicitudes(usernamePK);
                string mensaje = "Su rango ha sido promovido satisfactoriamente";
                Notificacion notificacion = new Notificacion(usernamePK, mensaje, null);
                creadorNotificacionController.CrearNotificacion(notificacion);
                /*Notid=ficacion Aceptacion*/
            } else
            {
                int votosRechazados = revisionSolicitudesPreviasMiembroSubirRangoNucleoController.VerTodosSolicitadosRechazados(usernamePK);
                double porcentajeRechazados = ((double) votosRechazados / (double) votosTotales) * (double) 100;
                if ( porcentajeRechazados == 50 && porcentajeAceptacion == 50 )
                {
                    string mensaje = "Su rango no pudo ser promovido dado un empate de votos";
                    Notificacion notificacion = new Notificacion(usernamePK, mensaje, null);
                    creadorNotificacionController.CrearNotificacion(notificacion);
                    editorMiembroSolicitaSubirRangoNucleoController.BorrarSolicitudes(usernamePK);
                }
            }
        }

        public void revisarVotosRechazo() {
            int votosTotales = revisionSolicitudesPreviasMiembroSubirRangoNucleoController.VerSiSolicitado(usernamePK);
            int votosRechazados = revisionSolicitudesPreviasMiembroSubirRangoNucleoController.VerTodosSolicitadosRechazados(usernamePK);
            double porcentajeRechazados = ((double) votosRechazados / (double) votosTotales) * (double) 100;
            if ( porcentajeRechazados > 50 || (nombreRolFK == "Activo" && rolNucleoFK == "Coordinador") )
            {

                editorMiembroSolicitaSubirRangoNucleoController.BorrarSolicitudes(usernamePK);
                string mensaje = "Su rango no ha sido promovido ya que no tuvo la aprobación mínima";
                Notificacion notificacion = new Notificacion(usernamePK, mensaje, null);
                creadorNotificacionController.CrearNotificacion(notificacion);
                /*Notificacion Rechazo*/
            } else
            {
                int votosAceptados = revisionSolicitudesPreviasMiembroSubirRangoNucleoController.VerTodosSolicitadosAceptados(usernamePK);
                double porcentajeAceptacion = ((double) votosAceptados / (double) votosTotales) * (double) 100;
                if ( porcentajeRechazados == 50 && porcentajeAceptacion == 50 )
                {
                    string mensaje = "Su rango no pudo ser promovdio dado un empate de votos";
                    Notificacion notificacion = new Notificacion(usernamePK, mensaje, null);
                    creadorNotificacionController.CrearNotificacion(notificacion);
                    editorMiembroSolicitaSubirRangoNucleoController.BorrarSolicitudes(usernamePK);
                }
            }
        }
    }

}