using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;

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

        public void OnGet()
        {

        }      
       
    }
}