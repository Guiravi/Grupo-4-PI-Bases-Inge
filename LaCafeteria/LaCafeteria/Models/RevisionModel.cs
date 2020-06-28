using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models
{
    public class RevisionModel
    {
        public string usernameMiemFK { get; set; }
        public int idArticuloFK { get; set; }
        public string estadoRevision { get; set; }
        public double puntaje { get; set; }
        public int opinion { get; set; }
        public int contribucion { get; set; }
        public int forma { get; set; }
        public string recomendacion { get; set; }
        public string comentarios { get; set; }
    }
}
