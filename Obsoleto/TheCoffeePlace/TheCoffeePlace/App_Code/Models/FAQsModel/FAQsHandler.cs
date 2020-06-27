using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
/// <summary>
/// Esta clase se encarga de manipular los archivos de FAQs y el modelo FAQsFileModel
/// </summary>
namespace TheCoffeePlace.Models
{

    public class FAQsHandler
    {
        // Este es el constructor de la clase que se encarga de crear el manipulador de los archivos

        public FAQsHandler()
        {

        }
        // Este método devuelve un vector de string para manipular los datos de un archivo 
        public string[] FileInformation(int option,FAQsModel faqsModel)
        {
            if (option == 1)
            {
                return faqsModel.linesFaqs;
            }
            else {
                return faqsModel.linesNumCat;
            }
            
        }
        // Este método rescribe todo el archivo según un vector de strings
        public void WriteAllLines(int option, string[] newLines, FAQsModel faqsModel)
        {
            if (option == 1)
            {
                File.WriteAllLines(faqsModel.filePathFaqs, newLines);

            }
            else {
                File.WriteAllLines(faqsModel.filePathNumCat, newLines);

            }

        }
        // Este método escribe una nueva línea en el archivo 
        public void WriteLastLine(int option, string newLine, FAQsModel faqsModel)
        {
            if (option == 1)
            {
                File.AppendAllText(faqsModel.filePathFaqs, newLine);
            }
            else {
                File.AppendAllText(faqsModel.filePathNumCat, newLine);
            }
        }
    }
}