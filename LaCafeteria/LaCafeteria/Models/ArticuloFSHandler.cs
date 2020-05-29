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
        public void GuardarArticuloDOCX(int idArticulo, byte[] contenido , string rutaCarpeta)
        {
            File.WriteAllBytes( rutaCarpeta + "/ArticulosDOCX/" + idArticulo + ".docx", contenido);
        }

        public void ConvertirDocxPDF(string nombreDocx, string rutaCarpeta)
        {
            Convertidor.DocxConvertToPdf(nombreDocx, rutaCarpeta);
        }

        public bool YaEstaEnCarpetaDOCX(int idArticulo, string rutaCarpeta)
        {
            string rutaArchivo = rutaCarpeta + "/ArticulosDOCX/" + idArticulo + ".docx";
            return File.Exists(rutaArchivo);
        }

        public bool YaEstaEnCarpetaPDF(int idArticulo, string rutaCarpeta)
        {
            string rutaArchivo = rutaCarpeta + "/ArticulosPDF/" + idArticulo + ".pdf";
            return File.Exists(rutaArchivo);
        }
    }
}
