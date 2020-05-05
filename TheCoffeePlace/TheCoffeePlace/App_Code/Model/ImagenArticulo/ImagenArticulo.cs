using System.ComponentModel.DataAnnotations;

/// <summary>
/// Summary description for ArticleImageModel
/// </summary>

namespace TheCoffeePlace.Models
{
    public class ImagenArticulo
    {
            public int idImagenPK { get; set; }

            public string rutaImagen { get; set; }

            public int idArticuloFK { get; set; }
    }
}