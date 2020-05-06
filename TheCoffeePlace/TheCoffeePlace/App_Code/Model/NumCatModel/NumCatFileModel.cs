using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
/// <summary>
/// Esta clase es el modelo  del archivo NumCatModel
/// </summary>
namespace TheCoffeePlace.Models
{

    public class NumCatFileModel
    {

        public String filePathNumCat { set; get; }
        //aqui se almacena el contenido de del archivo FAQs
        public string[] linesNumCat { set; get; }
        // Este es el constructor del modelo

        public NumCatFileModel()
        {

            this.filePathNumCat = ConfigurationManager.ConnectionStrings["filePathNumCat"].ToString();
            this.linesNumCat = File.ReadAllLines(this.filePathNumCat);
        }

    }
}