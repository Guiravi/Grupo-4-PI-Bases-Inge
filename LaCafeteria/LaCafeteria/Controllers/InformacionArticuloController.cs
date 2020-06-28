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
            List<string[]> listaAutores = GetAutoresArticuloListaStringArray(id);

            foreach ( string[] miembro in listaAutores )
            {
                autores = autores + miembro[1] + ", ";
            }
            autores = autores.Remove(autores.Length-2);

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

        public List<CategoriaTopicoModel> GetCategoriaTopicosArticulo(int id) {
            return informacionArticuloDBHandler.GetCategoriasTopicosArticulo(id);
        }

        public List<string> GetCategoriaTopicosArticuloString(int id)
        {
            List<string> listaString = new List<string>();
            List<CategoriaTopicoModel> listaModel = informacionArticuloDBHandler.GetCategoriasTopicosArticulo(id);

            foreach (CategoriaTopicoModel catTop in listaModel)
            {
                listaString.Add(catTop.ToString());
            }

            return listaString;

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
