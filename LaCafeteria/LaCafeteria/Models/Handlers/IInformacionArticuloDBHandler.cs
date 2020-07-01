using System.Collections.Generic;

namespace LaCafeteria.Models.Handlers
{
    public interface IInformacionArticuloDBHandler
    {
        List<DatosGraficoDona> GetArticulosPorRol();
        List<MiembroModel> GetAutoresDeArticulo(int id);
        byte[] GetBinario(int idArticulo);
        int? GetCalificacionMiembro(string username, int idArticulo);
        List<CategoriaTopicoModel> GetCategoriasTopicosArticulo(int id);
        List<DatosTablaCategoriaTopicos> GetDatosTablaCategoriaTopicosPorRol(string rol);
        ArticuloModel GetInformacionArticuloModel(int id);
        List<RevisionModel> GetRevisiones(int id);
        List<string> GetRevisoresDeArticulo(int id);
    }
}