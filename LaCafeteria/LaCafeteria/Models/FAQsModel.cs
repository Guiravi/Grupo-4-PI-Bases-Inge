using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
/// <summary>
/// Esta clase es el modelo  de FAQs
/// </summary>
namespace LaCafeteria.Models

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
            this.filePathFaqs = string.Format("Models/FAQs.txt");
            //Hosting.HostingEnvironment.MapPath(“\filename.txt”);
            //this.filePathFaqs = Server.MapPath("~/App_Data/data.txt");
            //this.filePathFaqs = "~/Models/FAQs.txt";
            this.linesFaqs = File.ReadAllLines(this.filePathFaqs);
            //string[] hola = new string[1]; 
            //hola[0] = "hola";
            //File.WriteAllLines(filePathFaqs,hola);
            this.filePathNumCat = string.Format("Models/NumCat.txt");
            this.linesNumCat = File.ReadAllLines(this.filePathNumCat);
        }

    }
}
