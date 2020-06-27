using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public class DatosGraficoDona
        {
            public string categoria { get; set; }

            public int cantidad { get; set; }

            public DatosGraficoDona(string cat, int cant)
            {
                categoria = cat;
                cantidad = cant;
            }
        }

        public class DatosGraficoBarrasApilado
        {
            public string nombreCategoria { get; set; }

            public string nombreSubcategoria { get; set; }

            public int cantidad { get; set; }

            public DatosGraficoBarrasApilado(string nomCat, string nomSub, int cant)
            {
                nombreCategoria = nomCat;
                nombreSubcategoria = nomSub;
                cantidad = cant;
            }
        }        
       
    }
}