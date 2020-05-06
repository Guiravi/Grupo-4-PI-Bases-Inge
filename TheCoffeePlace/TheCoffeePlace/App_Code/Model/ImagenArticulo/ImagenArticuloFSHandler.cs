using System;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using System.Web;




/// <summary>
/// Summary description for ImagenArticuloFSHandler
/// </summary>

namespace TheCoffeePlace.Models
{
    public class ImagenArticuloFSHandler
    {

        public string AgregarImagen(int idImagen, string nombreImagen, byte[]  contenido)
        {
            string nombreArchivo = Convert.ToString(idImagen) + nombreImagen;

            string rutaImagenCompleta = HttpContext.Current.Server.MapPath("~/Imagenes/ArticulosCortos/" + nombreArchivo);

            File.WriteAllBytes(rutaImagenCompleta, contenido);

            string rutaImagenRelativa = "Imagenes/ArticulosCortos/" + nombreArchivo;

            return rutaImagenRelativa;
        }

        public void BorrarImagen(string rutaImagen)
        {
            string rutaCompleta = HttpContext.Current.Server.MapPath("~/" + rutaImagen);
            try
            {
                System.IO.File.Delete(rutaCompleta);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
          
        }
    }
}
