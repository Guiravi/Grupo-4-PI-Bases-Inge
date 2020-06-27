using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class InformacionArticuloController {
        private InformacionArticuloDBHandler informacionArticuloDBHandler;

        public InformacionArticuloController() {
            informacionArticuloDBHandler = new InformacionArticuloDBHandler();
        }

        public String GetAutoresDeArticuloString(int id) {
            String autores = "";
            List<MiembroModel> listaAutores = informacionArticuloDBHandler.GetAutoresDeArticulo(id);

            for ( int i = 0; i < listaAutores.Count - 1; ++i )
            {
                autores = autores + listaAutores[i].nombre + " " + listaAutores[i].apellido1 + " " + listaAutores[i].apellido2 + ", ";
            }
            autores = autores + listaAutores[listaAutores.Count - 1].nombre + " " + listaAutores[listaAutores.Count - 1].apellido1 + " " + listaAutores[listaAutores.Count - 1].apellido2;

            return autores;
        }

        public List<string[]> GetAutoresArticuloListaStringArray(int idArticulo) {
            List<MiembroModel> listaModelo = informacionArticuloDBHandler.GetAutoresDeArticulo(idArticulo);
            List<string[]> listaAutores = new List<string[]>();

            foreach ( MiembroModel miembro in listaModelo )
            {
                string[] tupla = { miembro.usernamePK, miembro.GetNombreCompleto() };
                listaAutores.Add(tupla);
            }

            return listaAutores;
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
            List<string> topicos = informacionArticuloDBHandler.GetTopicosArticulo(id);

            string msjTopicos = "";

            for ( int i = 0; i < topicos.Count - 1; ++i )
            {
                msjTopicos = msjTopicos + topicos[i] + ", ";
            }

            msjTopicos = msjTopicos + topicos[topicos.Count - 1];

            return msjTopicos;
        }

        public List<CategoriaTopicoModel> GetCategoriaTopicosArticulo(int id) {
            return informacionArticuloDBHandler.GetCategoriasTopicosArticulo(id);
        }

        public int GetCalificacionMiembro(string username, int idArticulo) {
            return informacionArticuloDBHandler.GetCalificacionMiembro(username, idArticulo);
        }


        public ArticuloModel GetInformacionArticuloModel(int id) {
            ArticuloModel articulo = informacionArticuloDBHandler.GetInformacionArticuloModel(id);
            return articulo;
        }
    }
}
