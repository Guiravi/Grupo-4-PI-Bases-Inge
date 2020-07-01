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
    public class AsignarRevisorModel : PageModel
    {
		public List<MiembroModel> listaMiembrosInteresados { set; get; }
		public List<MiembroModel> listaMiembrosRevisores { set; get; }
		public List<MiembroModel> listaMiembrosParaAsignarRevision { set; get; }
		public List<MiembroModel> listaMiembrosParaSolicitudRevision { set; get; }

		public ArticuloModel articulo;

		[BindProperty(SupportsGet = true)]
		public int articuloAID { get; set; }

		[BindProperty]
		public string solicitudUsernamePK { get; set; }

		[BindProperty]
		public List<string> listaSolicitados { set; get; }

		[BindProperty]
		public List<string> listaAsignados { set; get; }

        private CreadorNotificacionController creadorNotificacionController;
		private BuscadorMiembrosController buscadorMiembroController;
		private CreadorSolicitudRevisionController creadorSolicitudRevisionController;
		private InformacionArticuloController informacionArticuloController;
		private AsignadorRevisoresController asignadorRevisoresController;
		private DestructorSolicitudRevisionController destructorSolicitudRevisionController;

		public AsignarRevisorModel()
		{
			listaMiembrosInteresados = new List<MiembroModel>();
			listaMiembrosParaSolicitudRevision = new List<MiembroModel>();
			listaMiembrosParaAsignarRevision = new List<MiembroModel>();
			listaMiembrosRevisores = new List<MiembroModel>();

            creadorNotificacionController = new CreadorNotificacionController();
			buscadorMiembroController = new BuscadorMiembrosController();
			creadorSolicitudRevisionController = new CreadorSolicitudRevisionController();
			informacionArticuloController = new InformacionArticuloController();
			asignadorRevisoresController = new AsignadorRevisoresController();
			destructorSolicitudRevisionController = new DestructorSolicitudRevisionController();
		}

		public void OnGet()
        {
			articulo = informacionArticuloController.GetInformacionArticuloModel(articuloAID);

			listaMiembrosInteresados = buscadorMiembroController.GetListaMiembrosInteresados(articuloAID);

			listaMiembrosParaSolicitudRevision = buscadorMiembroController.GetListaMiembrosParaSolicitudRevision(articuloAID);

			listaMiembrosParaAsignarRevision = buscadorMiembroController.GetlistaMiembrosParaAsignarRevision(articuloAID);

			listaMiembrosRevisores = buscadorMiembroController.GetListaMiembrosRevisores(articuloAID);

			if(!OnGetEsValido())
			{	
				//TODO: Setear notificacion de error
				Redirect("/ArticulosPorRevisar");
			}
		}

		public IActionResult OnPostSolicitarColaboracion()
		{
            if( listaAsignados.Count == 0 )
            {
                AvisosInmediatos.Set(this, "listaSolicitadosVacio", "Se necesita agregar a la lista los miembros nucleos que solicitara colaboracion", AvisosInmediatos.TipoAviso.Error);
            }
			else
            {
                string mensaje = "Se le solicita colaboracion para revisar el articulo: " + articulo.titulo;
                string url = "/ArticulosPAraRevisionNucleo";
                foreach ( string usernameMiemFK in listaSolicitados )
                {
                    creadorSolicitudRevisionController.CrearSolicitudRevision(usernameMiemFK, articuloAID, CreadorSolicitudRevisionController.Solicitado);
                    Notificacion notificacion = new Notificacion(usernameMiemFK, mensaje, url);
                    creadorNotificacionController.CrearNotificacion(notificacion);
                }
            }

			return Redirect("/AsignarRevisor/" + articuloAID);
		}

		public IActionResult OnPostAsignarRevisor()
		{
			if (listaAsignados.Count == 0)
			{
				AvisosInmediatos.Set(this, "listaAsignadosVacio", "Se necesita agregar a la lista los miembros nucleos que va a asignar como revisores", AvisosInmediatos.TipoAviso.Error);
			}
			{
				foreach (string usernameMiemFK in listaAsignados)
				{
					asignadorRevisoresController.AsignarRevisor(usernameMiemFK, articuloAID);
					ArticuloModel articulo = informacionArticuloController.GetInformacionArticuloModel(articuloAID);
					Notificacion notificacion = new Notificacion(usernameMiemFK, "Usted ha sido asignado como revisor del artículo " + articulo.titulo, "/MisArticulosPorRevisar" );
					creadorNotificacionController.CrearNotificacion(notificacion);
				}
			}
			return Redirect("/AsignarRevisor/" + articuloAID);
		}

		public IActionResult OnPostAceptarSolicitud()
		{
			asignadorRevisoresController.AsignarRevisor(solicitudUsernamePK, articuloAID);
			ArticuloModel articulo = informacionArticuloController.GetInformacionArticuloModel(articuloAID);
			Notificacion notificacion = new Notificacion(solicitudUsernamePK, "Usted ha sido aceptado como revisor del artículo " + articulo.titulo, "/MisArticulosPorRevisar");
			creadorNotificacionController.CrearNotificacion(notificacion);
			return Redirect("/AsignarRevisor/" + articuloAID);
		}

		public IActionResult OnPostRechazarSolicitud()
		{
			ArticuloModel articulo = informacionArticuloController.GetInformacionArticuloModel(articuloAID);
			Notificacion notificacion = new Notificacion(solicitudUsernamePK, "Usted ha sido rechazado como revisor del artículo " + articulo.titulo, "/MisArticulosPorRevisar");
			creadorNotificacionController.CrearNotificacion(notificacion);
			destructorSolicitudRevisionController.DestruirSolicitudRevision(solicitudUsernamePK, articuloAID);
			return Redirect("/AsignarRevisor/" + articuloAID);
		}

		public IActionResult OnPostCancelar()
		{
			return Redirect("/AsignarRevisor/" + articuloAID);
		}

		private bool OnGetEsValido()
		{
			bool valido = false;
			if(articulo != null)
			{
				valido = true;
			}

			return valido;
		}
    }
}