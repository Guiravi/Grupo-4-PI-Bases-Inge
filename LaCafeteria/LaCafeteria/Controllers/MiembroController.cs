using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;

namespace LaCafeteria.Controllers
{
	public class MiembroController
	{
		private MiembroDBHandler miembroDBHandler;

		public MiembroController()
		{
			miembroDBHandler = new MiembroDBHandler();
		}

		public bool ExisteMiembro(string usernamePK)
		{
			return miembroDBHandler.ExisteMiembro(usernamePK);
		}

		public void CrearMiembro(MiembroModel model)
		{
			miembroDBHandler.CrearMiembro(model);
		}

        public void ActualizarMiembro(string usernamePK, MiembroModel miembro)
        {
            miembroDBHandler.ActualizarMiembro(usernamePK, miembro);
        }

		public MiembroModel GetMiembro(string usernamePK)
		{
			return miembroDBHandler.GetMiembro(usernamePK);
		}

		public List<MiembroModel> GetListaMiembros()
		{
			return miembroDBHandler.GetListaMiembros();
		}



        public void CalificarArticulo(string username, int idArticulo, int valorCalif)
        {
            miembroDBHandler.CalificarArticulo(username, idArticulo, valorCalif);
        }

        public List<string[]> GetAutoresArticuloLista(int idArticulo)
        {
            List<MiembroModel> listaModelo = miembroDBHandler.GetAutoresArticulo(idArticulo);
            List<string[]> listaAutores = new List<string[]>();

            foreach (MiembroModel miembro in listaModelo)
            {
                string[] tupla = { miembro.usernamePK, miembro.GetNombreCompleto() };
                listaAutores.Add(tupla);
            }

            return listaAutores;
        }

    }	
}
