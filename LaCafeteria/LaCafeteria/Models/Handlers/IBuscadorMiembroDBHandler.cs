using System.Collections.Generic;

namespace LaCafeteria.Models.Handlers
{
    public interface IBuscadorMiembroDBHandler
    {
        List<MiembroModel> GetListaMiembros();
        List<MiembroModel> GetListaMiembrosDegradar();
        List<MiembroModel> GetListaMiembrosInteresados(int articuloAID);
        List<MiembroModel> GetlistaMiembrosParaAsignarRevision(int articuloAID);
        List<MiembroModel> GetListaMiembrosParaSolicitudRevision(int articuloAID);
        List<MiembroModel> GetListaMiembrosRevisores(int articuloAID);
        List<MiembroModel> GetListaMiembrosSolicitud(string usernameNucleoFK);
        List<MiembroModel> GetListaNucleos();
        List<MiembroModel> GetListaNucleosSolicitud();
        MiembroModel GetMiembro(string usernamePK);
        MiembroModel GetMiembroCoordinador();
        string GetRango(string usernamePK);
    }
}