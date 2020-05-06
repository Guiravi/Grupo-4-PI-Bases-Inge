using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
/// <summary>
/// Esta clase es el modelo  del archivo FAQs.txt
/// </summary>
namespace TheCoffeePlace.Models
{

    public class FAQsFileModel
    {

        public String filePathFaqs { set; get; }
        //aqui se almacena el contenido de del archivo linesNumCat
        public string[] linesFaqs { set; get; }
        // Este es el constructor del modelo

        public FAQsFileModel()
        {

            this.filePathFaqs = HttpContext.Current.Server.MapPath(ConfigurationManager.ConnectionStrings["filePathFaqs"].ToString());
            this.linesFaqs = File.ReadAllLines(this.filePathFaqs);
        }

    }
}