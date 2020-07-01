using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class EditorMiembroController
    {
        private EditorMiembroDBHandler editorMiembroDBHandler;

        public EditorMiembroController() {
            editorMiembroDBHandler = new EditorMiembroDBHandler();
        }
        public void DegradarMiembro(string usernamePK, string nombreRolFK)
        {
            if (nombreRolFK == "Activo")
            {
                nombreRolFK = "Periférico";
            }
            else
            {
                nombreRolFK = "Activo";
            }

            editorMiembroDBHandler.ModificarMiembro(usernamePK, nombreRolFK);

        }

        public void AscenderMiembro(string usernamePK, string nombreRolFK)
        {
            if (nombreRolFK == "Activo")
            {
                nombreRolFK = "Núcleo";
            }
            else
            {
                nombreRolFK = "Activo";
            }

            editorMiembroDBHandler.ModificarMiembro(usernamePK, nombreRolFK);

        }
        public void ActualizarMiembro(MiembroModel miembro) {
            editorMiembroDBHandler.ActualizarMiembro(miembro);
        }

        public void SetRolMiembro() {

        }
    }
}
