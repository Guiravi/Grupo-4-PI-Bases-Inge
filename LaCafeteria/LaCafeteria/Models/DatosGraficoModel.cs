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

    public class DatosTablaCategoriaTopicos
    {
        public string nombreRolFK { get; set; }

        public string nombreCategoriaFK { get; set; }

        public string nombreTopicoFK { get; set; }

        public int cantidad { get; set; }

        public int visitas { get; set; }

        public double puntajeProm { get; set; }

        public DatosTablaCategoriaTopicos(string nomRol, string nomCat, string nomTop, int cant, int visit, double punt)
        {
            nombreRolFK = nomRol;
            nombreCategoriaFK = nomCat;
            nombreTopicoFK = nomTop;
            cantidad = cant;
            visitas = visit;
            puntajeProm = punt;
        }
    }
}
