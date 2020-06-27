using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models
{
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
