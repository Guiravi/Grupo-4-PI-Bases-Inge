using System;
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
    public class MiPerfilModel : PageModel
    {
		public List<ArticuloModel> misArticulos { set; get; }

		public MiembroModel miembro { set; get; }

		public int cantResultados { set; get; }

        private BuscadorMiembrosController buscadorMiembrosController;
        private BuscadorArticuloController buscadorArticuloController;
        public InformacionArticuloController informacionArticuloController;
        public string usernamePK;
        public string nombreRolFK;
        public EditorMiembroSolicitaSubirRangoNucleoController miembroSolicitaSubirRangoNucleoEnviadaController;
        public RevisionSolicitudesPreviasMiembroSubirRangoNucleoController revisionSolicitudesPreviasMiembroSubirRangoNucleoController;
        public CreadorNotificacionController creadorNotificacionController;

        public List<MiembroModel> miembros { get; set; }

        public MiPerfilModel()
		{
            buscadorMiembrosController = new BuscadorMiembrosController();
            buscadorArticuloController = new BuscadorArticuloController();
            informacionArticuloController = new InformacionArticuloController();
            miembroSolicitaSubirRangoNucleoEnviadaController = new EditorMiembroSolicitaSubirRangoNucleoController();
            revisionSolicitudesPreviasMiembroSubirRangoNucleoController = new RevisionSolicitudesPreviasMiembroSubirRangoNucleoController();
            creadorNotificacionController = new CreadorNotificacionController();
        }

		public void OnGet()
		{
			// Cargar perfil
			miembro = buscadorMiembrosController.GetMiembro(Request.Cookies["usernamePK"]);
            nombreRolFK = Request.Cookies["nombreRolFK"];
            if (miembro.fechaNacimiento != null)
            {
                miembro.fechaNacimiento = Convertidor.CambiarFormatoFechaDMA(miembro.fechaNacimiento);
            }
                      
			// Cargar articulos
			usernamePK = Request.Cookies["usernamePK"];
			misArticulos = buscadorArticuloController.GetArticulosPorMiembro(usernamePK);
			cantResultados = misArticulos.Count;
		}

        public IActionResult OnPostModificar()
        {
            return Redirect("/ModificarPerfil");
        }
        public IActionResult OnPostSolicitar()
        {
            if (Request.Cookies["usernamePK"] != null)
            {
                miembros = buscadorMiembrosController.GetListaNucleosSolicitud();
                usernamePK = Request.Cookies["usernamePK"];
                nombreRolFK = buscadorMiembrosController.GetRango(usernamePK);
                if (nombreRolFK != "Periférico" && nombreRolFK != "Activo")
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
                        foreach (var miembro in miembros)
                        {
                            string mensaje = "Hay que revisar la solicitud para subir de rango del miembro " + usernamePK;
                            Notificacion notificacion = new Notificacion(miembro.usernamePK, mensaje, "/PromoverMiembro");
                            creadorNotificacionController.CrearNotificacion(notificacion);
                        }
                    }
                    else
                    {
                        AvisosInmediatos.Set(this, "fracasoSolicitud", "Usted ha enviado una solicitud que sigue en valoración", AvisosInmediatos.TipoAviso.Error);
                    }
                }
            }


            return Redirect("/MiPerfil");
        }

    }
}