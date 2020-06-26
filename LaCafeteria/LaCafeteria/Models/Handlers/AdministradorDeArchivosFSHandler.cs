using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LaCafeteria.Models.Handlers
{
    public class AdministradorDeArchivosFSHandler
    {
        public bool YaEstaEnCarpetaPDF(int idArticulo, string rutaCarpeta) {
            string rutaArchivo = rutaCarpeta + "/ArticulosPDF/" + idArticulo + ".pdf";
            return File.Exists(rutaArchivo);
        }

        public bool YaEstaEnCarpetaDOCX(int idArticulo, string rutaCarpeta) {
            string rutaArchivo = rutaCarpeta + "/ArticulosDOCX/" + idArticulo + ".docx";
            return File.Exists(rutaArchivo);
        }

        public void GuardarArticuloDOCX(int idArticulo, byte[] contenido, string rutaCarpeta) {
            File.WriteAllBytes(rutaCarpeta + "/ArticulosDOCX/" + idArticulo + ".docx", contenido);
        }

        public void BorrarViejoArchivo(int idArticulo, string rutaCarpeta) {
            string rutaDocx = rutaCarpeta + "/ArticulosDOCX/" + idArticulo + ".docx";
            string rutaPdf = rutaCarpeta + "/ArticulosPDF/" + idArticulo + ".pdf";

            File.Delete(rutaDocx);
            File.Delete(rutaPdf);
        }
    }
}
