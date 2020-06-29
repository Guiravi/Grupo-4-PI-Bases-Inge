using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class RevisorArticulosController
    {
        private RevisorArticuloDBHandler revisorArticuloHandler;
        private CreadorNotificacionController creadorNotificacionController;
        private BuscadorMiembrosController buscadorMiembrosController;
        private InformacionArticuloController informacionArticuloController;
        
        public RevisorArticulosController()
        {
            revisorArticuloHandler = new RevisorArticuloDBHandler();
            creadorNotificacionController = new CreadorNotificacionController();
            buscadorMiembrosController = new BuscadorMiembrosController();
            informacionArticuloController = new InformacionArticuloController();
        }

        public void ActualizarRevisionArticulo(RevisionModel revision)
        {
            revisorArticuloHandler.ActualizarEstadoRevisionArticulo(revision);

            NotificarCoordinadorRevisionFinalizada(revision.idArticuloFK);
        }

        private void NotificarCoordinadorRevisionFinalizada(int id) {
            if ( revisorArticuloHandler.ArticuloConRevisionFinalizada(id) )
            {
                MiembroModel coordinador = buscadorMiembrosController.GetMiembroCoordinador();
                ArticuloModel articulo = informacionArticuloController.GetInformacionArticuloModel(id);
                string mensaje = "El artículo " + articulo.titulo + " ha sido revisado por todos sus revisores. Por favor tomar una decisión.";
                Notificacion notificacion = new Notificacion(coordinador.usernamePK, mensaje, "/ArticulosRevisados");
                creadorNotificacionController.CrearNotificacion(notificacion);
            }
        }

    }
}
