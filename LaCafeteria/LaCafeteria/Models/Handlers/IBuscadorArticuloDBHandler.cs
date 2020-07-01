using System.Collections.Generic;

namespace LaCafeteria.Models.Handlers
{
    public interface IBuscadorArticuloDBHandler
    {
        List<ArticuloModel> GetArticulosNucleoEsSolicitado(string usernamePK);
        List<ArticuloModel> GetArticulosNucleoLeInteresa(string usernamePK);
        List<ArticuloModel> GetArticulosParaRevisarNucleo(string usernamePK);
        List<ArticuloModel> GetArticulosPorAutorYTipo(string autores, int tipos);
        List<ArticuloModel> GetArticulosPorEstado(string estadoArticulo);
        List<ArticuloModel> GetArticulosPorEstadoAsignaMiem(string estadoArticulo, string username);
        List<ArticuloModel> GetArticulosPorMiembro(string username);
        List<ArticuloModel> GetArticulosPorTituloYTipo(string titulo, int tipos);
        List<ArticuloModel> GetArticulosPorTopicoYTipo(CategoriaTopicoModel catTop, int tipos);
        List<ArticuloModel> GetArticulosRevisionFinalizada();
        List<ArticuloModel> GetTodosArticulos();
    }
}