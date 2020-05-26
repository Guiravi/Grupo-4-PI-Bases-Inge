using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
/// <summary>
/// Esta clase es el modelo  de FAQs
/// </summary>
namespace TheCoffeePlace.Models
{

    public class FAQsModel
    {

        public String filePathFaqs { set; get; }
        //aqui se almacena el contenido del archivo linesNumCat
        public string[] linesFaqs { set; get; }
        // Este es el constructor del modelo
        public String filePathNumCat { set; get; }
        //aqui se almacena el contenido del archivo FAQs
        public string[] linesNumCat { set; get; }
        
        public FAQsModel()
        {

            this.filePathFaqs = HttpContext.Current.Server.MapPath(ConfigurationManager.ConnectionStrings["filePathFaqs"].ToString());
            this.linesFaqs = File.ReadAllLines(this.filePathFaqs);
            this.filePathNumCat = HttpContext.Current.Server.MapPath(ConfigurationManager.ConnectionStrings["filePathNumCat"].ToString());
            this.linesNumCat = File.ReadAllLines(this.filePathNumCat);
        }

    }
}