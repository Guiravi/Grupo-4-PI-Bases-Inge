using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class RevisorArticulosController
    {
        private RevisorArticuloHandler revisorArticuloHandler;
        
        public RevisorArticulosController()
        {

        }

        public void ActualizarRevisionArticulo(double merito, int opinion, int contribucion, int forma, string estadoRevision,
                                                    string comentarios, string recomendacion, string username, int idArticulo)
        {
            revisorArticuloHandler.ActualizarEstadoRevisionArticulo(merito, opinion, contribucion, forma, estadoRevision,
                                                    comentarios, recomendacion, username, idArticulo);
        }
    }
}
