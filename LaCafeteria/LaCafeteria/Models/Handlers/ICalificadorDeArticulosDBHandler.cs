namespace LaCafeteria.Models.Handlers
{
    public interface ICalificadorDeArticulosDBHandler
    {
        void CalificarArticulo(string username, int idArticulo, int valorCalif);
    }
}