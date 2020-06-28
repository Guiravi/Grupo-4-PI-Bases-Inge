using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;

namespace LaCafeteria.Controllers
{
    public class RevisionSolicitudesPreviasMiembroSubirRangoNucleoController
    {
        public RevisionSolicitudesPreviasMiembroSubirRangoNucleoDBHandler revisionSolicitudesPreviasMiembroSubirRangoNucleoDBHandler;

        public RevisionSolicitudesPreviasMiembroSubirRangoNucleoController()
        {
            revisionSolicitudesPreviasMiembroSubirRangoNucleoDBHandler = new RevisionSolicitudesPreviasMiembroSubirRangoNucleoDBHandler();
        }
        public int VerSiSolicitado(string usernamePK)
        {
            return revisionSolicitudesPreviasMiembroSubirRangoNucleoDBHandler.VerSiSolicitado(usernamePK);
        }
        public int VerTodosSolicitadosAceptados(string usernamePK) {
            return revisionSolicitudesPreviasMiembroSubirRangoNucleoDBHandler.VerTodosSolicitadosAceptados(usernamePK);
        }
        public int VerTodosSolicitadosRechazados(string usernamePK)
        {
            return revisionSolicitudesPreviasMiembroSubirRangoNucleoDBHandler.VerTodosSolicitadosRechazados(usernamePK);
        }
    }
    
}
