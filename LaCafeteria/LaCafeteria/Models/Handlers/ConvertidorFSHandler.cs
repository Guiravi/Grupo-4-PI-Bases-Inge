using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class ConvertidorFSHandler
    {
        public void ConvertirDocxPDF(string nombreDocx, string rutaCarpeta) {
            Convertidor.DocxConvertToPdf(nombreDocx, rutaCarpeta);
        }
    }
}
