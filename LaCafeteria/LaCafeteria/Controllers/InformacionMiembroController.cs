using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Models;
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
	public class InformacionMiembroController
	{
		private InformacionMiembroDBHandler informacionMiembroDBHandler;
		
		public InformacionMiembroController()
		{
			informacionMiembroDBHandler = new InformacionMiembroDBHandler();
		}

		public List<Notificacion> GetNotificaciones(string usernamePK)
		{
			List<Notificacion> listaNotificaciones = null;
			listaNotificaciones = informacionMiembroDBHandler.GetNotificaciones(usernamePK);

			return listaNotificaciones;
		}

        public List<DatosGraficoDona> GetMiembrosPorPais()
        {
            return informacionMiembroDBHandler.GetMiembrosPorPais();
        }

        public List<DatosGraficoBarrasApilado> GetHabilidadesPorPais()
        {
            List<DatosGraficoBarrasApilado> lista = informacionMiembroDBHandler.GetHabilidadesPorPais();
            List<string> listaSinAsignar = informacionMiembroDBHandler.GetHabilidadesEstandarSinAsignar();

            for (int i = 0; i < listaSinAsignar.Count; i++)
            {
                DatosGraficoBarrasApilado habilidad = new DatosGraficoBarrasApilado(listaSinAsignar[i], "", 0);
                lista.Add(habilidad);
            }

            return lista;
        }

        public List<DatosGraficoBarrasApilado> GetHabilidadesPorIdioma()
        {
            List<DatosGraficoBarrasApilado> lista = informacionMiembroDBHandler.GetHabilidadesPorIdioma();
            List<string> listaSinAsignar = informacionMiembroDBHandler.GetHabilidadesEstandarSinAsignar();

            for (int i = 0; i < listaSinAsignar.Count; i++)
            {
                DatosGraficoBarrasApilado habilidad = new DatosGraficoBarrasApilado(listaSinAsignar[i], "", 0);
                lista.Add(habilidad);
            }

            return lista;
        }

        public List<DatosGraficoBarrasApilado> GetPasatiemposPorPais()
        {
            List<DatosGraficoBarrasApilado> lista = informacionMiembroDBHandler.GetPasatiemposPorPais();
            List<string> listaSinAsignar = informacionMiembroDBHandler.GetPasatiemposEstandarSinAsignar();

            for (int i = 0; i < listaSinAsignar.Count; i++)
            {
                DatosGraficoBarrasApilado pasatiempo = new DatosGraficoBarrasApilado(listaSinAsignar[i], "", 0);
                lista.Add(pasatiempo);
            }

            return lista;
        }

        public List<DatosGraficoBarrasApilado> GetPasatiemposPorIdioma()
        {
            List<DatosGraficoBarrasApilado> lista = informacionMiembroDBHandler.GetPastiemposPorIdioma();
            List<string> listaSinAsignar = informacionMiembroDBHandler.GetPasatiemposEstandarSinAsignar();

            for (int i = 0; i < listaSinAsignar.Count; i++)
            {
                DatosGraficoBarrasApilado pasatiempo = new DatosGraficoBarrasApilado(listaSinAsignar[i], "", 0);
                lista.Add(pasatiempo);
            }

            return lista;
        }

    }
}
