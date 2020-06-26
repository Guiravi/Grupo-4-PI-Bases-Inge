using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models
{
    public class ArticuloFSHandler
    {
        

        public void ConvertirDocxPDF(string nombreDocx, string rutaCarpeta)
        {
            Convertidor.DocxConvertToPdf(nombreDocx, rutaCarpeta);
        }






    }
}
