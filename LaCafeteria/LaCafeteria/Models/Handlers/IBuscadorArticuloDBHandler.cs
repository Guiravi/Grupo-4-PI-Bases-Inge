using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models.Handlers
{
    public interface IBuscadorArticuloDBHandler
    {
        List<ArticuloModel> GetArticulosPorAutorYTipo(String autores, int tipos);
        List<ArticuloModel> GetArticulosPorMiembro(string username);
        List<ArticuloModel> GetArticulosPorMiembroEstado(string username, string estadoArticulo);
        List<ArticuloModel> GetArticulosPorEstado(string estadoArticulo);
        List<ArticuloModel> GetArticulosPorEstadoAsignaMiem(string estadoArticulo, string username);
        List<ArticuloModel> GetArticulosPorTitulo(String titulo, int tipos);
        List<ArticuloModel> GetArticulosPorTopico(CategoriaTopicoModel catTop, int tipos);
        List<ArticuloModel> GetArticulosRevisionFinalizada();
        List<ArticuloModel> GetTodosArticulos();

    }
}
