using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

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
		public List<string> listaSolicitados { set; get; }

		[BindProperty]
		public List<string> listaAsignados { set; get; }

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
			// TODO: Validar informacion necesaria para solicitar colaboracion
			foreach(string usernameMiemFK in listaSolicitados)
			{
				creadorSolicitudRevisionController.CrearSolicitudRevision(usernameMiemFK, articuloAID, CreadorSolicitudRevisionController.Solicitado);
			}
			
			return Redirect("/AsignarRevisor/" + articuloAID);
		}

		public IActionResult OnPostAsignarRevisor()
		{
			// TODO: Validar informacion necesaria para asignar revision
			foreach (string usernameMiemFK in listaAsignados)
			{
				asignadorRevisoresController.AsignarRevisor(usernameMiemFK, articuloAID);
			}

			return Redirect("/AsignarRevisor/" + articuloAID);
		}

		public IActionResult OnPostAceptarSolicitud()
		{
			asignadorRevisoresController.AsignarRevisor(solicitudUsernamePK, articuloAID);
			return Redirect("/AsignarRevisor/" + articuloAID);
		}

		public IActionResult OnPostRechazarSolicitud()
		{
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