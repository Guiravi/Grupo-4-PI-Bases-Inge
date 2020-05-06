using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCoffeePlace.Models;
using TheCoffeePlace.Views;

/// <summary>
/// Summary description for ImagenArticuloController
/// </summary>

namespace TheCoffeePlace.Controllers
{
    public class ImagenArticuloController
    {
        private IView_ImagenArticulo view;

        public ImagenArticuloController(IView_ImagenArticulo view)
        {
            this.view = view;
        }

        public void GuardarImagen()
        {
            ImagenArticuloDBHandler DB_imgArtHandler = new ImagenArticuloDBHandler();
            ImagenArticuloFSHandler FS_imgArtHandler = new ImagenArticuloFSHandler();
            ArticuloDBHandler DB_articuloHandler = new ArticuloDBHandler();

            int idImagen = DB_imgArtHandler.ObtenerSiguienteId();
            
            string rutaImagen = FS_imgArtHandler.AgregarImagen(idImagen, view.nombreArchivoFileUpImagen, view.getContenidoFileUpImagen());

            int idArticulo = DB_articuloHandler.ObtenerSiguienteId();

            ImagenArticuloModel imagenModel = new ImagenArticuloModel(rutaImagen, idArticulo);

            DB_imgArtHandler.AgregarImagen(imagenModel);

        }

        public void ObtenerImagen()
        {
            ImagenArticuloDBHandler DB_imgArtHandler = new ImagenArticuloDBHandler();
            ArticuloDBHandler DB_articuloHandler = new ArticuloDBHandler();

            int idArticulo = DB_articuloHandler.ObtenerSiguienteId();

            view.setContenidoGridViewImagenes(DB_imgArtHandler.ObtenerImagen(idArticulo));

        }

        public void BorrarImagen(int idImagen)
        {
            ImagenArticuloDBHandler DB_imgArtHandler = new ImagenArticuloDBHandler();
            ImagenArticuloFSHandler FS_imgArtHandler = new ImagenArticuloFSHandler();

            FS_imgArtHandler.BorrarImagen(DB_imgArtHandler.ObtenerRutaImagen(idImagen));
            DB_imgArtHandler.BorrarImagen(idImagen);
        }
    }
}
