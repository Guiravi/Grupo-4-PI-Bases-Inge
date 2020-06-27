using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class VerReporteFiltrableModel : PageModel
    {
        public List<DatosGraficoDona> miembrosPorPais;
        public List<DatosGraficoBarrasApilado> habilidadesPorIdioma;
        public List<DatosGraficoBarrasApilado> habilidadesPorPais;
        public List<DatosGraficoBarrasApilado> pasatiemposPorIdioma;
        public List<DatosGraficoBarrasApilado> pasatiemposPorPais;
        public List<DatosGraficoDona> miembrosPorRol;
        public List<DatosGraficoDona> articulosPorRol;

        private InformacionMiembroController informacionMiembroController;

        public VerReporteFiltrableModel()
        {
            miembrosPorPais = new List<DatosGraficoDona>();
            habilidadesPorPais = new List<DatosGraficoBarrasApilado>();
            habilidadesPorIdioma = new List<DatosGraficoBarrasApilado>();
            pasatiemposPorPais = new List<DatosGraficoBarrasApilado>();
            pasatiemposPorIdioma = new List<DatosGraficoBarrasApilado>();
            miembrosPorPais = new List<DatosGraficoDona>();
            articulosPorRol = new List<DatosGraficoDona>();

            informacionMiembroController = new InformacionMiembroController();

            
        }

        public void OnGet()
        {
            miembrosPorPais = informacionMiembroController.GetMiembrosPorPais();
            habilidadesPorPais = informacionMiembroController.GetHabilidadesPorPais();
            habilidadesPorIdioma = informacionMiembroController.GetHabilidadesPorIdioma();
            pasatiemposPorPais = informacionMiembroController.GetPasatiemposPorPais();
            pasatiemposPorIdioma = informacionMiembroController.GetPasatiemposPorIdioma();
        }      
       
    }
}