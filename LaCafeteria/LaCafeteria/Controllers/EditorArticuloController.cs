using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class EditorArticuloController
    {
        private EditorArticuloDBHandler editorArticuloDBHandler;

        public void ActualizarEstadoArticulo(int id, string estadoArticulo) {
            editorArticuloDBHandler.ActualizarEstadoArticulo(id, estadoArticulo);
        }

        public void AgregarVisita(int id) {
            editorArticuloDBHandler.AgregarVisita(id);
        }
    }
}
