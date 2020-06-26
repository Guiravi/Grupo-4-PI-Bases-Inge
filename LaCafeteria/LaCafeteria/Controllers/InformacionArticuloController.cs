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

        public List<Tuple<string, string, double, string>> GetRevisiones(int id) {
            return informacionArticuloDBHandler.GetRevisiones(id);
        }

        public string GetRevisoresDeArticulo(int id) {
            string revisores = "";
            List<string> listaRevisores = informacionArticuloDBHandler.GetRevisoresDeArticulo(id);

            for ( int i = 0; i < listaRevisores.Count() - 1; i++ )
            {
                revisores = revisores + listaRevisores[i] + ", ";
            }
            revisores = revisores + listaRevisores[listaRevisores.Count() - 1];
            return revisores;
        }

        public string GetTopicosArticuloString(int id) {
            List<TopicoModel> topicos = informacionArticuloDBHandler.GetTopicosArticulo(id);

            string msjTopicos = "";

            for ( int i = 0; i < topicos.Count - 1; ++i )
            {
                msjTopicos = msjTopicos + topicos[i].nombre + ", ";
            }

            msjTopicos = msjTopicos + topicos[topicos.Count - 1].nombre;

            return msjTopicos;
        }


        public List<string> GetTopicosArticuloListaString(int id) {
            string topicos = GetTopicosArticuloString(id);

            List<string> lista = topicos.Split(",").ToList();

            for ( int i = 1; i < lista.Count; i++ )
            {
                lista[i] = lista[i].Remove(0, 1);
            }

            return lista;
        }
    }
}
