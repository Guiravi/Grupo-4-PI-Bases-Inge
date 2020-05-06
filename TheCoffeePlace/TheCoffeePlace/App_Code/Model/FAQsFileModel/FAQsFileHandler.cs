using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
/// <summary>
/// Esta clase se encarga de manipular el archivo FAQs.txt y el modelo FAQsFileModel
/// </summary>
namespace TheCoffeePlace.Models
{

    public class FAQsFileHandler
    {
        // Este es el constructor de la clase que se encarga de crear el manipulador de los archivos

        public FAQsFileHandler()
        {

        }
        // Este m�todo devuelve un vector de string para manipular los datos de un archivo NumCat.txt
        public string[] FileInformation(FAQsFileModel faqsFileModel)
        {
            return faqsFileModel.linesFaqs;
        }
        // Este m�todo rescribe todo el archivo seg�n un vector de strings
        public void WriteAllLines(string[] newLines, FAQsFileModel faqsFileModel)
        {

            File.WriteAllLines(faqsFileModel.filePathFaqs, newLines);
        }
        // Este m�todo escribe una nueva l�nea en el archivo 
        public void WriteLastLine(string newLine, FAQsFileModel faqsFileModel)
        {
            File.AppendAllText(faqsFileModel.filePathFaqs, newLine);

        }
    }
}