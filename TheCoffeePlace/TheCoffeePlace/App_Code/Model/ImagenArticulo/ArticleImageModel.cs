using System.ComponentModel.DataAnnotations;

/// <summary>
/// Summary description for ArticleImageModel
/// </summary>

namespace PruebasConcepto.Models
{
    public class ArticleImageModel
    {
            public int idImagenPK { get; set; }

            public string rutaImagen { get; set; }

            public int idArticuloFK { get; set; }
    }

}