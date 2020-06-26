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
    }
}
