using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCafeteria.Models
{
    public class CategoriaTopicoModel
    {
        public string nombreCategoriaPK { get; set; }
        public string nombreTopicoPK { get; set; }

        public string ToString()
        {
            return nombreCategoriaPK + ": " + nombreTopicoPK;
        }
    }


}
