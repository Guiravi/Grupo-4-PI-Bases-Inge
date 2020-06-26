using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Models.Handlers
{
    public class EditorMiembroController
    {
        private EditorMiembroDBHandler editorMiembroDBHandler;

        public void ActualizarMiembro(string usernamePK, MiembroModel miembro) {
            editorMiembroDBHandler.ActualizarMiembro(usernamePK, miembro);
        }

        public void SetRolMiembro() {

        }
    }
}
