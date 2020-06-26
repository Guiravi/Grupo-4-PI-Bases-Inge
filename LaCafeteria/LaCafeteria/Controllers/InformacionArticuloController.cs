using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class InformacionArticuloController
    {
        private InformacionArticuloDBHandler informacionArticuloDBHandler;

        public InformacionArticuloController() {
            informacionArticuloDBHandler = new InformacionArticuloDBHandler();
        }

        public String GetAutoresDeArticulo(int id) {
            String autores = "";
            List<MiembroModel> listaAutores = informacionArticuloDBHandler.GetAutoresDeArticulo(id);

            for ( int i = 0; i < listaAutores.Count - 1; ++i )
            {
                autores = autores + listaAutores[i].nombre + " " + listaAutores[i].apellido1 + " " + listaAutores[i].apellido2 + ", ";
            }
            autores = autores + listaAutores[listaAutores.Count - 1].nombre + " " + listaAutores[listaAutores.Count - 1].apellido1 + " " + listaAutores[listaAutores.Count - 1].apellido2;

            return autores;
        }
    }
}
