using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;

namespace LaCafeteria.Controllers
{
    public class EditorMiembroSolicitaSubirRangoNucleoController
    {
        private EditorMiembroSolicitaSubirRangoNucleoDBHandler editorMiembroSolicitaSubirRangoNucleoDBHandler;

        public EditorMiembroSolicitaSubirRangoNucleoController()
        {
            editorMiembroSolicitaSubirRangoNucleoDBHandler = new EditorMiembroSolicitaSubirRangoNucleoDBHandler();
        }

        public void BorrarSolicitudes(string usernamePK) {
            editorMiembroSolicitaSubirRangoNucleoDBHandler.BorrarSolicitudes(usernamePK);
        }


        public void SolicitarSubirRango(string usernamePK, List<MiembroModel> miembros) {
            editorMiembroSolicitaSubirRangoNucleoDBHandler.SolicitarSubirRango(usernamePK, miembros);
        }
        public void VotarPromover(string usernamePK, string usernameMiembroFK) {
            editorMiembroSolicitaSubirRangoNucleoDBHandler.VotarPromover(usernamePK, usernameMiembroFK);
        }
        public void VotarRechazar(string usernamePK, string usernameMiembroFK)
        {
            editorMiembroSolicitaSubirRangoNucleoDBHandler.VotarRechazar(usernamePK, usernameMiembroFK);
        }
    }	
}
